using BuildYourself.DAL;
using BuildYourself.Models;
using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;

namespace BuildYourself.Controllers
{
    public class FileController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }

        public IActionResult Privacy()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> CreateCategory(FileCategoryModel obj)
        {
            return Ok();
        }

        [HttpPost]
        public async Task<IActionResult> CreateFile(FileModel obj)
        {
            obj.StartDate = DateOnly.FromDateTime(DateTime.Now);
            return Ok();
        }
    }
}