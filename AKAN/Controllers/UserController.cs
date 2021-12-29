using AKAN.Data;
using AKAN.Models;
using Microsoft.AspNetCore.Mvc;

namespace AKAN.Controllers
{
    public class UserController
    {
        private readonly ApplicationDbContext _db;

        public UserController(ApplicationDbContext db)
        {
            _db = db;
        }
    }
}
