using System;
using DAL.EF;

namespace DAL.Interfaces
{
    public interface IExpenseFeatures
    {
        List<Expense> SearchByCategory(string name);
        List<Expense> FilterByCategoryAndDate(string category, DateTime? startDate);
        List<Expense> DisplayByPage(int page, int pageSize);
        decimal GetTotalExpenseByDateRange(DateTime startDate, DateTime endDate);
        decimal GetTotalExpenseByUser(int userId);
        object ExpenseSummary(int userId);

    }
}

