using Microsoft.EntityFrameworkCore;
using To_Do_List_API.Models;

namespace To_Do_List_API.Data
{
    public class ToDoListContext : DbContext
    {
        public ToDoListContext(DbContextOptions<ToDoListContext>options) : base(options) { }
        public DbSet<User> Users { get; set; }
        public DbSet<Loggs> Loggs { get; set; }
        public DbSet<ToDoListModel> ToDoLists { get; set; }
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlServer("Server=(local);Database=ToDoSQL;Trusted_Connection=True;MultipleActiveResultSets=True");
        }

    }
}
