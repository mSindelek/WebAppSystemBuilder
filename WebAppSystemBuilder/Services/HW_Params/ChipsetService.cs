using Microsoft.EntityFrameworkCore;
using WebAppSystemBuilder.DTO.Items.HWComponent.Mobo;
using WebAppSystemBuilder.Models.Items.HWComponent.Mobo;
using WebAppSystemBuilder.Models.Items.HWComponent.Shared;

namespace WebAppSystemBuilder.Services.hw_params {
    public class ChipsetService(AppDbContext dbContext) {

        private readonly AppDbContext _dbContext = dbContext;

        public IEnumerable<ChipsetDTO> GetAll() {
            var allChipsets = _dbContext.HW_Chipsets
                .Include(chp => chp.Socket)
                .Include(chp => chp.RamType);
            var chipsetDTOs = new List<ChipsetDTO>();
            foreach (var chipset in allChipsets) {
                chipsetDTOs.Add(ModelToDto(chipset));
            }
            return chipsetDTOs;
        }

        internal async Task<ChipsetDTO?> GetByIdAsync(int id) {
            var chipsetModelToEdit = await _dbContext.HW_Chipsets
                .Include(chp => chp.Socket)
                .Include(chp => chp.RamType)
                .FirstAsync(chp =>chp.Id ==id);
            if (chipsetModelToEdit == null) return null;
            return ModelToDto(chipsetModelToEdit);
        }

        internal async Task CreateAsync(ChipsetDTO newChipsetModel) {
            ChipsetModel chipsetToSave = await DtoToModelAsync(newChipsetModel);
            await _dbContext.AddAsync(chipsetToSave);
            await _dbContext.SaveChangesAsync();
        }

        internal async Task UpdateAsync(ChipsetDTO ChipsetDTO) {
            _dbContext.Update(await DtoToModelAsync(ChipsetDTO));
            await _dbContext.SaveChangesAsync();
        }

        internal async Task DeleteAsync(int id) {
            var chipsetModelToDelete = await _dbContext.HW_Chipsets.FindAsync(id);
            if (chipsetModelToDelete != null) {
                _dbContext.HW_Chipsets.Remove(chipsetModelToDelete);
            }
            await _dbContext.SaveChangesAsync();
        }

        //conversions
        public IEnumerable<RamTypeModel> GetDdrDropdownData() => _dbContext.HW_RamTypes.OrderBy(x => x.Name);
        public IEnumerable<CPUSocketModel> GetSocketDropdownData() => _dbContext.HW_CPUSockets.OrderBy(x => x.Name);

        private static ChipsetDTO ModelToDto(ChipsetModel chipset) => new() {
            Id = chipset.Id,
            Name = chipset.Name,
            SocketName = chipset.Socket.Name,
            SocketId = chipset.Socket.Id,
            RamTypeName = chipset.RamType.Name,
            RamTypeId = chipset.RamType.Id,
            ShortDesc = chipset.RamType.ShortDesc,
        };

        private async Task<ChipsetModel> DtoToModelAsync(ChipsetDTO newChipset) => new() {
            Id = newChipset.Id,
            Name = newChipset.Name,
            Socket = await _dbContext.HW_CPUSockets.FindAsync(newChipset.SocketId) ?? throw new ArgumentNullException(newChipset.SocketId.ToString()), // TODO: better handling of possible null reference - id doesnt exist eg. due deletion from different endpoint
            RamType = await _dbContext.HW_RamTypes.FindAsync(newChipset.RamTypeId) ?? throw new ArgumentNullException(newChipset.RamTypeId.ToString()),
            ShortDesc = newChipset.ShortDesc,
        };

    }
}

