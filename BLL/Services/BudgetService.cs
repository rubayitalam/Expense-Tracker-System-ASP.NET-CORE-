   using AutoMapper;
    using DAL;
    using DAL.EF;
using global::BLL.DTOs;
namespace BLL.Services
{
        public class BudgetService
        {
            public static Mapper GetMapper()
            {
                var config = new MapperConfiguration(cfg =>
                {
                    cfg.CreateMap<Budget, BudgetDTO>();
                    cfg.CreateMap<BudgetDTO, Budget>();
                });
                return new Mapper(config);
            }

            public static List<BudgetDTO> Get()
            {
                var repo = DataAccessFactory.BudgetData();
                return GetMapper().Map<List<BudgetDTO>>(repo.Get());
            }

            public static BudgetDTO Get(int id)
            {
                var repo = DataAccessFactory.BudgetData();
                var budget = repo.Get(id);
                var ret = GetMapper().Map<BudgetDTO>(budget);
                return ret;
            }

            public static BudgetDTO Create(BudgetDTO e)
            {
                //var repo = DataAccessFactory.ExpenseData();
                //var expense = GetMapper().Map<Expense>(expenseDTO);
                //return repo.Create(expense);


                var user = DataAccessFactory.BudgetData();
                var ret = user.Create(GetMapper().Map<Budget>(e));
                return GetMapper().Map<BudgetDTO>(ret);
            }

            public static BudgetDTO Update(BudgetDTO e)
            {
                var repo = DataAccessFactory.BudgetData();
                var budget = repo.Update(GetMapper().Map<Budget>(e));
                return GetMapper().Map<BudgetDTO>(budget);
            }

            public static bool Delete(int id)
            {
                var repo = DataAccessFactory.BudgetData();
                return repo.Delete(id);
            }

        public static List<BudgetDTO> FilterByAmountRange(decimal minAmount, decimal maxAmount)
        {
            var repo = DataAccessFactory.BudgetFeatures(); // Access the repository
            var budgets = repo.FilterByAmountRange(minAmount, maxAmount); // Fetch data from the repository
            return GetMapper().Map<List<BudgetDTO>>(budgets); // Map entity to DTO
        }

        public static List<BudgetDTO> DisplayByPage(int page, int pageSize)
        {
            var repo = DataAccessFactory.BudgetFeatures();
            var ret = GetMapper().Map<List<BudgetDTO>>(repo.DisplayByPage(page, pageSize));
            return ret;
        }
        public static decimal GetCategoryBudgetRemaining(int userId, string category, decimal categoryBudget)
        {
            var repo = DataAccessFactory.BudgetFeatures(); // Get repository instance
            return repo.GetCategoryBudgetRemaining(userId, category, categoryBudget);
        }

        public static decimal GetTotalBudgetByUser(int userId)
        {
            var repo = DataAccessFactory.BudgetFeatures();
            return repo.GetTotalBudgetByUser(userId);
        }

    }
}



