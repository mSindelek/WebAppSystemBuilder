using WebAppSystemBuilder.DTO.Items.HWComponent.GPU;
using WebAppSystemBuilder.Models.Items.HWComponent.GPU;

namespace WebAppSystemBuilder.Services.hw_params {
    public class GraphicsBaseService(AppDbContext dbContext) {

        private readonly AppDbContext _dbContext = dbContext;

        public List<GraphicsBaseDTO> GetAll() {
            var allGpuBases = _dbContext.HW_GraphicsBases.ToList();
            var gpuBaseDTOs = new List<GraphicsBaseDTO>();
            foreach (var gpuBase in allGpuBases) {
                gpuBaseDTOs.Add(ModelToDto(gpuBase));
            }
            return gpuBaseDTOs;
        }

        internal async Task<GraphicsBaseDTO?> GetByIdAsync(int id) {
            var GraphicsBaseModelToEdit = await _dbContext.HW_GraphicsBases.FindAsync(id);
            if (GraphicsBaseModelToEdit == null) return null;
            return ModelToDto(GraphicsBaseModelToEdit);
        }

        internal async Task CreateAsync(GraphicsBaseDTO newGraphicsBaseModel) {
            GraphicsBaseModel GraphicsBaseToSave = DtoToModel(newGraphicsBaseModel);
            await _dbContext.AddAsync(GraphicsBaseToSave);
            await _dbContext.SaveChangesAsync();
        }

        internal async Task UpdateAsync(GraphicsBaseDTO GraphicsBaseDTO) {
            _dbContext.Update(DtoToModel(GraphicsBaseDTO));
            await _dbContext.SaveChangesAsync();
        }

        internal async Task DeleteAsync(int id) {
            var GraphicsBaseModelToDelete = await _dbContext.HW_GraphicsBases.FindAsync(id);
            if (GraphicsBaseModelToDelete != null) {
                _dbContext.HW_GraphicsBases.Remove(GraphicsBaseModelToDelete);
            }
            await _dbContext.SaveChangesAsync();
        }

        //conversions
        private static GraphicsBaseDTO ModelToDto(GraphicsBaseModel gpuBase) => new() {
            Id = gpuBase.Id,
            Name = gpuBase.Name,
            TDP = gpuBase.TDP,
            ShortDesc = gpuBase.ShortDesc,
        };

        private static GraphicsBaseModel DtoToModel(GraphicsBaseDTO newGpuBase) => new() {
            Id = newGpuBase.Id,
            Name = newGpuBase.Name,
            TDP = newGpuBase.TDP,
            ShortDesc = newGpuBase.ShortDesc,
        };

    }
}
