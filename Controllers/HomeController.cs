using System.Diagnostics;
using System.Security.Claims;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Project.Models;

namespace Project.Controllers;

public class HomeController : Controller
{
    private readonly ILogger<HomeController> _logger;

    public HomeController(ILogger<HomeController> logger)
    {
        _logger = logger;
    }

    [Authorize]
    [Authorize(Roles = "admin, user")]
    public IActionResult Index()
    {
        string role = User.FindFirst(x => x.Type == ClaimsIdentity.DefaultRoleClaimType).Value;
        return Content($"ваша роль: {role}");
    }
    [Authorize(Roles = "admin")]
    public IActionResult About()
    {
        return Content("Вход только для администратора");
    }

    public IActionResult Privacy()
    {
        return View();
    }

    [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
    public IActionResult Error()
    {
        return View(new ErrorViewModel {RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier});
    }
}