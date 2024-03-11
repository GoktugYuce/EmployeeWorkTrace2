using EmployeeWorkTrace.DataAccess.Data;
using EmployeeWorkTrace.DataAccess.Repository.IRepository;
using EmployeeWorkTrace.Models;
using EmployeeWorkTrace.Models.ViewModels;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;

namespace EmployeeWorkTrace2.Areas.Worker.Controllers
{
    [Area("Worker")]
    public class WorksController : Controller
    {
        public readonly IUnitOfWork _unitOfWork;
        private readonly IWebHostEnvironment _webHostEnvironment;
        public WorksController(IUnitOfWork unitOfWork, IWebHostEnvironment webHostEnvironment)
        {
            _unitOfWork = unitOfWork;
            _webHostEnvironment = webHostEnvironment;
        }
        public IActionResult Works()
        {
            List<Works> objWorksList = _unitOfWork.Works.GetAll().ToList();
            return View(objWorksList);
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

            // Fetch the Work and include WorkItems
            var workFromDb = _unitOfWork.Works.GetFirstOrDefault(u => u.WorkId == id, includeProperties: "WorkItems");

            if (workFromDb == null)
            {
                return NotFound();
            }

            // Create the WorksVM
            WorksVM worksVM = new()
            {
                Works = workFromDb,
                WorkersList = _unitOfWork.Workers.GetAll().Select(i => new SelectListItem
                {
                    Text = i.WorkerName,
                    Value = i.WorkerId.ToString()
                }),
                WorkItems = workFromDb.WorkItems ?? new List<WorkItem>() // Initialize WorkItems
            };

            return View("~/Areas/Worker/Views/Works/Edit.cshtml", worksVM);
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

                if (Request.Form.Files.Any())
                {
                    var workItems = new List<WorkItem>();
                    for (int i = 0; i < Request.Form.Files.Count; i++)
                    {
                        var file = Request.Form.Files[i];
                        if (file.Name.Contains("Works.WorkItems"))
                        {
                            string uploadsFolder = Path.Combine(_webHostEnvironment.WebRootPath, "uploads");
                            string filePath = Path.Combine(uploadsFolder, file.FileName);

                            using (var fileStream = new FileStream(filePath, FileMode.Create))
                            {
                                file.CopyTo(fileStream);
                            }

                            // ... (Set other WorkItem properties, potentially from form fields) ...

                            workItems.Add(new WorkItem
                            {
                                ItemName = file.FileName, // Or extract from form if needed
                                CreationDate = DateTime.Now,
                                Work = obj.Works // Associate the WorkItem with the Work
                            });
                        }
                    }
                    obj.Works.WorkItems = workItems; // Add to ViewModel
                }

                _unitOfWork.Works.Update(obj.Works);
                _unitOfWork.Save();
                TempData["success"] = "Work Updated Successfully!";
                return RedirectToAction("Works"); // Redirect to your list view
            }
            return View(obj);
        }

        [HttpPost]
        public IActionResult UpdateWorkState(int workId, string newState)
        {
            var work = _unitOfWork.Works.GetFirstOrDefault(w => w.WorkId == workId);
            if (work == null)
            {
                return NotFound();
            }
            // Update the WorkState (Parse newState from string)
            if (!Enum.TryParse(newState, out WorkState workStateValue))
            {
                return BadRequest("Invalid work state provided."); // Handle invalid input
            }
            work.WorkState = workStateValue;
            _unitOfWork.Works.Update(work);
            _unitOfWork.Save();

            return Ok();
        }

        [HttpGet]
        public IActionResult GetAll() 
        {
            List<Works> objWorksList = _unitOfWork.Works.GetAll().ToList();
            return Json(new { data = objWorksList });
        }

    }
}
