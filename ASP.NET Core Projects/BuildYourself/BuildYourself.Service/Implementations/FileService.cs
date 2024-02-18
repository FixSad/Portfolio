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

        public async Task<IBaseResponse<bool>> UpdateFile(FileItem model)
        {
            try
            {
                _logger.LogInformation($"Request to update the FileItem - {model.Name}");
                await _fileRepository.Update(model);
                _logger.LogInformation($"The FileItem was updated - {model.Name}");
                return new BaseResponse<bool>
                {
                    StatusCode = StatusCode.Success,
                    Description = $"The file's description has been saved"
                };
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, $"[FileService.UpdateFile]: {ex.Message}");
                return new BaseResponse<bool>
                {
                    StatusCode = StatusCode.InternalServerError,
                    Description = ex.Message
                };
            }
        }

        public async Task<IBaseResponse<bool>> ChangeFileStatus(string? filter)
        {
            try
            {
                var file = await _fileRepository.GetAll().Where(x => x.Name == filter).FirstOrDefaultAsync();
                _logger.LogInformation($"Request to change the FileItem status - {file.Name}");
                if (file.Status == FileStatus.Uncompleted)
                    file.Status = FileStatus.InProcess;
                else if (file.Status == FileStatus.InProcess)
                    file.Status = FileStatus.Completed;
                else
                    file.Status = FileStatus.Uncompleted;

                await _fileRepository.Update(file);
                _logger.LogInformation($"The file status was changed - {file.Name}");
                return new BaseResponse<bool>
                {
                    Description = "The file status was changed",
                    StatusCode = StatusCode.Success
                };
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, $"[FileService.ChangeFileStatus]: {ex.Message}");
                return new BaseResponse<bool>
                {
                    Description = ex.Message,
                    StatusCode = StatusCode.InternalServerError
                };
            }
        }

        public async Task<IBaseResponse<FileItem>> Create(FileViewModel model)
        {
            try
            {
                _logger.LogInformation($"Request to create the FileItem - {model.FileName}");
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

        public async Task<IEnumerable<FileItem>> GetFiles(string Name = "")
        {
            try
            { 
                _logger.LogInformation($"Request to get FileItems");
                var files = _fileRepository.GetAll();
                
                if(string.IsNullOrEmpty(Name) || string.IsNullOrWhiteSpace(Name))
                {
                    _logger.LogInformation($"Request to get FileItems is successful");

                    return files;
                }

                var file = files.Where(x => x.Name == Name);

                return file;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, $"[FileService.GetFiles]: {ex.Message}");
                return null;
            }
        }
    }
}