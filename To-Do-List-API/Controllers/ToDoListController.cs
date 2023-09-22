using FluentValidation;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using To_Do_List_API.Data;
using To_Do_List_API.Models;
using To_Do_List_API.Services;
using To_Do_List_API.Validation;

namespace To_Do_List_API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ToDoListController : ControllerBase
    {
        private readonly ToDoListContext _context;
        private readonly IToDoListService _service;
        public ToDoListController(IToDoListService service, ToDoListContext context)
        {
            _context = context;
            _service = service;
        }
        [Authorize]
        [HttpPost("Add-To-Do-List")]
        public IActionResult AddToDoList([FromBody] ToDoListModel addToDoList)
        {
            var userId = int.Parse(User.Identity.Name);
            var validator = new ToDoListValidator();
            var validatorResult = validator.Validate(addToDoList);

            if (!validatorResult.IsValid)
            {
                return BadRequest(validatorResult.Errors);
            }
            else
            {
                _service.AddToDoList(addToDoList, userId);
                return Ok(new { Message = "To-Do-List has successfully created!" });
            }
        }
        [Authorize]
        [HttpGet("GetOwn-To-Do-List")]
        public IEnumerable<ToDoListModel> GetOwnList()
        {
            var userId = int.Parse(User.Identity.Name);
            var ownList = _context.Users
                                .Include(x => x.ToDoLists)
                                .SingleOrDefault(x => x.Id == userId);

            if (ownList != null)
            {
                return ownList.ToDoLists;
            }
            else
            {
                return new List<ToDoListModel>();
            }
        }

        [Authorize]
        [HttpGet("SortListByCategory")]
        public IEnumerable<ToDoListModel> SortByCategory(string category)
        {
            var userId = int.Parse(User.Identity.Name);
            var sortedList = _service.SortByCategory(userId, category);
            return sortedList;
        }
        [Authorize]
        [HttpGet("SortByCompleted")]
        public IEnumerable<ToDoListModel> SortByCompleted(bool completed)
        {
            var userId = int.Parse(User.Identity.Name);
            var sortedByCompleted = _service.SortByCompleted(userId, completed);
            return sortedByCompleted;
        }
        [Authorize]
        [HttpPut("UpdateCompletedStatus")]
        public IActionResult UpdateCompletedStatus(int todoId, ToDoListModel updateStatus)
        {
            var userId = int.Parse(User.Identity.Name);
            var existingToDo = _context.ToDoLists.SingleOrDefault(x => x.Id == todoId);
            if(existingToDo == null)
            {
                return BadRequest("There is no any To-Do to update!");
            }
            if(userId != existingToDo.UserId)
            {
                return BadRequest("That is not your to-do!");
            }
            else
            {
                    _service.UpdateCompletedStatus(todoId, updateStatus);
                    return Ok("To-Do status has successfully updated!");
            }
        }
        [Authorize]
        [HttpPut("UpdateToDoList")]
        public IActionResult UpdateToDoList(ToDoListModel updateTodo, int todoId)
        {
            var userId = int.Parse(User.Identity.Name);
            var existingToDo = _context.ToDoLists.SingleOrDefault(x => x.Id == todoId);

            if (existingToDo == null)
            {
                return BadRequest("There is no any To-Do to update!");
            }

            if (userId != existingToDo.UserId)
            {
                return BadRequest("That is not your to-do!");
            }
            else
            {
                var todoValidator = new ToDoListValidator();
                var validatorResult = todoValidator.Validate(updateTodo);

                if (!validatorResult.IsValid)
                {
                    return BadRequest(validatorResult.Errors);
                }
                else
                {
                    _service.UpdateToDoList(updateTodo, todoId);
                    return Ok("ToDo has successfully updated!");
                }
            }
        }
        [Authorize]
        [HttpDelete("DeleteToDo")]
        public IActionResult DeleteToDo(int todoId)
        {
            var userId = int.Parse(User.Identity.Name);

            var ownToDo = _context.ToDoLists.SingleOrDefault(x => x.Id == todoId && x.UserId == userId);

            if (ownToDo == null)
            {
                return NotFound("The specified ToDo item was not found!");
            }
            else
            {
                var success = _service.DeleteToDo(todoId);
                if (success)
                {
                    return Ok("ToDo has been successfully deleted!");
                }
                else
                {
                    return BadRequest("Failed to delete ToDo!");
                }
            }
        }
    }
}
