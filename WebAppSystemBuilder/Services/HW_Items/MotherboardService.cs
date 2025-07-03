using Microsoft.EntityFrameworkCore;
using WebAppSystemBuilder.DTO.Items.HWComponent.Mobo;
using WebAppSystemBuilder.Models.Items.HWComponent.Mobo;

namespace WebAppSystemBuilder.Services.hw_items {
    public class MotherboardService(AppDbContext dbContext) {

        private readonly AppDbContext _dbContext = dbContext;

        public IEnumerable<MotherboardDTO> GetAll() {
            var allMotherboards = _dbContext.HW_Motherboards
                .Include(mobo => mobo.Chipset)
                .Include(mobo=>mobo.Chipset.Socket)
                .Include(mobo=>mobo.Chipset.RamType);
            var motherboardDTOs = new List<MotherboardDTO>();
            foreach (var motherboard in allMotherboards) {
                motherboardDTOs.Add(ModelToDto(motherboard));
            }
            return motherboardDTOs;
        }

        internal async Task<MotherboardDTO?> GetByIdAsync(int id) {
            var motherboardModelToEdit = await _dbContext.HW_Motherboards
                .Include(mb => mb.Chipset)
                .Include(mb => mb.Chipset.Socket)
                .Include(mb => mb.Chipset.RamType)
                .FirstAsync(mb => mb.Id == id);
            if (motherboardModelToEdit == null) return null;
            return ModelToDto(motherboardModelToEdit);
        }

        internal async Task CreateAsync(MotherboardDTO newMotherboardModel) {
            MotherboardModel motherboardToSave = await DtoToModelAsync(newMotherboardModel);
            await _dbContext.AddAsync(motherboardToSave);
            await _dbContext.SaveChangesAsync();
        }

        internal async Task UpdateAsync(MotherboardDTO MotherboardDTO) {
            _dbContext.Update(await DtoToModelAsync(MotherboardDTO));
            await _dbContext.SaveChangesAsync();
        }

        internal async Task DeleteAsync(int id) {
            var motherboardModelToDelete = await _dbContext.HW_Motherboards.FindAsync(id);
            if (motherboardModelToDelete != null) {
                _dbContext.HW_Motherboards.Remove(motherboardModelToDelete);
            }
            await _dbContext.SaveChangesAsync();
        }

        //conversions

        internal IEnumerable<ChipsetModel> GetChipsetDropdownData() => _dbContext.HW_Chipsets.OrderBy(socket =>  socket.Name);

        private static MotherboardDTO ModelToDto(MotherboardModel motherboard) => new() {
            Id = motherboard.Id,
            Name = motherboard.Name,
            ChipsetName = motherboard.Chipset.Name,
            ChipsetId = motherboard.Chipset.Id,
            SocketName = motherboard.Chipset.Socket.Name,
            ShortDesc = motherboard.Chipset.ShortDesc,
            Description = motherboard.Description,
        };

        private async Task<MotherboardModel> DtoToModelAsync(MotherboardDTO newMotherboard) => new() {
            Id = newMotherboard.Id,
            Name = newMotherboard.Name,
            Chipset = await _dbContext.HW_Chipsets.FindAsync(newMotherboard.ChipsetId) ?? throw new ArgumentNullException(newMotherboard.ChipsetId.ToString()), // TODO: better handling of possible null reference - id doesnt exist eg. due deletion from different endpoint
            Description = newMotherboard.Description,
        };

    }
}

