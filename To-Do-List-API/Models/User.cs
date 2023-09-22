using System.Collections.Generic;

namespace To_Do_List_API.Models
{
    public class User
    {
        public int Id { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string UserName { get; set; }
        public string Password { get; set; }
        public string Role { get; set; }
        public List<ToDoListModel> ToDoLists { get; set; }

        public User()
        {
            Role = "User";
        }
    }
}
