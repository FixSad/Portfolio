using BuildYourself.DAL.Interfaces;
using BuildYourself.Domain.Enities;
using BuildYourself.Domain.Response;
using BuildYourself.Domain.ViewModel;
using BuildYourself.Domain.Enums;
using BuildYourself.Domain.Extensions;
using BuildYourself.Service.Interfaces;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Runtime.CompilerServices;

namespace BuildYourself.Service.Implementations
{
    public class FileService : IFileService
    {
        private IBaseRepository<FileItem> _fileRepository;
        private IBaseRepository<FileCategory> _fileCategoryRepository;
        private ILogger<FileService> _logger;

        public FileService(IBaseRepository<FileItem> fileRepository,
            ILogger<FileService> logger,
            IBaseRepository<FileCategory> fileCategoryRepository)
        {
            _fileRepository = fileRepository;
            _logger = logger;
            _fileCategoryRepository = fileCategoryRepository;
        }

        public async Task<bool> ChangeFileStatus(string? filter)
        {
            var isSuccess = false;
            try
            {
                var file = await _fileRepository.GetAll().Where(x => x.Name == filter).FirstOrDefaultAsync();
                if (file.Status == FileStatus.Uncompleted)
                    file.Status = FileStatus.InProcess;
                else if (file.Status == FileStatus.InProcess)
                    file.Status = FileStatus.Completed;
                else
                    file.Status = FileStatus.Uncompleted;

                await _fileRepository.Update(file);
                isSuccess = true;
            }
            catch (Exception ex)
            {
                throw;
            }
            return isSuccess;
        }

        public async Task<IBaseResponse<FileItem>> Create(FileViewModel model)
        {
            try
            {
                _logger.LogInformation($"Request to create a FileItem - {model.FileName}");
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

                var category = await _fileCategoryRepository.GetAll()
                    .Where(x=> x.Name == model.FileCategory)
                    .FirstOrDefaultAsync();

                FileItem fileItem = new FileItem()
                {
                    Name = model.FileName,
                    Description = model.FileDescription,
                    StartDate = model.StartDate,
                    Category = category,
                    Status = model.FileStatus
                };

                await _fileRepository.Create(fileItem);
                _logger.LogInformation($"The FileItem was created - {model.FileName}");

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

        public async Task<IEnumerable<FileItem>> GetFiles()
        {
            try
            { 
                _logger.LogInformation($"Request to get FileItems");
                var files = _fileRepository.GetAll();
                
                _logger.LogInformation($"Request to get FileItems is successful");

                return files;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, $"[FileService.GetFiles]: {ex.Message}");
                return null;
            }
        }
    }
}
