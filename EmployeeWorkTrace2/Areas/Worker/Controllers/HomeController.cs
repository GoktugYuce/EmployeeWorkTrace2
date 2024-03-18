using EmployeeWorkTrace.Models;
using EmployeeWorkTrace.DataAccess.Data;
using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;
using EmployeeWorkTrace.DataAccess.Repository.IRepository;
using Microsoft.AspNetCore.Authorization;

namespace EmployeeWorkTrace2.Areas.Worker.Controllers
{
    [Area("Worker")]
    [Authorize]
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly IUnitOfWork _unitOfWork;

        public HomeController(ILogger<HomeController> logger, IUnitOfWork unitOfWork)
        {
            _logger = logger;
            _unitOfWork = unitOfWork;
        }

        public IActionResult Index()
        {
            IEnumerable<Workers> workersList = _unitOfWork.Workers.GetAll();
            return View(workersList);
        }

        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
