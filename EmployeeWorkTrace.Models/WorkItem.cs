using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations.Schema;

namespace EmployeeWorkTrace.Models
{
    [Table("WorkItems")]
    public class WorkItem
    {
        [Key]
        public int WorkItemId { get; set; }

        [Required]
        [MaxLength(100)]
        [DisplayName("Item Name")]
        public string ItemName { get; set; }

        public DateTime CreationDate { get; set; }

        public int WorkId { get; set; } // Foreign Key to the Works table
        public Works Work { get; set; } // Navigation Property
    }

}
