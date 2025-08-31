using pathLab.Application.DTOs.RequestDto;
using pathLab.Application.DTOs;
using pathLab.Domain.Entities;

namespace pathLab.Application.Interfaces
{
    public interface ICbcService
    {
        Task<IEnumerable<CbcTest>> GetAllAsync();
        Task<CbcTest> GetByIdAsync(int id);
        Task AddAsync(CbcTestDto dto);
        Task UpdateAsync(int id, CbcTestDto dto);
        Task DeleteAsync(int id);
    }
}
