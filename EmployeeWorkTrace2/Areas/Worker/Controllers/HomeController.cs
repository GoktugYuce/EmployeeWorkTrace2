using EmployeeWorkTrace.Models;
using EmployeeWorkTrace.DataAccess.Data;
using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;
using EmployeeWorkTrace.DataAccess.Repository.IRepository;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

namespace EmployeeWorkTrace2.Areas.Worker.Controllers
{
    [Area("Worker")]
    [Authorize]
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly IUnitOfWork _unitOfWork;
        private readonly UserManager<IdentityUser> _userManager;
        private readonly DataContext _db;

        public HomeController(ILogger<HomeController> logger, IUnitOfWork unitOfWork, DataContext db, UserManager<IdentityUser> userManager)
        {
            _logger = logger;
            _unitOfWork = unitOfWork;
            _userManager = userManager;
            _db = db;
        }

        public IActionResult Index()
        {
            IEnumerable<Workers> workersList = _unitOfWork.Workers.GetAll();
            return View(workersList);
        }

        public async Task<IActionResult> MyWorksAsync()
        {
            var userId = _userManager.GetUserId(User); // Get the logged-in user's ID

            var myWorks = await _db.Works
                                .Where(w => w.UserId == userId)
                                .ToListAsync();

            return View(myWorks);
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
