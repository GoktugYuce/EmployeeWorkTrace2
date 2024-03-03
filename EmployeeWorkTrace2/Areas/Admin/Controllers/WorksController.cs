using EmployeeWorkTrace.DataAccess.Data;
using EmployeeWorkTrace.DataAccess.Repository.IRepository;
using EmployeeWorkTrace.Models;
using EmployeeWorkTrace.Models.ViewModels;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore; 

namespace EmployeeWorkTrace2.Areas.Admin.Controllers
{
    [Area("Worker")]
    public class WorksController : Controller
    {
        public readonly IUnitOfWork _unitOfWork;
        public WorksController(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }
        public IActionResult Works()
        {
            List<Works> objCategoryList = _unitOfWork.Works.GetAll().ToList();
            return View(objCategoryList);
        }

        public IActionResult View(int? id)
        {
            if (id == null || id == 0)
            {
                return NotFound();
            }
            Works worksFromDb = _unitOfWork.Works
                                            .GetAll("WorkItems")
                                            .FirstOrDefault(u => u.WorkId == id);


            if (worksFromDb == null)
            {
                return NotFound();
            }
            return View(worksFromDb);
        }

        [HttpGet]
        public IActionResult Edit(int? id)
        {
            if (id == null || id == 0)
            {
                return NotFound();
            }

            var workFromDb = _unitOfWork.Works.GetFirstOrDefault(u => u.WorkId == id);
            if (workFromDb == null)
            {
                return NotFound();
            }

            WorksVM worksVM = new()
            {
                Works = workFromDb,
                WorkersList = _unitOfWork.Workers.GetAll().Select(i => new SelectListItem
                {
                    Text = i.WorkerName,
                    Value = i.WorkerId.ToString()
                })
            };

            return View("~/Areas/Admin/Views/Works/Edit.cshtml", worksVM);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Edit(WorksVM obj)
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
                _unitOfWork.Works.Update(obj.Works);
                _unitOfWork.Save();
                TempData["success"] = "Work Updated Successfully!";
                return RedirectToAction("Works"); // Redirect to your list view
            }
            return View(obj);
        }

    }
}
