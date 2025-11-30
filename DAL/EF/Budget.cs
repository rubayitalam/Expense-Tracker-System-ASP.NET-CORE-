using System;
using System.ComponentModel.DataAnnotations.Schema;

namespace DAL.EF
{
	public class Budget
	{
        public int Id { get; set; }  
        public decimal Amount { get; set; }
        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }
        [ForeignKey("Cate")]
        public int UserId { get; set; }
        public virtual User Cate { get; set; }
    }
}

