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

        public IActionResult FileInfo()
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
        public async Task<IActionResult> ChangeFileStatus(string FileName)
        {
            var response = await _fileService.ChangeFileStatus(FileName);

            if (response.StatusCode == Domain.Enums.StatusCode.Success)
                return Ok(new { description = response.Description });
            return BadRequest(new { description = response.Description });
        }

        [HttpPost]
        public async Task<IActionResult> GenerateFile(string[] filters)
        {
            var response = await _fileService.GetRandomFile(filters);

            if (response.StatusCode == Domain.Enums.StatusCode.Success)
                return Ok(new { description = response.Description });
            return BadRequest(new { description = response.Description });
        }

        [HttpPost]
        public async Task<IActionResult> UpdateFileDescription(string FileName, string FileDescription)
        {
            var files = await _fileService.GetFiles(FileName);
            var file = files.FirstOrDefault();

            if(file != null)
            {
                if (file.Description == FileDescription)
                    return BadRequest(new { description = "The description has not changed!" });

                file.Description = FileDescription;
                var response = await _fileService.UpdateFile(file);
                
                if (response.StatusCode == Domain.Enums.StatusCode.Success)
                    return Ok(new {description = response.Description});
                return BadRequest(new {description = response.Description});
            }
            return BadRequest();
        }
    }
}