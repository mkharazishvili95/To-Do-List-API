using System;

namespace To_Do_List_API.Models
{
    public class ToDoListModel
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
        public DateTime FinalDate { get; set; }
        public bool IsCompleted { get; set; }
        public string Category { get; set; }
        public int UserId { get; set; }
        public User User { get; set; }
        public enum ToDoListModelType
        {
            Work,
            Home,
            Personal,
            Education,
            Travel,
            Other
        }
        public ToDoListModel()
        {
            IsCompleted = false;
        }
    }
}
