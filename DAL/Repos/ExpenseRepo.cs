using System;
using global::DAL.EF;
using global::DAL.Interfaces;

namespace DAL.Repos
{
    public class ExpenseRepo : Repo, IRepo<Expense, int, Expense>, IExpenseFeatures
    {
        public Expense Create(Expense obj)
        {
            db.Expenses.Add(obj);
            db.SaveChanges();
            return obj;
        }

        public bool Delete(int id)
        {
            var ex = Get(id);
            db.Expenses.Remove(ex);
            return db.SaveChanges() > 0;
        }

        public Expense Get(int id)
        {
            return db.Expenses.Find(id);
        }

        public List<Expense> Get()
        {
            return db.Expenses.ToList();
        }

        //public List<Expense> SearchByCategory(string name)
        //{
        //    throw new NotImplementedException();
        //}
        public List<Expense> SearchByCategory(string name)
        {
            return db.Expenses.Where(x => x.Category.Contains(name)).ToList();
        }


        public List<Expense> FilterByCategoryAndDate(string category, DateTime? startDate)
        {
            var query = db.Expenses.AsQueryable();

            if (!string.IsNullOrEmpty(category))
            {
                query = query.Where(x => x.Category.Contains(category)); // Filter by category
            }

            if (startDate.HasValue)
            {
                query = query.Where(x => x.Date >= startDate.Value); // Filter by start date
            }

            return query.ToList();
        }


        public Expense Update(Expense obj)
        {
            var ex = Get(obj.Id);
            db.Entry(ex).CurrentValues.SetValues(obj);
            db.SaveChanges();
            return ex;
        }

        bool IRepo<Expense, int, Expense>.Delete(User user)
        {
            throw new NotImplementedException();
        }

        public List<Expense> DisplayByPage(int page, int pageSize)
        {
            return db.Expenses.OrderBy(p => p.Id).Skip((page - 1) * pageSize).Take(pageSize).ToList();
        }

        public decimal GetTotalExpenseByDateRange(DateTime startDate, DateTime endDate)
        {
            return db.Expenses
                     .Where(x => x.Date >= startDate && x.Date <= endDate)
                     .Sum(x => x.Amount);  
        }
        public decimal GetTotalExpenseByUser(int userId)
        {
            return db.Expenses
                     .Where(e => e.UserId == userId)
                     .Sum(e => e.Amount);
        }
        public object ExpenseSummary(int userId)
        {
            var ret= db.Expenses
                     .Where(e => e.UserId == userId)
                     .Sum(e => e.Amount);
            var us = db.Expenses.Where(e => e.UserId == userId).Select(e => new
            { ExpenseCategory =e.Category,
              ExpenseAmont = e.Amount
            }).ToList();
  
            return new
            {
                Categories = us,
                TotalExpense = ret
            };
        }

    }
}




