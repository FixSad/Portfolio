using BuildYourself.DAL;
using BuildYourself.Domain.Enities;
using BuildYourself.Domain.ViewModel;
using BuildYourself.Models;
using BuildYourself.Service.Interfaces;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
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

        public async Task<IActionResult> Index()
        {
            var categories = await _fileCategoryService.GetAll();

            ViewBag.Categories = new SelectList(categories, "Id", "Name");

            return View(await _fileService.GetFiles());
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
            obj.FileStatus = Domain.Enums.FileStatus.Uncompleted;
            var response = await _fileService.Create(obj);
            if (response.StatusCode == Domain.Enums.StatusCode.Success)
                return Ok(new { description = response.Description });
            return BadRequest(new { description = response.Description });
        }

        [HttpPost]
        public async Task<IActionResult> ChangeFileStatus(string TestName)
        {
            await _fileService.ChangeFileStatus(TestName);
            
            return RedirectToAction("Index");
        }

    }
}