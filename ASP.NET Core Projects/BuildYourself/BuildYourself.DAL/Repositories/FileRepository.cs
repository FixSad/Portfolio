using BuildYourself.DAL.Interfaces;
using BuildYourself.Domain.Enities;

namespace BuildYourself.DAL.Repositories
{
    public class FileRepository : IBaseRepository<FileItem>
    {
        private ApplicationDBContext _dbContext;

        public FileRepository(ApplicationDBContext dBContext)
        {
            _dbContext = dBContext;
        }

        public async Task Create(FileItem entity)
        {
            _dbContext.Files.Add(entity);
            await _dbContext.SaveChangesAsync();
        }

        public async Task Delete(FileItem entity)
        {
            _dbContext.Files.Remove(entity);
            await _dbContext.SaveChangesAsync();
        }

        public IQueryable<FileItem> GetAll()
        {
            return _dbContext.Files;
        }

        public async Task Update(FileItem entity)
        {
            _dbContext.Files.Update(entity);
            await _dbContext.SaveChangesAsync();
        }
    }
}
