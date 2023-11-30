using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Ams.Data;
using Ams.Models;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
using Ams.View_Models;

namespace Ams.Controllers
{
    public class UserController : Controller
    {
        private readonly AppDbContext _context;

        public UserController(AppDbContext context)
        {
            _context = context;
        }

        // GET: User
        public async Task<IActionResult> Index()
        {
            return _context.users != null ?
                        View(await _context.users.ToListAsync()) :
                        Problem("Entity set 'AppDbContext.users'  is null.");
        }
        public IActionResult Create()
        {
            return View();
        }
        [HttpPost]
        public IActionResult Create(UserVm vm)
        {
            if (ModelState.IsValid)
            {
                if (vm.CPassword == vm.Password)
                {

                    var user = new User
                    {


                        Name = vm.Name,
                        Last_name = vm.Last_name,
                        Email = vm.Email,
                        Contact = vm.Contact,
                        address = vm.address,
                        UserName = vm.UserName,
                        Password = vm.Password
                    };
                    user.Password= BCrypt.Net.BCrypt.HashPassword(vm.Password);
                    _context.users.Add(user);
                    _context.SaveChanges();
                    return RedirectToAction("Index");
                }

               
                else
                {
                    throw new Exception("Password Doesn't match!!");
                }
                }
            
            return View(vm);

            
        }

        // GET: User/Details/5
    }
}
