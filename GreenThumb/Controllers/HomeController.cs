using GreenThumb.Models;
using Microsoft.AspNetCore.Mvc;
using System.Text.RegularExpressions;

namespace HomePage.Controllers
{
	public class HomeController : Controller
	{
		[Route("/")]
		public IActionResult Index()
		{
			return View();
		}

		[Route("About/")]
		public IActionResult About()
		{
			return View();
		}
	}
}
