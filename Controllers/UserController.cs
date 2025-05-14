using Microsoft.AspNetCore.Mvc;
using Todo.Data;

namespace Todo.Controllers;

public class UserController(TodoDataContext _context) : Controller
{
    private readonly TodoDataContext context = _context;
    
    
    
}