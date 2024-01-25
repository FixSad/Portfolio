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
    public interface IFileCategoryService
    {
        Task<IBaseResponse<FileCategory>> Create(FileCategoryViewModel model);
    }
}
