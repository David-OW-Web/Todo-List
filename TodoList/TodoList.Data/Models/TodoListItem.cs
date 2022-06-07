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
        [Required]
        [StringLength(100)]
        public string? Title { get; set; }
        public DateTime CreatedAt { get; set; } = DateTime.Now;
        public bool Done { get; set; }
        [ForeignKey("TodoList")]
        public int? TodoListId { get; set; }
        public TodoList? TodoList { get; set; }
    }
}
