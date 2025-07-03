using Microsoft.EntityFrameworkCore;
using WebAppSystemBuilder.DTO.Items.HWComponent.GPU;
using WebAppSystemBuilder.Models.Items.HWComponent.GPU;

namespace WebAppSystemBuilder.Services.hw_items {
    public class GraphicsCardService(AppDbContext dbContext) {

        private readonly AppDbContext _dbContext = dbContext;

        public IEnumerable<GraphicsCardDTO> GetAll() {
            var allGraphicsCards = _dbContext.HW_GraphicsCards
                .Include(gpu => gpu.GPUBaseModel);
            var graphicsCardDTOs = new List<GraphicsCardDTO>();
            foreach (var graphicsCard in allGraphicsCards) {
                graphicsCardDTOs.Add(ModelToDto(graphicsCard));
            }
            return graphicsCardDTOs;
        }

        internal async Task<GraphicsCardDTO?> GetByIdAsync(int id) {
            var graphicsCardModelToEdit = await _dbContext.HW_GraphicsCards
                .Include(gpu => gpu.GPUBaseModel)
                .FirstAsync(gpu => gpu.Id == id);
            if (graphicsCardModelToEdit == null) return null;
            return ModelToDto(graphicsCardModelToEdit);
        }

        internal async Task CreateAsync(GraphicsCardDTO newGraphicsCardModel) {
            GraphicsCardModel graphicsCardToSave = await DtoToModelAsync(newGraphicsCardModel);
            await _dbContext.AddAsync(graphicsCardToSave);
            await _dbContext.SaveChangesAsync();
        }

        internal async Task UpdateAsync(GraphicsCardDTO GraphicsCardDTO) {
            _dbContext.Update(await DtoToModelAsync(GraphicsCardDTO));
            await _dbContext.SaveChangesAsync();
        }

        internal async Task DeleteAsync(int id) {
            var graphicsCardModelToDelete = await _dbContext.HW_GraphicsCards.FindAsync(id);
            if (graphicsCardModelToDelete != null) {
                _dbContext.HW_GraphicsCards.Remove(graphicsCardModelToDelete);
            }
            await _dbContext.SaveChangesAsync();
        }

        //conversions

        internal IEnumerable<GraphicsBaseModel> GetGraphicsBaseDropdownData() => _dbContext.HW_GraphicsBases.OrderBy(socket =>  socket.Name);

        private static GraphicsCardDTO ModelToDto(GraphicsCardModel graphicsCard) => new() {
            Id = graphicsCard.Id,
            Name = graphicsCard.Name,
            BaseModelName = graphicsCard.GPUBaseModel.Name,
            GPUBaseModelId = graphicsCard.GPUBaseModel.Id,
            ShortDesc = graphicsCard.GPUBaseModel.ShortDesc,
            Description = graphicsCard.Description,
        };

        private async Task<GraphicsCardModel> DtoToModelAsync(GraphicsCardDTO newGraphicsCard) => new() {
            Id = newGraphicsCard.Id,
            Name = newGraphicsCard.Name,
            GPUBaseModel = await _dbContext.HW_GraphicsBases.FindAsync(newGraphicsCard.GPUBaseModelId) ?? throw new ArgumentNullException(newGraphicsCard.GPUBaseModelId.ToString()), // TODO: better handling of possible null reference - id doesnt exist eg. due deletion from different endpoint
            Description = newGraphicsCard.Description,
        };

    }
}

