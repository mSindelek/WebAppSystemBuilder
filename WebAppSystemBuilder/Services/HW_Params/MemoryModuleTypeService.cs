using WebAppSystemBuilder.DTO.Items.HWComponent.RAM;
using WebAppSystemBuilder.Models.Items.HWComponent.RAM;

namespace WebAppSystemBuilder.Services.hw_params {
    public class MemoryModuleTypeService(AppDbContext dbContext) {

        private readonly AppDbContext _dbContext = dbContext;

        public List<ModuleTypeDTO> GetAll() {
            var allModuleTypes = _dbContext.HW_ModuleTypes.ToList();
            var moduleTypeDTOs = new List<ModuleTypeDTO>();
            foreach (var moduleType in allModuleTypes) {
                moduleTypeDTOs.Add(ModelToDto(moduleType));
            }
            return moduleTypeDTOs;
        }

        internal async Task<ModuleTypeDTO?> GetByIdAsync(int id) {
            var moduleTypeModelToEdit = await _dbContext.HW_ModuleTypes.FindAsync(id);
            if (moduleTypeModelToEdit == null) return null;
            return ModelToDto(moduleTypeModelToEdit);
        }

        internal async Task CreateAsync(ModuleTypeDTO newModuleTypeModel) {
            ModuleTypeModel moduleTypeToSave = DtoToModel(newModuleTypeModel);
            await _dbContext.AddAsync(moduleTypeToSave);
            await _dbContext.SaveChangesAsync();
        }

        internal async Task UpdateAsync(ModuleTypeDTO ModuleTypeDTO) {
            _dbContext.Update(DtoToModel(ModuleTypeDTO));
            await _dbContext.SaveChangesAsync();
        }

        internal async Task DeleteAsync(int id) {
            var moduleTypeModelToDelete = await _dbContext.HW_ModuleTypes.FindAsync(id);
            if (moduleTypeModelToDelete != null) {
                _dbContext.HW_ModuleTypes.Remove(moduleTypeModelToDelete);
            }
            await _dbContext.SaveChangesAsync();
        }

        //conversions
        private static ModuleTypeDTO ModelToDto(ModuleTypeModel moduleType) => new() {
            Id = moduleType.Id,
            Name = moduleType.Name,
            ShortDesc = moduleType.ShortDesc,
        };

        private static ModuleTypeModel DtoToModel(ModuleTypeDTO newModuleType) => new() {
            Id = newModuleType.Id,
            Name = newModuleType.Name,
            ShortDesc = newModuleType.ShortDesc,
        };

    }
}

