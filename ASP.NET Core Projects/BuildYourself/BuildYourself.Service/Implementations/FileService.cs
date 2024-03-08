using BuildYourself.DAL.Interfaces;
using BuildYourself.Domain.Enities;
using BuildYourself.Domain.Enums;
using BuildYourself.Domain.Response;
using BuildYourself.Domain.ViewModel;
using BuildYourself.Service.Interfaces;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;

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

        public async Task<IBaseResponse<bool>> ChangeFileStatus(string FileName)
        {
            try
            {
                var file = await _fileRepository.GetAll()
                    .Where(x => x.Name == FileName).FirstOrDefaultAsync();
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
                    .Where(x => x.Name == model.FileCategory)
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

        public async Task<IBaseResponse<bool>> DeleteFile(string FileName)
        {
            try
            {
                _logger.LogInformation($"Request to delete FileItem - " + FileName);
                var file = await _fileRepository.GetAll()
                    .Where(x => x.Name.Equals(FileName))
                    .FirstOrDefaultAsync();

                if (file == null)
                {
                    return new BaseResponse<bool>
                    {
                        StatusCode = StatusCode.FileWasNotFound,
                        Description = $"{FileName} was not found"
                    };
                }

                await _fileRepository.Delete(file);
                _logger.LogInformation($"The FileItem was deleted - {FileName}");

                return new BaseResponse<bool>
                {
                    StatusCode = StatusCode.Success,
                    Description = $"The file was deleted successfully!"
                };
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, $"[FileService.DeleteFile]: {ex.Message}");
                return new BaseResponse<bool>
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

                if (string.IsNullOrEmpty(Name) || string.IsNullOrWhiteSpace(Name))
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

        public async Task<IBaseResponse<FileItem>> GetRandomFile(string[] filters)
        {
            try
            {
                Random random = new Random();
                _logger.LogInformation($"Request to get random FileItem");

                var files = await _fileRepository.GetAll()
                    .Where(x => x.Status == FileStatus.Uncompleted)
                    .Select(x => new FileViewModel()
                    {
                        FileName = x.Name,
                        FileDescription = x.Description,
                        FileCategory = x.Category.Name,
                        FileStatus = x.Status
                    }).ToListAsync();

                if (filters.Contains("All Categories") || filters.Length == 0)
                {
                    if (files.Count() > 0)
                    {
                        _logger.LogInformation($"The file was received!");

                        if (files.Count() == 1)
                        {
                            return new BaseResponse<FileItem>
                            {
                                Description = files[0].FileName,
                                StatusCode = StatusCode.Success
                            };
                        }
                        
                        return new BaseResponse<FileItem>
                        {
                            Description = files[random.Next(0, files.Count())].FileName,
                            StatusCode = StatusCode.Success
                        };
                    }
                }
                else if(filters.Length>0)
                {
                    List<FileViewModel> newFiles = new List<FileViewModel>();

                    foreach (var category in filters)
                    {
                        var tempFiles = files.Where(x => x.FileCategory == category).ToList();
                        newFiles.AddRange(tempFiles);
                    }
                    
                    if(newFiles.Count() > 0)
                    {
                        _logger.LogInformation($"The file was received!");

                        if (newFiles.Count() == 1)
                        {
                            return new BaseResponse<FileItem>
                            {
                                Description = newFiles[0].FileName,
                                StatusCode = StatusCode.Success
                            };
                        }
                        return new BaseResponse<FileItem>
                        {
                            Description = newFiles[random.Next(0, newFiles.Count())].FileName,
                            StatusCode = StatusCode.Success
                        };
                    }
                }

                _logger.LogInformation($"No Files with FileStatus = Uncompleted.");

                return new BaseResponse<FileItem>
                {
                    Description = "No Files!",
                    StatusCode = StatusCode.NoIncompleteFiles
                };
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, $"[FileService.GetRandomFile]: {ex.Message}");
                return new BaseResponse<FileItem>
                {
                    Description = ex.Message,
                    StatusCode = StatusCode.InternalServerError
                };
            }
        }
    }
}