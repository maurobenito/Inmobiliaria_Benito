
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

[Authorize] // 🔐 Esta línea hace que requiera login
public class HomeController : Controller
{
    public IActionResult Index()
    {
        return View();
    }
}
