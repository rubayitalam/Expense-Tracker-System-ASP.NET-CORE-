using System;
namespace BLL.DTOs
{
	public class BudgetDTO
	{
        
        public int Id { get; set; }
        public int UserId { get; set; }
        public decimal Amount { get; set; }
        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }
    }
}

