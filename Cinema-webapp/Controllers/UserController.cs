using Cinema_webapp.Data;
using Microsoft.AspNetCore.Mvc;

namespace Cinema_webapp.Controllers
{
    public class UserController : Controller
    {
        private readonly ApplicationDbContext _db;

        public UserController(ApplicationDbContext db)
        {
            _db = db;
        }

        [HttpGet]
        public IActionResult Index()
        {
            var users = _db.Users.ToList();
            return View(users);
        }
    }
}
