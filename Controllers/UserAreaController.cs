using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Todo.Controllers;

[Authorize]
public class UserAreaController : Controller
{
    public IActionResult Index()
    {
        return View("_LoginPartial");
    }

}