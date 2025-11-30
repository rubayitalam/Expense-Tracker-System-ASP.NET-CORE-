using System;
using global::DAL.EF;
using global::DAL.Interfaces;

namespace DAL.Repos
{
    public class BudgetRepo : Repo, IRepo<Budget, int, Budget>, IBudgetFeatures
    {
        public Budget Create(Budget obj)
        {
            db.Budgets.Add(obj);
            db.SaveChanges();
            return obj;
        }

        public bool Delete(int id)
        {
            var ex = Get(id);
            db.Budgets.Remove(ex);
            return db.SaveChanges() > 0;
        }

        public Budget Get(int id)
        {
            return db.Budgets.Find(id);
        }

        public List<Budget> Get()
        {
            return db.Budgets.ToList();
        }

        public List<Budget> SearchByBudget(string name)
        {
            throw new NotImplementedException();
        }

        public Budget Update(Budget obj)
        {
            var ex = Get(obj.Id);
            db.Entry(ex).CurrentValues.SetValues(obj);
            db.SaveChanges();
            return ex;
        }

        bool IRepo<Budget, int, Budget>.Delete(User user)
        {
            throw new NotImplementedException();
        }
        public List<Budget> FilterByAmountRange(decimal minAmount, decimal maxAmount)
        {
            return db.Budgets
                     .Where(x => x.Amount >= minAmount && x.Amount <= maxAmount)
                     .ToList();
        }

        public List<Budget> DisplayByPage(int page, int pageSize)
        {
            return db.Budgets.OrderBy(p => p.Id).Skip((page - 1) * pageSize).Take(pageSize).ToList();
        }
        public decimal GetCategoryBudgetRemaining(int userId, string category, decimal categoryBudget)
        {
            // Calculate total spent in the specified category for the user
            var totalSpent = db.Expenses
                .Where(e => e.UserId == userId && e.Category == category)
                .Sum(e => e.Amount);

            // Calculate remaining budget for the category
            return categoryBudget - totalSpent;
        }

        public decimal GetTotalBudgetByUser(int userId)
        {
            return db.Budgets
                     .Where(e => e.UserId == userId)
                     .Sum(e => e.Amount);
        }


    }
}




