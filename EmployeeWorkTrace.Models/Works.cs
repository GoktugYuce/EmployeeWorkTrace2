using Microsoft.AspNetCore.Mvc.ModelBinding.Validation;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace EmployeeWorkTrace.Models
{
    public class Works
    {
        [Key]
        public int WorkId { get; set; }
        [Required]
        [MaxLength(30)]
        [DisplayName("Work Name")]
        public string WorkName { get; set; }
        [Required]
        [MaxLength(30)]
        [DisplayName("Worker Name")]
        [ValidateNever]
        public string WorkerName { get; set; }
        public DateTime? WorkCreationDate { get; set; }
        public DateTime? WorkEndDate { get; set; }
        public Works()
        {
            WorkCreationDate = DateTime.Now;
        }

        public int WorkerId { get; set; }
        [ForeignKey("WorkerId")]
        [ValidateNever]
        public Workers Workers { get; set; }
        public ICollection<WorkItem> WorkItems { get; set; }
    }
}
