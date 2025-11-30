
using System.Text;
using BLL.DTOs;
using BLL.Services;
using DAL;
using Microsoft.AspNetCore.Mvc;

namespace FinalLab.Controllers
{
    [ApiController]
    [Route("Expenses")]
    public class ExpenseController : ControllerBase
    {
        // GET: api/expenses
        [HttpGet("List")]
        public IActionResult GetAllExpenses()
        {
            try
            {
                var expenses = ExpenseService.Get();
                if (expenses == null || !expenses.Any())
                {
                    return NotFound(new { message = "No expenses found." });
                }
                return Ok(expenses);
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { message = ex.Message });
            }
        }

        
        // POST: api/expenses
        [HttpPost("Create")]
        public IActionResult CreateExpense([FromBody] ExpenseDTO expenseDto)
        {
            try
            {
                if (expenseDto == null)
                {
                    return BadRequest(new { message = "Invalid expense data." });
                }

                // Check if the ID is 0 (indicating it's a new expense) before creating
                if (expenseDto.Id != 0)
                {
                    return BadRequest(new { message = "ID should not be set for new expenses." });
                }

                var createdExpense = ExpenseService.Create(expenseDto);
                if (createdExpense == null)
                {
                    return StatusCode(500, new { message = "Error creating expense." });
                }

                return CreatedAtAction(nameof(GetExpenseById), new { id = createdExpense.Id }, createdExpense);
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { message = ex.Message });
            }
        }

        // PUT: api/expenses/{id}
        [HttpPut("Updated")]
        public IActionResult UpdateExpense(int id, [FromBody] ExpenseDTO expenseDto)
        {
            try
            {
                if (expenseDto == null || id != expenseDto.Id)
                {
                    return BadRequest(new { message = "Invalid expense data or ID mismatch." });
                }

                var updatedExpense = ExpenseService.Update(expenseDto);
                if (updatedExpense == null)
                {
                    return NotFound(new { message = "Expense not found." });
                }

                return Ok(new { message = "Expense updated successfully.", data = updatedExpense });
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { message = ex.Message });
            }
        }

        // DELETE: api/expenses/{id}
        [HttpDelete("Delete")]
        public IActionResult DeleteExpense(int id)
        {
            try
            {
                if (id <= 0)
                {
                    return BadRequest(new { message = "Invalid expense ID." });
                }

                var isDeleted = ExpenseService.Delete(id);
                if (isDeleted)
                {
                    return Ok(new { message = "Expense deleted successfully." });
                }
                else
                {
                    return NotFound(new { message = "Expense not found." });
                }
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { message = ex.Message });
            }
        }
        // GET: api/expenses/{id}
        [HttpGet("Search-By-Id")]
        public IActionResult GetExpenseById(int id)
        {
            try
            {
                var expense = ExpenseService.Get(id);
                if (expense == null)
                {
                    return NotFound(new { message = "Expense not found." });
                }
                return Ok(expense);
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { message = ex.Message });
            }
        }




        [HttpGet("Search-By-Category")]
        public IActionResult SearchExpensesByCategory([FromQuery] string category)
        {
            try
            {
                if (string.IsNullOrEmpty(category))
                {
                    return BadRequest(new { message = "Category name cannot be null or empty." });
                }

                // Debug this line
                var expenses = ExpenseService.SearchByCategory(category);

                if (expenses == null || !expenses.Any())
                {
                    return NotFound(new { message = "No expenses found matching the specified category." });
                }

                return Ok(expenses);
            }
            catch (Exception ex)
            {
                // Log exception for debugging
                Console.WriteLine(ex);
                return StatusCode(500, new { message = "An error occurred while searching for expenses.", error = ex.Message });
            }
        }

        [HttpGet("Filter ")]
        public IActionResult FilterExpenses([FromQuery] string category, [FromQuery] DateTime? startDate)
        {
            try
            {
                // Validation: Ensure at least the category is provided
                if (string.IsNullOrEmpty(category) && !startDate.HasValue)
                {
                    return BadRequest(new { message = "You must provide at least one filter: category or start date." });
                }

                // Call the service to filter expenses
                var filteredExpenses = ExpenseService.FilterByCategoryAndDate(category, startDate);

                // If no results found, return 404
                if (filteredExpenses == null || !filteredExpenses.Any())
                {
                    return NotFound(new { message = "No expenses found matching the specified criteria." });
                }

                // Return the filtered expenses
                return Ok(filteredExpenses);
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { message = "An error occurred while filtering expenses.", error = ex.Message });
            }
        }

        [HttpGet("Display By Page")]
        public IActionResult DisplayByPage([FromQuery] int page, [FromQuery] int pageSize)
        {
            try
            {
                if (page <= 0 || pageSize <= 0)
                {
                    return BadRequest(new { message = "Invalid page or pageSize value." });
                }

                var expenses = ExpenseService.DisplayByPage(page, pageSize);
                if (expenses == null || expenses.Count == 0)
                {
                    return NotFound(new { message = "No expenses found." });
                }

                return Ok(expenses);
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { message = ex.Message });
            }
        }

        [HttpGet("total-expense")]
        public IActionResult GetTotalExpense(DateTime startDate, DateTime endDate)
        {
            try
            {
                // Call the service to get the total expense
                decimal totalExpense = ExpenseService.GetTotalExpenseByDateRange(startDate, endDate);

                // Return the result in the response
                return Ok(new { totalExpense });
            }
            catch (Exception ex)
            {
                // If any error occurs, return a 500 response with the error message
                return StatusCode(500, new { message = ex.Message });
            }
        }
        [HttpGet("User Total Expense")]
        public IActionResult GetTotalExpenseByUser([FromQuery] int userId)
        {
            try
            {
                if (userId <= 0)
                {
                    return BadRequest(new { message = "Invalid user ID provided." });
                }

                var totalExpense = ExpenseService.GetTotalExpenseByUser(userId);
                return Ok(new { UserId = userId, TotalExpense = totalExpense });
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { message = ex.Message });
            }
        }

        //[HttpGet("Export-CSV")]
        //public IActionResult ExportExpensesToCSV()
        //{
        //    try
        //    {
        //        var csvData = ExpenseService.ExportExpensesToCSV();

        //        if (string.IsNullOrEmpty(csvData))
        //        {
        //            return NotFound(new { message = "No expenses available to export." });
        //        }

        //        // Convert CSV string to a byte array
        //        var bytes = Encoding.UTF8.GetBytes(csvData);

        //        // Return the CSV file as a downloadable response
        //        return File(bytes, "text/csv", "expenses.csv");
        //    }
        //    catch (Exception ex)
        //    {
        //        return StatusCode(500, new { message = "An error occurred while exporting expenses.", error = ex.Message });
        //    }
        //}
        [HttpGet("Export-CSV")]
        public IActionResult ExportFilteredExpensesToCSV([FromQuery] string? category, [FromQuery] DateTime? startDate, [FromQuery] DateTime? endDate)
        {
            try
            {
                var csvData = ExpenseService.ExportFilteredExpensesToCSV(category, startDate, endDate);

                if (string.IsNullOrEmpty(csvData))
                {
                    return NotFound(new { message = "No expenses found for the given filters." });
                }

                // Convert CSV string to a byte array
                var bytes = Encoding.UTF8.GetBytes(csvData);

                // Return the CSV file as a downloadable response
                return File(bytes, "text/csv", "filtered_expenses.csv");
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { message = "An error occurred while exporting expenses.", error = ex.Message });
            }
        }


        [HttpGet("summary/{userId}")]
        public IActionResult GetExpenseSummary(int userId)
        {
            try
            {
                // Call the ExpenseSummary service to get the summary for the given userId
                var summary = ExpenseSummary(userId);

                // If the summary is null or empty, return a NotFound result
                if (summary == null)
                {
                    return NotFound(new { message = "Expense summary not found." });
                }

                // Return the summary in the response
                return Ok(summary);
            }
            catch (Exception ex)
            {
                // Log the exception (logging omitted for brevity)
                return StatusCode(500, new { message = ex.Message });
            }
        }

        // Service method as a static call (assuming it is a static method)
        public static object ExpenseSummary(int userId)
        {
            var repo = DataAccessFactory.ExpenseFeatures();
            return repo.ExpenseSummary(userId);
        }
    }

}




