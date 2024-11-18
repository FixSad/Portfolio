using BuildYourself.Domain.Enities;
using BuildYourself.Domain.Response;
using BuildYourself.Domain.ViewModel;

namespace BuildYourself.Service.Interfaces
{
    public interface IFileCategoryService
    {
        Task<IBaseResponse<FileCategory>> Create(FileCategoryViewModel model);

        Task<IEnumerable<FileCategory>> GetAll();
    }
}
