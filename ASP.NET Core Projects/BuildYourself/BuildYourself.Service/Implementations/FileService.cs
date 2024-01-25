using BuildYourself.DAL.Interfaces;
using BuildYourself.Domain.Enities;
using BuildYourself.Domain.Response;
using BuildYourself.Domain.ViewModel;
using BuildYourself.Domain.Enums;
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
    public class FileService : IFileService
    {
        private IBaseRepository<FileItem> _fileRepository;
        private ILogger<FileService> _logger;

        public FileService(IBaseRepository<FileItem> fileRepository,
            ILogger<FileService> logger)
        {
            _fileRepository = fileRepository;
            _logger = logger;
        }

        public async Task<IBaseResponse<FileItem>> Create(FileViewModel model)
        {
            try
            {
                _logger.LogInformation($"request to create a FileItem - {model.FileName}");
                var file = await _fileRepository.GetAll()
                    .Where(x => x.Name == model.FileName)
                    .FirstOrDefaultAsync();

                if (file != null)
                {
                    return new BaseResponse<FileItem>
                    {
                        Description = $"File with that name already exists",
                        StatusCode = StatusCode.ItemIsHadAlready
                    };
                }

                FileItem fileItem = new FileItem()
                {
                    Name = model.FileName,
                    Description = model.FileDescription,
                    StartDate = model.StartDate,
                    Category = model.FileCategory
                };

                await _fileRepository.Create(fileItem);
                _logger.LogInformation($"the FileItem was created - {model.FileName}");

                return new BaseResponse<FileItem>
                {
                    StatusCode = StatusCode.Success,
                    Description = $"The file was created successfully!"
                };
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, $"[FileService.Create]: {ex.Message}");
                return new BaseResponse<FileItem>
                {
                    StatusCode = StatusCode.InternalServerError,
                    Description = ex.Message
                };
            }
        }
    }
}
