using System;
namespace DAL.EF
{
	public class User
	{
        public int Id { get; set; }  
        public string UserName { get; set; }
        public string Email { get; set; }
        public string Password { get; set; }
        public virtual List<Expense> Expenses { get; set; }
        public virtual List<Budget> Budgets { get; set; }
        public User()
        {
            Expenses = new List<Expense>();
            Budgets = new List<Budget>();

        }
    }
}

