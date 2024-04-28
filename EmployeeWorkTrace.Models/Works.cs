using Microsoft.AspNetCore.Mvc.ModelBinding.Validation;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace EmployeeWorkTrace.Models
{
    public enum WorkState
    {
        //[Display(Name = "Start Work")]
        StartWork,

        //[Display(Name = "Under Development")]
        UnderDevelopment,

        //[Display(Name = "Code Review")]
        CodeReview,

        Completed
    }
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
        public string? Description { get; set; }

        public string UserId { get; set; }
        [ForeignKey("UserId")]
        [ValidateNever]
        public Workers Workers { get; set; }
        public ICollection<WorkItem> WorkItems { get; set; }
        public WorkState WorkState { get; set; } = WorkState.StartWork; // Default to 'StartWork'
        public ICollection<DiscussionMessage>? DiscussionMessages { get; set; }
    }
}
