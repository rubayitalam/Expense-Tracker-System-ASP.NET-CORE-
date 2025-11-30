using System;
using DAL.EF;
using DAL.Interfaces;

namespace DAL.Repos
{
    public class UserRepo : Repo, IRepo<User, int, User>, IUserFeatures
    {
        public User Create(User obj)
        {
            db.Users.Add(obj);
            db.SaveChanges();
            return obj;
        }

        public bool Delete(int id)
        {
            var ex = Get(id);
            db.Users.Remove(ex);
            return db.SaveChanges() > 0;
        }

        public User Get(int id)
        {
            return db.Users.Find(id);
        }

        public List<User> Get()
        {
            return db.Users.ToList();
        }

        public User Update(User obj)
        {
            var ex = Get(obj.Id);
            db.Entry(ex).CurrentValues.SetValues(obj);
            db.SaveChanges();
            return ex;
        }

        bool IRepo<User, int, User>.Delete(User user)
        {
            throw new NotImplementedException();
        }

        public List<User> SearchByName(string name)
        {
            return db.Users.Where(x => x.UserName.Contains(name)).ToList();
        }
        public List<User> DisplayByPage(int page, int pageSize)
        {
            return db.Users.OrderBy(p => p.Id).Skip((page - 1) * pageSize).Take(pageSize).ToList();
        }

    }
}
