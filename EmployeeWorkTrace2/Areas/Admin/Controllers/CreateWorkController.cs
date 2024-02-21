using EmployeeWorkTrace.DataAccess.Data;
using EmployeeWorkTrace.DataAccess.Repository.IRepository;
using EmployeeWorkTrace.Models;
using Microsoft.AspNetCore.Mvc;

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
            return View();
        }
        [HttpPost]
        public IActionResult Create(Works obj)
        {
            if (ModelState.IsValid)
            {
                _unitOfWork.Works.Add(obj);
                _unitOfWork.Save();
                TempData["success"] = "Work Created Successfully!";
                return RedirectToAction("Create");
            }
            else
            {
                return View();
            }

        }
    }
}
