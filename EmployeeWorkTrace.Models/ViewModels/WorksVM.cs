using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc.ModelBinding.Validation;
using Microsoft.AspNetCore.Mvc.Rendering;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EmployeeWorkTrace.Models.ViewModels
{
    public class WorksVM
    {
        public Works Works { get; set; }
        [ValidateNever]
        public IEnumerable<SelectListItem> WorkersList { get; set; }
        public IFormFile? File { get; set; }
    }
}
