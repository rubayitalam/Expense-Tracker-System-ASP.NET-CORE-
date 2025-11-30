

using System;
using DAL.EF;
using DAL.Interfaces;
using DAL.Repos;

namespace DAL
{
    public class DataAccessFactory
    {
        public static IRepo<Expense, int, Expense> ExpenseData()
        {
            return new ExpenseRepo();
        }
        public static IRepo<User, int, User> UserData()
        {
            return new UserRepo();
        }
        public static IRepo<Budget, int, Budget> BudgetData()
        {
            return new BudgetRepo();
        }
        public static IExpenseFeatures ExpenseFeatures()
        {
            return new ExpenseRepo();
        }
        public static IUserFeatures UserFeatures()
        {
            return new UserRepo();
        }

        public static IBudgetFeatures BudgetFeatures()
        {
            return new BudgetRepo();
        }
    }
}

