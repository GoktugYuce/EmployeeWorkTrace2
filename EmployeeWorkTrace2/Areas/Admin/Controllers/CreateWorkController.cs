using EmployeeWorkTrace.DataAccess.Data;
using EmployeeWorkTrace.DataAccess.Repository.IRepository;
using EmployeeWorkTrace.Models;
using EmployeeWorkTrace.Models.ViewModels;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace EmployeeWorkTrace2.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class CreateWorkController : Controller
    {
        public readonly IUnitOfWork _unitOfWork;
        public CreateWorkController(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
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
                    Value = u.WorkerId.ToString()
                }),
                Works = new Works()

            };
            return View(worksVM);
        }
        [HttpPost]
        public IActionResult Create(WorksVM obj)
        {
            if (ModelState.IsValid)
            {
                if (obj.Works.WorkerId > 0)
                {
                    // Fetch the entire Worker object
                    var worker = _unitOfWork.Workers.GetFirstOrDefault(u => u.WorkerId == obj.Works.WorkerId);
                    if (worker != null)
                    {
                        obj.Works.WorkerName = worker.WorkerName;
                    }
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
