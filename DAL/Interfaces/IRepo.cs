using System;
using DAL.EF;

namespace DAL.Interfaces
{
    public interface IRepo<CLASS, ID, RET>
    {
        RET Create(CLASS obj);
        RET Update(CLASS obj);
        CLASS Get(ID id);
        List<CLASS> Get();
        bool Delete(ID id);
        //bool Delete(Student student);
        bool Delete(User user);

    }

}