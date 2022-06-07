using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TodoList.Data.Models;
using TodoList.Data.Models.Auth;

namespace TodoList.Data
{
    public class TodoListDbContext : IdentityDbContext<ApplicationUser>
    {
        public TodoListDbContext(DbContextOptions<TodoListDbContext> options) : base(options)
        {

        }

        // DB-Set Properties für Projekt

        public DbSet<TodoList.Data.Models.TodoList> TodoLists { get; set; }
        public DbSet<TodoListItem> TodoListItems { get; set; }

        // Konfiguration. Tabellen zu singular-Namen

        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);

            // Tabellen umbenennen
            builder.Entity<TodoList.Data.Models.TodoList>().ToTable("TodoList");
            builder.Entity<TodoListItem>().ToTable("TodoListItem");
        }
    }
}
