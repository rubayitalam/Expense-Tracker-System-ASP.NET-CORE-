using System;
using System.ComponentModel.DataAnnotations.Schema;

namespace DAL.EF
{
	public class Expense
	{
        public int Id { get; set; }  
        public decimal Amount { get; set; }
        public string Category { get; set; }
        public DateTime Date { get; set; }
        public string Description { get; set; }
        public bool IsRecurring { get; set; }
        [ForeignKey("Cate")]
        public int UserId { get; set; }
        public virtual User Cate { get; set; }
    }
}

