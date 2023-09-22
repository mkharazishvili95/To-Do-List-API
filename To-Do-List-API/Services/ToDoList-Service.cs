using FluentValidation;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using To_Do_List_API.Data;
using To_Do_List_API.Models;
using To_Do_List_API.Validation;
using Tweetinvi.Core.Models;

namespace To_Do_List_API.Services
{
    public interface IToDoListService
    {
        bool AddToDoList(ToDoListModel addToDoList, int userId);
        IEnumerable<ToDoListModel> GetOwnList();
        bool DeleteToDo(int todoId);
        bool UpdateToDoList(ToDoListModel updateToDo, int todoId);
        IEnumerable<ToDoListModel> SortByCategory(int userId, string listCategory);
        IEnumerable<ToDoListModel> SortByCompleted(int userId, bool isCompleted);
        bool UpdateCompletedStatus(int todoId, ToDoListModel updateListStatus);
    }
    public class ToDoListService : IToDoListService
    {
        private readonly ToDoListContext _context;
        public ToDoListService(ToDoListContext context)
        {
            _context = context;
        }

        public bool AddToDoList(ToDoListModel addToDoList, int userId)
        {
            var validator = new ToDoListValidator();
            var validatorResult = validator.Validate(addToDoList);

            if (!validatorResult.IsValid)
            {
                return false;
            }
            else
            {
                addToDoList.UserId = userId;

                _context.Add(addToDoList);
                _context.SaveChanges();
                return true;
            }
        }

        public bool DeleteToDo(int todoId)
        {
            var todoItem = _context.ToDoLists.SingleOrDefault(x => x.Id == todoId);

            if (todoItem == null)
            {
                return false;
            }
            else
            {
                _context.ToDoLists.Remove(todoItem);
                _context.SaveChanges();
                return true;
            }
        }
        
        public IEnumerable<ToDoListModel> GetOwnList()
        {
            var getAllList = _context.ToDoLists.ToList();
            return getAllList;
        }

        public IEnumerable<ToDoListModel> SortByCategory(int userId, string listCategory)
        {
            var sortByListCategory = _context.ToDoLists
                .Where(x => x.UserId == userId && x.Category.ToUpper() == listCategory.ToUpper());

            return sortByListCategory;
        }

        public IEnumerable<ToDoListModel> SortByCompleted(int userId, bool isCompleted)
        {
            var sortByCompleted = _context.ToDoLists
                .Where(x => x.UserId == userId && x.IsCompleted == isCompleted);
            return sortByCompleted;
        }
        public bool UpdateToDoList(ToDoListModel updateToDo, int todoId)
        {
            var existingToDo = _context.ToDoLists.SingleOrDefault(x => x.Id == todoId);
            if(existingToDo == null)
            {
                return false;
            }
            else
            {
                    existingToDo.Title = updateToDo.Title;
                    existingToDo.Description = updateToDo.Description;
                    existingToDo.FinalDate = updateToDo.FinalDate;
                    existingToDo.Category = updateToDo.Category;
                    existingToDo.IsCompleted = updateToDo.IsCompleted;
                    _context.Update(existingToDo);
                    _context.SaveChanges();
                    return true;
            }
        }
        public bool UpdateCompletedStatus(int todoId, ToDoListModel updateListStatus)
        {
            var existingToDo = _context.ToDoLists.SingleOrDefault(x => x.Id == todoId);
            if( existingToDo == null)
            {
                return false;
            }
            else
            {
                existingToDo.IsCompleted = updateListStatus.IsCompleted;
                _context.Update(existingToDo);
                _context.SaveChanges();
                return true;
            }
        }
    }
}
    
