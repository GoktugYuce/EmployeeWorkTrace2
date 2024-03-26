using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EmployeeWorkTrace.Models
{
    public class Admins
    {
        [Key]
        public string UserId { get; set; }
        public string AdminName { get; set; }
        public string? Title { get; set; }
        public DateTime? CreateDate { get; set; }
        public DateTime? ModifiedDate { get; set; }
    }

}
