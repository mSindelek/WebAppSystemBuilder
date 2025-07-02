using WebAppSystemBuilder.DTO.Items.HWComponent.Shared;
using WebAppSystemBuilder.Models.Items.HWComponent.Shared;

namespace WebAppSystemBuilder.Services.hw_params {
    public class RamTypeService(AppDbContext dbContext) {

        private readonly AppDbContext _dbContext = dbContext;

        public List<RamTypeDTO> GetAll() {
            var allRamTypes = _dbContext.HW_RamTypes.ToList();
            var ramTypeDTOs = new List<RamTypeDTO>();
            foreach (var ramType in allRamTypes) {
                ramTypeDTOs.Add(ModelToDto(ramType));
            }
            return ramTypeDTOs;
        }

        internal async Task<RamTypeDTO?> GetByIdAsync(int id) {
            var RamTypeModelToEdit = await _dbContext.HW_RamTypes.FindAsync(id);
            if (RamTypeModelToEdit == null) return null;
            return ModelToDto(RamTypeModelToEdit);
        }

        internal async Task CreateAsync(RamTypeDTO newRamTypeModel) {
            RamTypeModel ramTypeToSave = DtoToModel(newRamTypeModel);
            await _dbContext.AddAsync(ramTypeToSave);
            await _dbContext.SaveChangesAsync();
        }

        internal async Task UpdateAsync(RamTypeDTO RamTypeDTO) {
            _dbContext.Update(DtoToModel(RamTypeDTO));
            await _dbContext.SaveChangesAsync();
        }

        internal async Task DeleteAsync(int id) {
            var RamTypeModelToDelete = await _dbContext.HW_RamTypes.FindAsync(id);
            if (RamTypeModelToDelete != null) {
                _dbContext.HW_RamTypes.Remove(RamTypeModelToDelete);
            }
            await _dbContext.SaveChangesAsync();
        }

        //conversions
        private static RamTypeDTO ModelToDto(RamTypeModel ramType) => new() {
            Id = ramType.Id,
            Name = ramType.Name,
            ShortDesc = ramType.ShortDesc,
        };

        private static RamTypeModel DtoToModel(RamTypeDTO newRamType) => new() {
            Id = newRamType.Id,
            Name = newRamType.Name,
            ShortDesc = newRamType.ShortDesc,
        };

    }
}

