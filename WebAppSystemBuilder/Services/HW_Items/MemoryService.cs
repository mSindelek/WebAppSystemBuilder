using Microsoft.EntityFrameworkCore;
using WebAppSystemBuilder.DTO.Items.HWComponent.RAM;
using WebAppSystemBuilder.Models.Items.HWComponent.RAM;
using WebAppSystemBuilder.Models.Items.HWComponent.Shared;
using WebAppSystemBuilder.DTO.Items.HWComponent.Shared;

namespace WebAppSystemBuilder.Services.hw_items {
    public class MemoryService(AppDbContext dbContext) {

        private readonly AppDbContext _dbContext = dbContext;

        public IEnumerable<MemoryDTO> GetAll() {
            var allMemorys = _dbContext.HW_Memories
                .Include(ram => ram.RamType)
                .Include(ram => ram.ModuleType);
            var memoryDTOs = new List<MemoryDTO>();
            foreach (var memory in allMemorys) {
                memoryDTOs.Add(ModelToDto(memory));
            }
            return memoryDTOs;
        }

        internal async Task<MemoryDTO?> GetByIdAsync(int id) {
            var memoryModelToEdit = await _dbContext.HW_Memories
                .Include(ram => ram.RamType)
                .Include(ram => ram.ModuleType)
                .FirstAsync(ram => ram.Id == id);
            if (memoryModelToEdit == null) return null;
            return ModelToDto(memoryModelToEdit);
        }

        internal async Task CreateAsync(MemoryDTO newMemoryModel) {
            MemoryModel memoryToSave = await DtoToModelAsync(newMemoryModel);
            await _dbContext.AddAsync(memoryToSave);
            await _dbContext.SaveChangesAsync();
        }

        internal async Task UpdateAsync(MemoryDTO MemoryDTO) {
            _dbContext.Update(await DtoToModelAsync(MemoryDTO));
            await _dbContext.SaveChangesAsync();
        }

        internal async Task DeleteAsync(int id) {
            var memoryModelToDelete = await _dbContext.HW_Memories.FindAsync(id);
            if (memoryModelToDelete != null) {
                _dbContext.HW_Memories.Remove(memoryModelToDelete);
            }
            await _dbContext.SaveChangesAsync();
        }

        //conversions

        internal IEnumerable<RamTypeModel> GetRamTypeDropdownData() => _dbContext.HW_RamTypes.OrderBy(socket =>  socket.Name);
        internal IEnumerable<ModuleTypeModel> GetModuleTypeDropdownData() => _dbContext.HW_ModuleTypes.OrderBy(socket => socket.Name);


        private static MemoryDTO ModelToDto(MemoryModel memory) => new() {
            Id = memory.Id,
            Name = memory.Name,
            RamTypeName = memory.RamType.Name,
            RamTypeId = memory.RamType.Id,
            ModuleTypeName = memory.ModuleType.Name,
            ModuleTypeId = memory.ModuleType.Id,
            ECC = memory.ECC,
            ShortDesc = memory.ShortDesc,
            Description = memory.Description,
        };

        private async Task<MemoryModel> DtoToModelAsync(MemoryDTO newMemory) => new() {
            Id = newMemory.Id,
            Name = newMemory.Name,
            RamType = await _dbContext.HW_RamTypes.FindAsync(newMemory.RamTypeId) ?? throw new ArgumentNullException(newMemory.RamTypeId.ToString()), // TODO: better handling of possible null reference - id doesnt exist eg. due deletion from different endpoint
            ModuleType = await _dbContext.HW_ModuleTypes.FindAsync(newMemory.ModuleTypeId) ?? throw new ArgumentNullException(newMemory.RamTypeId.ToString()),
            ECC = newMemory.ECC,
            ShortDesc = newMemory.ShortDesc,
            Description = newMemory.Description,
        };

    }
}

