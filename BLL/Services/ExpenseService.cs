
using System;
using System.Text;
using AutoMapper;
using BLL.DTOs;
using DAL;
using DAL.EF;

namespace BLL.Services
{
    public class ExpenseService
    {
        public static Mapper GetMapper()
        {
            var config = new MapperConfiguration(cfg =>
            {
                cfg.CreateMap<Expense, ExpenseDTO>();
                cfg.CreateMap<ExpenseDTO, Expense>();
            });
            return new Mapper(config);
        }

        public static List<ExpenseDTO> Get()
        {
            var repo = DataAccessFactory.ExpenseData();
            return GetMapper().Map<List<ExpenseDTO>>(repo.Get());
        }

        public static ExpenseDTO Get(int id)
        {
            var repo = DataAccessFactory.ExpenseData();
            var expense = repo.Get(id);
            var ret = GetMapper().Map<ExpenseDTO>(expense);
            return ret;
        }

        public static ExpenseDTO Create(ExpenseDTO e)
        {
            //var repo = DataAccessFactory.ExpenseData();
            //var expense = GetMapper().Map<Expense>(expenseDTO);
            //return repo.Create(expense);


            var user = DataAccessFactory.ExpenseData();
            var ret = user.Create(GetMapper().Map<Expense>(e));
            return GetMapper().Map<ExpenseDTO>(ret);
        }

        public static ExpenseDTO Update(ExpenseDTO e)
        {
            var repo = DataAccessFactory.ExpenseData();
            var expense = repo.Update(GetMapper().Map<Expense>(e));
            return GetMapper().Map<ExpenseDTO>(expense);
        }

        public static bool Delete(int id)
        {
            var repo = DataAccessFactory.ExpenseData();
            return repo.Delete(id);
        }

        public static List<ExpenseDTO> SearchByCategory(string name)
        {
            var repo = DataAccessFactory.ExpenseFeatures();
            var ret = GetMapper().Map<List<ExpenseDTO>>(repo.SearchByCategory(name));
            return ret;
        }

        public static List<ExpenseDTO> FilterByCategoryAndDate(string category, DateTime? startDate)
        {
            var repo = DataAccessFactory.ExpenseFeatures();
            var filteredExpenses = repo.FilterByCategoryAndDate(category, startDate);

            
            return GetMapper().Map<List<ExpenseDTO>>(filteredExpenses);
        }

        public static List<ExpenseDTO> DisplayByPage(int page, int pageSize)
        {
            var repo = DataAccessFactory.ExpenseFeatures();
            var ret = GetMapper().Map<List<ExpenseDTO>>(repo.DisplayByPage(page, pageSize));
            return ret;
        }

        public static decimal GetTotalExpenseByDateRange(DateTime startDate, DateTime endDate)
        {
            var repo = DataAccessFactory.ExpenseFeatures();  
            return repo.GetTotalExpenseByDateRange(startDate, endDate);  
        }


        public static decimal GetTotalExpenseByUser(int userId)
        {
            var repo = DataAccessFactory.ExpenseFeatures();
            return repo.GetTotalExpenseByUser(userId);
        }

        
        public static string ExportFilteredExpensesToCSV(string category = null, DateTime? startDate = null, DateTime? endDate = null)
        {
            var repo = DataAccessFactory.ExpenseData();
            var expenses = repo.Get(); 

            
            if (!string.IsNullOrEmpty(category))
            {
                expenses = expenses.Where(e => e.Category.Equals(category, StringComparison.OrdinalIgnoreCase)).ToList();
            }

            if (startDate.HasValue)
            {
                expenses = expenses.Where(e => e.Date >= startDate.Value).ToList();
            }

            if (endDate.HasValue)
            {
                expenses = expenses.Where(e => e.Date <= endDate.Value).ToList();
            }

            if (!expenses.Any())
            {
                return null; // Return null if no data matches the filters
            }

            // Create a StringBuilder for the CSV content
            var csv = new StringBuilder();

            // Add the header row
            csv.AppendLine("Id,Category,Amount,Date,UserId");

            // Add data rows
            foreach (var expense in expenses)
            {
                csv.AppendLine($"{expense.Id},{expense.Category},{expense.Amount},{expense.Date},{expense.UserId}");
            }

            return csv.ToString(); // Return the CSV data as a string
        }

        public static object ExpenseSummary(int userId)
        {
            var repo = DataAccessFactory.ExpenseFeatures();
            return repo.ExpenseSummary(userId);
        }

    }
}
