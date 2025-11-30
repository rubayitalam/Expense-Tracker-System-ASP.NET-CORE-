using System;
namespace BLL.DTOs
{
	public class ExpenseDTO
	{
        public int Id { get; set; }
        public int UserId { get; set; }
        public decimal Amount { get; set; }
        public string Category { get; set; }
        public DateTime Date { get; set; }
        public string Description { get; set; }
        public bool IsRecurring { get; set; }
        
    }
}

