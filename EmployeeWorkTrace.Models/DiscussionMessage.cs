using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EmployeeWorkTrace.Models
{
    public class DiscussionMessage
    {
        [Key]
        public int MessageId { get; set; }
        public int WorkId { get; set; }
        public string SenderId { get; set; }
        public string MessageText { get; set; }
        public DateTime CreationDate { get; set; }

        public DiscussionMessage()
        {
            CreationDate = DateTime.Now;
        }

        // Navigation Properties
        public ApplicationUser Sender { get; set; }
        public Works Work { get; set; }
    }
}
