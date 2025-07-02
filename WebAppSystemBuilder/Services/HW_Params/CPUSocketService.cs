using WebAppSystemBuilder.DTO.Items.HWComponent.Shared;
using WebAppSystemBuilder.Models.Items.HWComponent.Shared;

namespace WebAppSystemBuilder.Services.hw_params {
    public class CPUSocketService(AppDbContext dbContext) {

        private readonly AppDbContext _dbContext = dbContext;

        public List<CPUSocketDTO> GetAll() {
            var allSockets = _dbContext.HW_CPUSockets.ToList();
            var socketDTOs = new List<CPUSocketDTO>();
            foreach (var socket in allSockets) {
                socketDTOs.Add(ModelToDto(socket));
            }
            return socketDTOs;
        }

        internal async Task<CPUSocketDTO?> GetByIdAsync(int id) {
            var cpuSocketModelToEdit = await _dbContext.HW_CPUSockets.FindAsync(id);
            if (cpuSocketModelToEdit == null) return null;
            return ModelToDto(cpuSocketModelToEdit);
        }

        internal async Task CreateAsync(CPUSocketDTO newCPUSocketModel) {
            CPUSocketModel cpuSocketToSave = DtoToModel(newCPUSocketModel);
            await _dbContext.AddAsync(cpuSocketToSave);
            await _dbContext.SaveChangesAsync();
        }

        internal async Task UpdateAsync(CPUSocketDTO CPUSocketDTO) {
            _dbContext.Update(DtoToModel(CPUSocketDTO));
            await _dbContext.SaveChangesAsync();
        }

        internal async Task DeleteAsync(int id) {
            var CPUSocketModelToDelete = await _dbContext.HW_CPUSockets.FindAsync(id);
            if (CPUSocketModelToDelete != null) {
                _dbContext.HW_CPUSockets.Remove(CPUSocketModelToDelete);
            }
            await _dbContext.SaveChangesAsync();
        }

        //conversions
        private static CPUSocketDTO ModelToDto(CPUSocketModel socket) => new() {
            Id = socket.Id,
            Name = socket.Name,
            ShortDesc = socket.ShortDesc,
        };

        private static CPUSocketModel DtoToModel(CPUSocketDTO newSocket) => new() {
            Id = newSocket.Id,
            Name = newSocket.Name,
            ShortDesc = newSocket.ShortDesc,
        };

    }
}

