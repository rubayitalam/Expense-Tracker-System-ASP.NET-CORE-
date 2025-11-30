using System;
using DAL.EF;

namespace DAL.Repos
{
    public class Repo
    {
        protected FinalLabContext db;
        protected Repo()
        {
            db = new FinalLabContext();
        }
    }
}

