using Microsoft.EntityFrameworkCore;
using pathLab.Domain.Entities;
using pathLab.Infrastructure.Repositories.IRepo;

namespace pathLab.Infrastructure.Repositories.Repo
{
    public class CbcTestRepository : ICbcTestRepository
    {
        private readonly ApplicationDbContext _context;

        public CbcTestRepository(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<CbcTest>> GetAllAsync()
        {
            return await _context.CbcTest.ToListAsync();
        }

        public async Task<CbcTest> GetByIdAsync(int id)
        {
            return await _context.CbcTest.FindAsync(id);
        }

        public async Task AddAsync(CbcTest test)
        {
            await _context.CbcTest.AddAsync(test);
            await _context.SaveChangesAsync();
        }

        public async Task UpdateAsync(CbcTest test)
        {
            _context.CbcTest.Update(test);
            await _context.SaveChangesAsync();
        }

        public async Task DeleteAsync(int id)
        {
            var test = await _context.CbcTest.FindAsync(id);
            if (test != null)
            {
                _context.CbcTest.Remove(test);
                await _context.SaveChangesAsync();
            }
        }
    }
}
