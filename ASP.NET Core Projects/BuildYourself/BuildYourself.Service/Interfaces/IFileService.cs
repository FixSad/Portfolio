using BuildYourself.Domain.Enities;
using BuildYourself.Domain.Response;
using BuildYourself.Domain.ViewModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BuildYourself.Service.Interfaces
{
    public interface IFileService
    {
        Task<IBaseResponse<FileItem>> Create(FileViewModel model);

        Task<IEnumerable<FileItem>> GetFiles(string Name="");

        Task<IBaseResponse<bool>> ChangeFileStatus(string? filter);

        Task<IBaseResponse<bool>> UpdateFile(FileItem model);
    }
}
