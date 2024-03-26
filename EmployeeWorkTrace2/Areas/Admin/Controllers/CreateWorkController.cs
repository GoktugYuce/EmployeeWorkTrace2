using EmployeeWorkTrace.DataAccess.Data;
using EmployeeWorkTrace.DataAccess.Repository.IRepository;
using EmployeeWorkTrace.Models;
using EmployeeWorkTrace.Models.ViewModels;
using EmployeeWorkTrace.Utility;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace EmployeeWorkTrace2.Areas.Admin.Controllers
{
    [Area("Admin")]
    [Authorize(Roles = SD.Role_Admin)]
    public class CreateWorkController : Controller
    {
        public readonly IUnitOfWork _unitOfWork;
        private readonly IWebHostEnvironment _webHostEnvironment;
        public CreateWorkController(IUnitOfWork unitOfWork, IWebHostEnvironment webHostEnvironment)
        {
            _unitOfWork = unitOfWork;
            _webHostEnvironment = webHostEnvironment;
        }
        public IActionResult Create()
        {
            //List<Works> objWorksList = _unitOfWork.Works.GetAll().ToList();

            WorksVM worksVM = new()
            {
                WorkersList = _unitOfWork.Workers
                .GetAll().Select(u => new SelectListItem
                {
                    Text = u.WorkerName,
                    Value = u.UserId.ToString()
                }),
                Works = new Works()

            };
            return View(worksVM);
        }
        [HttpPost]
        public IActionResult Create(WorksVM obj)
        {
            foreach (var modelStateKey in ModelState.Keys.Where(k => k.StartsWith("Works.WorkItems")).ToList())
            {
                ModelState.Remove(modelStateKey);
            }
            if (ModelState.IsValid)
            {
                if (obj.Works.UserId != null)
                {
                    // Fetch the entire Worker object
                    var worker = _unitOfWork.Workers.GetFirstOrDefault(u => u.UserId == obj.Works.UserId);
                    if (worker != null)
                    {
                        obj.Works.WorkerName = worker.WorkerName;
                    }
                }
                if (obj.File != null)
                {
                    string uploadsFolder = Path.Combine(_webHostEnvironment.WebRootPath, "uploads");
                    string filePath = Path.Combine(uploadsFolder, obj.File.FileName);

                    using (var fileStream = new FileStream(filePath, FileMode.Create))
                    {
                        obj.File.CopyTo(fileStream);
                    }

                    var workItem = new WorkItem()
                    {
                        ItemName = obj.File.FileName,
                        CreationDate = DateTime.Now,
                        Work = obj.Works
                    };

                    obj.Works.WorkItems = new List<WorkItem> { workItem };
                }

                _unitOfWork.Works.Add(obj.Works);
                _unitOfWork.Save();
                TempData["success"] = "Work Created Successfully!";
                return RedirectToAction("Create");
            }
            return View(obj); // Changed from the default return
        }

    }
}
