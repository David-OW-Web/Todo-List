using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TodoList.Data.Models.Auth;

namespace TodoList.Data.Models
{
    public class TodoList
    {
        [Key]
        public int Id { get; set; }
        [Required(ErrorMessage = "{0} ist Pflichtfeld")]
        [StringLength(30)]
        public string? Name { get; set; }
        [Display(Name = "Erstellt am")]
        public DateTime CreatedAt { get; set; } = DateTime.Now;
        [Display(Name = "Aktualisiert am")]
        public DateTime UpdatedAt { get; set; } = DateTime.Now;
        [Display(Name = "Aktiv?")]
        public bool Active { get; set; } = true;
        [ForeignKey("User")]
        [Display(Name = "Erstellt von")]
        public string? UserId { get; set; }
        public ApplicationUser? User { get; set; }
        public ICollection<TodoListItem>? TodoListItems { get; set; }
    }
}
