using BuildYourself.Domain.Enities;
using BuildYourself.Domain.Response;
using BuildYourself.Domain.ViewModel;

namespace BuildYourself.Service.Interfaces
{
    public interface IFileService
    {
        Task<IBaseResponse<FileItem>> Create(FileViewModel model);

        Task<IEnumerable<FileItem>> GetFiles(string Name="");

        Task<IBaseResponse<bool>> ChangeFileStatus(string FileName);

        Task<IBaseResponse<bool>> UpdateFile(FileItem model);

        Task<IBaseResponse<bool>> DeleteFile(string FileName);

        Task<IBaseResponse<FileItem>> GetRandomFile(string[] filters);
    }
}
