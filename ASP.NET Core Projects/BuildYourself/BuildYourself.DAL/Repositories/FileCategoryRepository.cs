using BuildYourself.DAL.Interfaces;
using BuildYourself.Domain.Enities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BuildYourself.DAL.Repositories
{
    public class FileCategoryRepository : IBaseRepository<FileCategory>
    {
        private ApplicationDBContext _dbContext;

        public FileCategoryRepository(ApplicationDBContext dBContext)
        {
            _dbContext = dBContext;
        }

        public async Task Create(FileCategory entity)
        {
            await _dbContext.FileCategories.AddAsync(entity);
            await _dbContext.SaveChangesAsync();
        }

        public async Task Delete(FileCategory entity)
        {
            _dbContext.FileCategories.Remove(entity);
            await _dbContext.SaveChangesAsync();
        }

        public IQueryable<FileCategory> GetAll()
        {
            return _dbContext.FileCategories;
        }

        public async Task Update(FileCategory entity)
        {
            _dbContext.FileCategories.Update(entity);
            await _dbContext.SaveChangesAsync();
        }
    }
}
