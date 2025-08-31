using pathLab.Application.DTOs;
using pathLab.Application.Interfaces;
using pathLab.Domain.Entities;
using pathLab.Infrastructure.Repositories.IRepo;

namespace pathLab.Application.Service
{
    public class CbcService : ICbcService
    {
        private readonly ICbcTestRepository _repository;

        public CbcService(ICbcTestRepository repository)
        {
            _repository = repository;
        }

        public async Task<IEnumerable<CbcTest>> GetAllAsync()
        {
            return await _repository.GetAllAsync();
        }

        public async Task<CbcTest> GetByIdAsync(int id)
        {
            return await _repository.GetByIdAsync(id);
        }

        public async Task AddAsync(CbcTestDto dto)
        {
            var entity = new CbcTest
            {
                PatientId = dto.PatientId,
                Hemoglobin = dto.Hemoglobin,
                WBC = dto.WBC,
                RBC = dto.RBC,
                Platelets = dto.Platelets,
                Hematocrit = dto.Hematocrit,
                Gender = dto.Gender,

                MCV = dto.MCV,
                MCH = dto.MCH,
                MCHC = dto.MCHC
            };

            await _repository.AddAsync(entity);
        }

        public async Task UpdateAsync(int id, CbcTestDto dto)
        {
            var entity = await _repository.GetByIdAsync(id);
            if (entity != null)
            {
                entity.Hemoglobin = dto.Hemoglobin;
                entity.WBC = dto.WBC;
                entity.RBC = dto.RBC;
                entity.Platelets = dto.Platelets;
                entity.Hematocrit = dto.Hematocrit;
                entity.MCV = dto.MCV;
                entity.MCH = dto.MCH;
                entity.MCHC = dto.MCHC;

                await _repository.UpdateAsync(entity);
            }
        }

        public async Task DeleteAsync(int id)
        {
            await _repository.DeleteAsync(id);
        }
    }
}

