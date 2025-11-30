using System;
using DAL.EF;

namespace DAL.Interfaces
{
    public interface IUserFeatures
    {
        List<User> SearchByName(string name);
        List<User> DisplayByPage(int page, int pageSize);

    }
}

