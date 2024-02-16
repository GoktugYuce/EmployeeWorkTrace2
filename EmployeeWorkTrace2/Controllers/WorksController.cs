using EmployeeWorkTrace2.Data;
using EmployeeWorkTrace2.Models;
using Microsoft.AspNetCore.Mvc;

namespace EmployeeWorkTrace2.Controllers
{
    public class WorksController : Controller
    {
        public readonly DataContext _db;
        public WorksController(DataContext db) 
        {
            _db = db;
        }
        public IActionResult Works()
        {
            List<Works> objCategoryList = _db.Works.ToList();
            return View(objCategoryList);
        }

        public IActionResult View(int? id)
        {
            if(id == null || id == 0)
            {
                return NotFound();
            }
            Works worksFromDb = _db.Works.Find(id);
            if(worksFromDb == null)
            {
                return NotFound();
            }
            return View(worksFromDb);
        }

    }
}
