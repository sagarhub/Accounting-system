using Ams.Manager.Interfaces;
using Ams.ViewModels;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Ams.Data;


namespace Ams.Controllers;

[AllowAnonymous]
public class LoginController : Controller
{
    private readonly AppDbContext _context;
    private readonly IAuthManager _authManager;

    public LoginController(AppDbContext context, IAuthManager authManager)
    {
        _context = context;
        _authManager = authManager;
    }

    public IActionResult Login()
    {
        var vm = new LoginVm();
        return View(vm);
    }

    [HttpPost]
    public async Task<IActionResult> Login(LoginVm vm)
    {
        if (!ModelState.IsValid)
        {
            return View(vm);
        }

        try
        {
            await _authManager.Login(vm.UserName, vm.Password);
            return RedirectToAction("Index", "Home");
        }
        catch (Exception)
        {
            return View(vm);
        }
    }

    public async Task<IActionResult> Logout()
    {
        await _authManager.Logout();
        return RedirectToAction("Login");
    }
}