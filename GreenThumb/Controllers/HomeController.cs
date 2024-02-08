using GreenThumb.Models;
using Microsoft.AspNetCore.Mvc;
using System.Text.RegularExpressions;

namespace HomePage.Controllers
{
	public class HomeController : Controller
	{
		public IActionResult Index()
		{
			return View();
		}
	}
}
