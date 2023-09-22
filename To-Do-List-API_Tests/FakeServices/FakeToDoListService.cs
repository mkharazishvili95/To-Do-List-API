using System;
using System.Collections.Generic;
using System.Linq;
using To_Do_List_API.Models;
using To_Do_List_API.Services;

namespace To_Do_List_API_Tests.FakeServices
{
    public class FakeToDoListService : IToDoListService
    {
        private List<ToDoListModel> _fakeToDoList;
        private List<ToDoListModel> fakeToDoList;

        public FakeToDoListService()
        {
            _fakeToDoList = new List<ToDoListModel>();
        }

        public FakeToDoListService(List<ToDoListModel> fakeToDoList)
        {
            _fakeToDoList = fakeToDoList ?? new List<ToDoListModel>();
        }

        public bool AddToDoList(ToDoListModel addToDoList, int userId)
        {
            _fakeToDoList.Add(addToDoList);
            return true;
        }

        public bool DeleteToDo(int todoId)
        {
            var todoItem = _fakeToDoList.Find(x => x.Id == todoId);

            if (todoItem == null)
            {
                return false;
            }
            else
            {
                _fakeToDoList.Remove(todoItem);
                return true;
            }
        }

        public IEnumerable<ToDoListModel> GetOwnList()
        {
            return _fakeToDoList;
        }
        public IEnumerable<ToDoListModel> SortByCategory(int userId, string listCategory)
        {
            var sortByListCategory = _fakeToDoList
                .Where(x => x.UserId == userId && x.Category.ToUpper() == listCategory.ToUpper());

            return sortByListCategory;
        }

        public IEnumerable<ToDoListModel> SortByCompleted(int userId, bool isCompleted)
        {
            var sortByCompleted = _fakeToDoList
                .Where(x => x.UserId == userId && x.IsCompleted == isCompleted);
            return sortByCompleted;
        }
        public bool UpdateToDoList(ToDoListModel updateToDo, int todoId)
        {
            if (_fakeToDoList == null)
            {
                throw new ArgumentNullException(nameof(_fakeToDoList), "The _fakeToDoList is null.");
            }

            var existingDoTo = _fakeToDoList.SingleOrDefault(x => x.Id == todoId);

            if (existingDoTo == null)
            {
                return false;
            }
            else
            {
                existingDoTo.Title = updateToDo.Title;
                existingDoTo.Description = updateToDo.Description;
                existingDoTo.FinalDate = updateToDo.FinalDate;
                existingDoTo.Category = updateToDo.Category;
                existingDoTo.IsCompleted = updateToDo.IsCompleted;
                return true;
            }
        }
        public bool UpdateCompletedStatus(int todoId, ToDoListModel updateListStatus)
        {
            var existingToDo = _fakeToDoList.SingleOrDefault(x => x.Id == todoId);
            if (existingToDo == null)
            {
                return false;
            }
            else
            {
                existingToDo.IsCompleted = updateListStatus.IsCompleted;
                return true;
            }
        }
    }
}
