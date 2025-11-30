using BLL.DTOs;
using BLL.Services;
using Microsoft.AspNetCore.Mvc;

namespace FinalLab.Controllers
{
    [ApiController]
    [Route("Budget")]
    public class BudgetController : ControllerBase
    {
        // GET: api/budgets
        [HttpGet("List")]
        public IActionResult GetAllBudgets()
        {
            try
            {
                var budgets = BudgetService.Get();
                if (budgets == null || !budgets.Any())
                {
                    return NotFound(new { message = "No budgets found." });
                }
                return Ok(budgets);
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { message = ex.Message });
            }
        }

        // GET: api/budgets/{id}
        [HttpGet("Search")]
        public IActionResult GetBudgetById(int id)
        {
            try
            {
                var budget = BudgetService.Get(id);
                if (budget == null)
                {
                    return NotFound(new { message = "Budget not found." });
                }
                return Ok(budget);
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { message = ex.Message });
            }
        }

        // POST: api/budgets
        [HttpPost("Create")]
        public IActionResult CreateBudget([FromBody] BudgetDTO budgetDto)
        {
            try
            {
                if (budgetDto == null)
                {
                    return BadRequest(new { message = "Invalid budget data." });
                }

                // Check if the ID is 0 (indicating it's a new budget) before creating
                if (budgetDto.Id != 0)
                {
                    return BadRequest(new { message = "ID should not be set for new budgets." });
                }

                var createdBudget = BudgetService.Create(budgetDto);
                if (createdBudget == null)
                {
                    return StatusCode(500, new { message = "Error creating budget." });
                }

                return CreatedAtAction(nameof(GetBudgetById), new { id = createdBudget.Id }, createdBudget);
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { message = ex.Message });
            }
        }

        // PUT: api/budgets/{id}
        [HttpPut("Updated")]
        public IActionResult UpdateBudget(int id, [FromBody] BudgetDTO budgetDto)
        {
            try
            {
                if (budgetDto == null || id != budgetDto.Id)
                {
                    return BadRequest(new { message = "Invalid budget data or ID mismatch." });
                }

                var updatedBudget = BudgetService.Update(budgetDto);
                if (updatedBudget == null)
                {
                    return NotFound(new { message = "Budget not found." });
                }

                return Ok(new { message = "Budget updated successfully.", data = updatedBudget });
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { message = ex.Message });
            }
        }

        // DELETE: api/budgets/{id}
        [HttpDelete("Delete")]
        public IActionResult DeleteBudget(int id)
        {
            try
            {
                if (id <= 0)
                {
                    return BadRequest(new { message = "Invalid budget ID." });
                }

                var isDeleted = BudgetService.Delete(id);
                if (isDeleted)
                {
                    return Ok(new { message = "Budget deleted successfully." });
                }
                else
                {
                    return NotFound(new { message = "Budget not found." });
                }
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { message = ex.Message });
            }
        }

        [HttpGet("filter Amount-Range")]
        public IActionResult FilterByAmountRange(decimal minAmount, decimal maxAmount)
        {
            try
            {
                // Call the service method to filter budgets
                var budgets = BudgetService.FilterByAmountRange(minAmount, maxAmount);

                if (budgets == null || budgets.Count == 0)
                {
                    // Return a 404 response if no results are found
                    return NotFound(new { message = "No budgets found within the specified amount range." });
                }

                // Return the filtered budgets with a 200 response
                return Ok(budgets);
            }
            catch (Exception ex)
            {
                // Handle errors and return a 500 response
                return StatusCode(500, new { message = ex.Message });
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

                var budgets = BudgetService.DisplayByPage(page, pageSize);
                if (budgets == null || budgets.Count == 0)
                {
                    return NotFound(new { message = "No expenses found." });
                }

                return Ok(budgets);
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { message = ex.Message });
            }
        }

        [HttpGet("Budget-remaining")]
        public IActionResult GetCategoryBudgetRemaining(int userId, string category, decimal categoryBudget)
        {
            try
            {
                if (string.IsNullOrWhiteSpace(category) || categoryBudget <= 0)
                {
                    return BadRequest(new { message = "Invalid category or budget value." });
                }

                var remainingBudget = BudgetService.GetCategoryBudgetRemaining(userId, category, categoryBudget);

                return Ok(new
                {
                    message = "Category budget remaining calculated successfully.",
                    data = new
                    {
                        Category = category,
                        RemainingBudget = remainingBudget
                    }
                });
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { message = ex.Message });
            }
        }
        [HttpGet("User Total Budget")]
        public IActionResult GetTotalBudgetByUser([FromQuery] int userId)
        {
            try
            {
                if (userId <= 0)
                {
                    return BadRequest(new { message = "Invalid user ID provided." });
                }

                var totalBudget = BudgetService.GetTotalBudgetByUser(userId);
                return Ok(new { UserId = userId, TotalExpense = totalBudget });
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { message = ex.Message });
            }
        }


    }
}


