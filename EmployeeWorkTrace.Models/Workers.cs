using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EmployeeWorkTrace.Models
{
    public class Workers
    {
        [Key]
        public int WorkerId { get; set; }
        public string WorkerName { get; set; }
        public int WorkerPassword { get; set; }
        public DateTime? CreateDate { get; set; }
        public DateTime? ModifiedDate { get; set; }
        public string? Title { get; set; }

    }
}
