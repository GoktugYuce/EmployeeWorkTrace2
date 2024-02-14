using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace EmployeeWorkTrace2.Models
{
    public class Works
    {
        [Key]
        public int WorkId { get; set; }
        [Required]
        [MaxLength(30)]
        [DisplayName("Work Name")]
        public string WorkName { get; set; }
        public string Assignee { get; set; }
        public DateTime WorkCreationDate { get; set; }
        public DateTime WorkEndDate { get; set;}

        public Works()
        {
            WorkCreationDate = DateTime.Now;
        }
    }
}
