using System;
using DAL.EF;

namespace DAL.Interfaces
{
	public interface IBudgetFeatures
	{
		List<Budget> SearchByBudget(string name);
        List<Budget> FilterByAmountRange(decimal minAmount, decimal maxAmount);
        List<Budget> DisplayByPage(int page, int pageSize);
        decimal GetCategoryBudgetRemaining(int userId, string category, decimal categoryBudget);
        decimal GetTotalBudgetByUser(int userId);
    }
}

