using EmployeeWorkTrace2.Data;
using EmployeeWorkTrace2.Models;
using Microsoft.AspNetCore.Mvc;

namespace EmployeeWorkTrace2.Controllers
{
    public class CreateWorkController : Controller
    {
        public readonly DataContext _db;
        public CreateWorkController(DataContext db)
        {
            _db = db;
        }
        public IActionResult Create()
        {
            return View();
        }
        [HttpPost]
        public IActionResult Create(Works obj)
        {
            if(ModelState.IsValid)
            {
                _db.Works.Add(obj);
                _db.SaveChanges();
                return RedirectToAction("Create");
            }
            else
            {
                return View();
            }
            
        }
    }
}
