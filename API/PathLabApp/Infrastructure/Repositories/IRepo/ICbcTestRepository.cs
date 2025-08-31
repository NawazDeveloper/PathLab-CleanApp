using pathLab.Domain.Entities;

namespace pathLab.Infrastructure.Repositories.IRepo
{
    public interface ICbcTestRepository
    {
        Task<IEnumerable<CbcTest>> GetAllAsync();
        Task<CbcTest> GetByIdAsync(int id);
        Task AddAsync(CbcTest test);
        Task UpdateAsync(CbcTest test);
        Task DeleteAsync(int id);
    }
}
