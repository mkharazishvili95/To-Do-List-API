using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using To_Do_List_API.Models;
using To_Do_List_API.Services;
using To_Do_List_API_Tests.FakeServices;

namespace To_Do_List_API_Tests
{
    public class ToDoListServiceTests
    {
        [Test]
        public void AddToDoList_Returns_True()
        {
            var fakeService = new FakeToDoListService();
            var addToDoList = new ToDoListModel { Id = 1, Title = "Test Task", Description = "Test Description" };
            var userId = 1;
            var result = fakeService.AddToDoList(addToDoList, userId);
            Assert.IsTrue(result);
        }

        [Test]
        public void DeleteToDo_Returns_True()
        {
            var fakeService = new FakeToDoListService();
            var todoId = 1;
            var addToDoList = new ToDoListModel { Id = todoId, Title = "Test Task", Description = "Test Description" };
            fakeService.AddToDoList(addToDoList, userId: 1);
            var result = fakeService.DeleteToDo(todoId);
            Assert.IsTrue(result);
        }

        [Test]
        public void GetOwnList_Returns_CorrectCount()
        {
            var fakeService = new FakeToDoListService();
            var addToDoList1 = new ToDoListModel { Id = 1, Title = "Test Task 1", Description = "Test Description 1" };
            var addToDoList2 = new ToDoListModel { Id = 2, Title = "Test Task 2", Description = "Test Description 2" };
            fakeService.AddToDoList(addToDoList1, userId: 1);
            fakeService.AddToDoList(addToDoList2, userId: 1);
            var result = fakeService.GetOwnList();
            Assert.AreEqual(2, result.Count());
        }
        [Test]
        public void SortByCategory_Returns_CorrectCount()
        {
            var fakeService = new FakeToDoListService();
            var addToDoList1 = new ToDoListModel { Id = 1, Title = "Test Task 1", Description = "Test Description 1", UserId = 1, Category = "Work" };
            var addToDoList2 = new ToDoListModel { Id = 2, Title = "Test Task 2", Description = "Test Description 2", UserId = 1, Category = "Home" };
            fakeService.AddToDoList(addToDoList1, userId: 1);
            fakeService.AddToDoList(addToDoList2, userId: 1);
            var result = fakeService.SortByCategory(userId: 1, listCategory: "Work");
            Assert.AreEqual(1, result.Count());
        }

        [Test]
        public void SortByCompleted_Returns_CorrectCount()
        {
            var fakeService = new FakeToDoListService();
            var addToDoList1 = new ToDoListModel { Id = 1, Title = "Test Task 1", Description = "Test Description 1", UserId = 1, IsCompleted = true };
            var addToDoList2 = new ToDoListModel { Id = 2, Title = "Test Task 2", Description = "Test Description 2", UserId = 1, IsCompleted = false };
            fakeService.AddToDoList(addToDoList1, userId: 1);
            fakeService.AddToDoList(addToDoList2, userId: 1);
            var result = fakeService.SortByCompleted(userId: 1, isCompleted: true);
            Assert.AreEqual(1, result.Count());
        }
        [Test]
        public void UpdateToDoList_WhenToDoExists_ShouldReturnTrue()
        {
            var fakeToDoList = new List<ToDoListModel>
    {
        new ToDoListModel { Id = 1, Title = "Sample Task", Description = "Sample Description" }
    };

            var fakeService = new FakeToDoListService(fakeToDoList);
            var toDoIdToUpdate = 1;
            var updateToDo = new ToDoListModel
            {
                Id = 1,
                Title = "Updated Task",
                Description = "Updated Description",
                FinalDate = DateTime.Now.AddDays(1),
                Category = "Work",
                IsCompleted = true
            };
            var result = fakeService.UpdateToDoList(updateToDo, toDoIdToUpdate);
            Assert.IsTrue(result);
        }
        [Test]
        public void UpdateCompletedStatus_WhenToDoExists_ShouldReturnTrue()
        {
            var fakeToDoList = new List<ToDoListModel>
    {
        new ToDoListModel { Id = 1, Title = "Sample Task", Description = "Sample Description", IsCompleted = false }
    };

            var fakeService = new FakeToDoListService(fakeToDoList);

            var toDoIdToUpdate = 1;
            var updateToDo = new ToDoListModel
            {
                IsCompleted = true
            };
            var result = fakeService.UpdateCompletedStatus(toDoIdToUpdate, updateToDo);
            Assert.IsTrue(result);
            var updatedToDo = fakeService.GetOwnList().SingleOrDefault(x => x.Id == toDoIdToUpdate);
            Assert.NotNull(updatedToDo);
            Assert.IsTrue(updatedToDo.IsCompleted);
        }

    }
}
