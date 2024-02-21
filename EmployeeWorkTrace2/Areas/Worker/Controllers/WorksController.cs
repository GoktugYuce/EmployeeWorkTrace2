using EmployeeWorkTrace.DataAccess.Data;
using EmployeeWorkTrace.DataAccess.Repository.IRepository;
using EmployeeWorkTrace.Models;
using Microsoft.AspNetCore.Mvc;

namespace EmployeeWorkTrace2.Areas.Worker.Controllers
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
            Works worksFromDb = _unitOfWork.Works.Get(u => u.WorkId == id);
            if (worksFromDb == null)
            {
                return NotFound();
            }
            return View(worksFromDb);
        }

    }
}
