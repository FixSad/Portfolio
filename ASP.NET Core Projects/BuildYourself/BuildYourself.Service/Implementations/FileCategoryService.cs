using BuildYourself.DAL.Interfaces;
using BuildYourself.Domain.Enities;
using BuildYourself.Domain.Enums;
using BuildYourself.Domain.Response;
using BuildYourself.Domain.ViewModel;
using BuildYourself.Service.Interfaces;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BuildYourself.Service.Implementations
{
    public class FileCategoryService : IFileCategoryService
    {
        private IBaseRepository<FileCategory> _fileCategoryRepository;
        private ILogger<FileCategoryService> _logger;

        public FileCategoryService(IBaseRepository<FileCategory> fileCategoryRepository,
            ILogger<FileCategoryService> logger)
        {
            _fileCategoryRepository = fileCategoryRepository;
            _logger = logger;
        }

        public async Task<IBaseResponse<FileCategory>> Create(FileCategoryViewModel model)
        {
            try
            {
                _logger.LogInformation($"request to create a FileCategory - {model.CategoryName}");
                var fileCategory = await _fileCategoryRepository.GetAll()
                    .Where(x => x.Name == model.CategoryName).FirstOrDefaultAsync();

                if(fileCategory != null) 
                {
                    return new BaseResponse<FileCategory>()
                    {
                        Description = $"Сategory with that name already exists",
                        StatusCode = StatusCode.CategoryIsHadAlready
                    };
                }
                fileCategory = new FileCategory()
                {
                    Name = model.CategoryName,
                    Description = model.CategoryDescription,
                    IsEntertainment = model.CategoryIsEntertainment
                };

                await _fileCategoryRepository.Create(fileCategory);

                _logger.LogInformation($"the category was created - {model.CategoryName}");

                return new BaseResponse<FileCategory>()
                {
                    StatusCode = StatusCode.Success,
                    Description = $"The FileCategory was created successfully!"
                };
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, $"[FileCategoryService.Create]: {ex.Message}");
                return new BaseResponse<FileCategory>()
                {
                    StatusCode = StatusCode.InternalServerError,
                    Description = ex.Message
                };
            }
        }
    }
}
