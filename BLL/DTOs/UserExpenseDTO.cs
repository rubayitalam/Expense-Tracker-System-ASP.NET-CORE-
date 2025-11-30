using System;
namespace BLL.DTOs
{
	public class UserExpenseDTO : UserDTO
	{
		public List<ExpenseDTO> Expenses { get; set; }
		public UserExpenseDTO()
		{
			Expenses = new List<ExpenseDTO>();
		}
	}

}