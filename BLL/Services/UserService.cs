using System;
using AutoMapper;
using BLL.DTOs;
using DAL;
using DAL.EF;

namespace BLL.Services
{
    public class UserService
    {
         
            public static Mapper GetMapper()
            {
                var config = new MapperConfiguration(cfg =>
                {
                    cfg.CreateMap<User, UserDTO>();
                    cfg.CreateMap<UserDTO, User>();
                    cfg.CreateMap<User, UserExpenseDTO>();
                    cfg.CreateMap<Expense, ExpenseDTO>();
                });
                return new Mapper(config);
            }
        public static UserDTO Create(UserDTO s)
        {
            var user = DataAccessFactory.UserData();
            var ret = user.Create(GetMapper().Map<User>(s));
            return GetMapper().Map<UserDTO>(ret);
        }


        public static List<UserDTO> Get()
            {
                var repo = DataAccessFactory.UserData();
                return GetMapper().Map<List<UserDTO>>(repo.Get());
            }

        public static UserDTO Update(UserDTO s)
        {
            var user = DataAccessFactory.UserData();
            var updatedEntity = user.Update(GetMapper().Map<User>(s));
            return updatedEntity != null ? GetMapper().Map<UserDTO>(updatedEntity) : null;
        }



        public static bool Delete(int id)
        {
            var user = DataAccessFactory.UserData();
            return user.Delete(id);
        }



        public static UserDTO Get(int id)
            {
                var repo = DataAccessFactory.UserData();
                var category = repo.Get(id);
                var ret = GetMapper().Map<UserDTO>(category);
                return ret;

            }
            public static UserExpenseDTO GetwithProducts(int id)
            {
                var repo = DataAccessFactory.UserData();
                var category = repo.Get(id);
                var ret = GetMapper().Map<UserExpenseDTO>(category);
                return ret;

            }


        public static List<UserDTO> SearchByName(string name)
        {
            var repo = DataAccessFactory.UserFeatures();
            var ret = GetMapper().Map<List<UserDTO>>(repo.SearchByName(name));
            return ret;
        }

        public static List<UserDTO> DisplayByPage(int page, int pageSize)
        {
            var repo = DataAccessFactory.UserFeatures();
            var ret = GetMapper().Map<List<UserDTO>>(repo.DisplayByPage(page, pageSize));
            return ret;
        }

    }
}

