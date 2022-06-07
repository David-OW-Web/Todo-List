using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TodoList.Data.Models
{
    public class TodoListItem
    {
        [Key]
        public int Id { get; set; }
        [Required(ErrorMessage = "{0} ist Pflichtfeld")]
        [StringLength(100)]
        [Display(Name = "Titel/Kurzbeschreibung")]
        public string? Title { get; set; }
        [Display(Name = "Erstellt am")]
        public DateTime CreatedAt { get; set; } = DateTime.Now;
        [Display(Name = "Erledigt?")]
        public bool Done { get; set; }
        [ForeignKey("TodoList")]
        [Display(Name = "Todo-Liste")]
        public int? TodoListId { get; set; }
        public TodoList? TodoList { get; set; }
    }
}
