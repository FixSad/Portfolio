using BuildYourself.DAL;
using BuildYourself.Domain.ViewModel;
using BuildYourself.Models;
using BuildYourself.Service.Interfaces;
using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;

namespace BuildYourself.Controllers
{
    public class FileController : Controller
    {
        private IFileCategoryService _fileCategoryService;
        private IFileService _fileService;

        public FileController(IFileCategoryService fileCategoryService,
            IFileService fileService)
        {
            _fileCategoryService = fileCategoryService;
            _fileService = fileService;
        }

        public IActionResult Index()
        {
            return View();
        }

        public IActionResult Privacy()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> CreateCategory(FileCategoryViewModel obj)
        {
            var response = await _fileCategoryService.Create(obj);
            if (response.StatusCode == Domain.Enums.StatusCode.Success)
                return Ok(new { description = response.Description });
            return BadRequest(new { description = response.Description });
        }

        [HttpPost]
        public async Task<IActionResult> CreateFile(FileViewModel obj)
        {
            obj.StartDate = DateTime.Now;
            var response = await _fileService.Create(obj);
            if (response.StatusCode == Domain.Enums.StatusCode.Success)
                return Ok(new { description = response.Description });
            return BadRequest(new { description = response.Description });
        }
    }
}