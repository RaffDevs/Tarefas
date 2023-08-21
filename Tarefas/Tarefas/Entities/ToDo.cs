using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace Tarefas.Entities
{
    [Table("ToDo")]
    public class ToDo
    {
        [Key]
        public int Id { get; set; }

        public DateTime? Date { get; set; }

        public TimeSpan StartTime { get; set; }

        public TimeSpan EndTime { get; set; }

        public string Name { get; set; }

        public string Description { get; set; }

        public bool Done { get; set; }
    }
}
