using BLL.DTOs;
using BLL.Services;
using Microsoft.AspNetCore.Mvc;

namespace FinalLab.Controllers
{
    [ApiController]
    [Route("User")]
    public class UserController : ControllerBase
    {
        
        // GET: api/student
        [HttpGet("List")]
        public IActionResult GetAllStudents()
        {
            try
            {
                var users = UserService.Get();
                return Ok(users);
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { message = ex.Message });
            }
        }


        [HttpGet("Seacrh")]
        public IActionResult GetStudentById(int id)
        {
            try
            {
                var user = UserService.Get(id);
                if (user == null)
                {
                    return NotFound(new { message = "Student not found." });
                }
                return Ok(user);
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { message = ex.Message });
            }
        }

        // POST: api/student/create
        [HttpPost("Create")]
        public IActionResult CreateUser([FromBody] UserDTO userDto)
        {
            try
            {
                if (userDto == null)
                {
                    return BadRequest(new { message = "Invalid user data." });
                }

                // Ensure Id is not set to prevent duplicate primary key issue
                userDto.Id = 0;

                var user = UserService.Create(userDto);
                return CreatedAtAction(nameof(GetStudentById), new { id = user.Id }, user);
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { message = ex.Message });
            }
        }
        // DELETE: api/student/delete/{id}
        [HttpDelete("Delete")]
        public IActionResult DeleteUser(int id)
        {
            try
            {
                if (id <= 0)
                {
                    return BadRequest(new { message = "Invalid student ID." });
                }

                var isDeleted = UserService.Delete(id);

                if (isDeleted)
                {
                    return Ok(new { message = "User deleted successfully." });
                }
                else
                {
                    return NotFound(new { message = "User not found." });
                }
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { message = ex.Message });
            }
        }

        // PUT: api/student/update
        [HttpPut("Update")]
        public IActionResult UpdateUser([FromBody] UserDTO userDto)
        {
            try
            {
                if (userDto == null || userDto.Id <= 0)
                {
                    return BadRequest(new { message = "Invalid user data." });
                }

                var updatedUser = UserService.Update(userDto);

                if (updatedUser != null)
                {
                    return Ok(new { message = "User updated successfully.", data = updatedUser });
                }
                else
                {
                    return NotFound(new { message = "User not found." });
                }
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { message = ex.Message });
            }
        }


        [HttpGet("Search-By-Name")]
        public IActionResult SearchByName([FromQuery] string name)
        {
            try
            {
                if (string.IsNullOrEmpty(name))
                {
                    return BadRequest(new { message = "User name cannot be null or empty." });
                }

                // Debug this line
                var users = UserService.SearchByName(name);

                if (users == null || !users.Any())
                {
                    return NotFound(new { message = "No user found matching the specified name." });
                }

                return Ok(users);
            }
            catch (Exception ex)
            {
                // Log exception for debugging
                Console.WriteLine(ex);
                return StatusCode(500, new { message = "An error occurred while searching for name.", error = ex.Message });
            }
        }


        [HttpGet("Display By Page")]
        public IActionResult DisplayByPage([FromQuery] int page, [FromQuery] int pageSize)
        {
            try
            {
                if (page <= 0 || pageSize <= 0)
                {
                    return BadRequest(new { message = "Invalid page or pageSize value." });
                }

                var users = UserService.DisplayByPage(page, pageSize);
                if (users == null || users.Count == 0)
                {
                    return NotFound(new { message = "No expenses found." });
                }

                return Ok(users);
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { message = ex.Message });
            }
        }



    }
} 