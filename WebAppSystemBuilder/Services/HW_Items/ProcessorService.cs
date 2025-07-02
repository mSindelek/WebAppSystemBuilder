using Microsoft.EntityFrameworkCore;
using WebAppSystemBuilder.DTO.Items.HWComponent.CPU;
using WebAppSystemBuilder.Models.Items.HWComponent.CPU;
using WebAppSystemBuilder.Models.Items.HWComponent.Shared;

namespace WebAppSystemBuilder.Services.hw_items {
    public class ProcessorService(AppDbContext dbContext) {

        private readonly AppDbContext _dbContext = dbContext;


        public IEnumerable<ProcessorDTO> GetAll() {
            var allProcessors = _dbContext.HW_Processors.Include(cpu => cpu.Socket);
            var processorDTOs = new List<ProcessorDTO>();
            foreach (var processor in allProcessors) {
                processorDTOs.Add(ModelToDto(processor));
            }
            return processorDTOs;
        }

        internal async Task<ProcessorDTO?> GetByIdAsync(int id) {
            var processorModelToEdit = await _dbContext.HW_Processors.FindAsync(id);
            if (processorModelToEdit == null) return null;
            return ModelToDto(processorModelToEdit);
        }

        internal async Task CreateAsync(ProcessorDTO newProcessorModel) {
            ProcessorModel processorToSave = await DtoToModelAsync(newProcessorModel);
            await _dbContext.AddAsync(processorToSave);
            await _dbContext.SaveChangesAsync();
        }

        internal async Task UpdateAsync(ProcessorDTO ProcessorDTO) {
            _dbContext.Update(await DtoToModelAsync(ProcessorDTO));
            await _dbContext.SaveChangesAsync();
        }

        internal async Task DeleteAsync(int id) {
            var processorModelToDelete = await _dbContext.HW_Processors.FindAsync(id);
            if (processorModelToDelete != null) {
                _dbContext.HW_Processors.Remove(processorModelToDelete);
            }
            await _dbContext.SaveChangesAsync();
        }

        //conversions

        internal IEnumerable<CPUSocketModel> GetSocketDropdownData() => _dbContext.HW_CPUSockets.OrderBy(socket =>  socket.Name);

        private static ProcessorDTO ModelToDto(ProcessorModel processor) => new() {
            Id = processor.Id,
            Name = processor.Name,
            SocketName = processor.Socket.Name,
            SocketId = processor.Socket.Id,
            TDP = processor.TDP,
        };

        private async Task<ProcessorModel> DtoToModelAsync(ProcessorDTO newProcessor) => new() {
            Id = newProcessor.Id,
            Name = newProcessor.Name,
            TDP = newProcessor.TDP,
            Socket = await _dbContext.HW_CPUSockets.FindAsync(newProcessor.SocketId) ?? throw new ArgumentNullException(newProcessor.SocketId.ToString()), // TODO: better handling of possible null reference - id doesnt exist eg. due deletion from different endpoint
        };

    }
}

