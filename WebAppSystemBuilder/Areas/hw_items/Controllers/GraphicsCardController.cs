using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using WebAppSystemBuilder.DTO.Items.HWComponent.GPU;
using WebAppSystemBuilder.Services.hw_items;

namespace WebAppSystemBuilder.Areas.hw_items.Controllers {
    [Authorize]
    [Area("hw_items")]
    //[Route("Hardware/Items/[controller]/[action]")]
    public class GraphicsCardController(GraphicsCardService graphicsCardService) : Controller {
        internal readonly GraphicsCardService _graphicsCardService = graphicsCardService;
        private void FillDropdowns() {
            ViewBag.GraphicsBase = new SelectList(_graphicsCardService.GetGraphicsBaseDropdownData(), "Id", "Name");
        }

        [AllowAnonymous]
        [HttpGet]
        public IActionResult Index() {
            var allGraphicsCards = _graphicsCardService.GetAll();
            return View(allGraphicsCards);
        }

        [HttpGet]
        public IActionResult Create() {
            FillDropdowns();
            return View();
        }
        [Authorize(Roles = "Editor")]
        [HttpPost]
        public async Task<IActionResult> CreateAsync(GraphicsCardDTO newGraphicsCard) {
            await _graphicsCardService.CreateAsync(newGraphicsCard);
            return RedirectToAction("Index");
        }
        [HttpGet]
        public async Task<IActionResult> EditAsync(int id) {
            var graphicsCardToEdit = await _graphicsCardService.GetByIdAsync(id);
            if (graphicsCardToEdit == null) {
                return View("NotFound");
            }
            FillDropdowns();
            return View(graphicsCardToEdit);
        }

        [Authorize(Roles = "Editor")]
        [HttpPost]
        public async Task<IActionResult> EditAsync(GraphicsCardDTO graphicsCardDTO) {
            await _graphicsCardService.UpdateAsync(graphicsCardDTO);
            return RedirectToAction("Index");
        }

        [Authorize(Roles = "Admin")]
        [HttpPost]
        public async Task<IActionResult> DeleteAsync(int id) {
            await _graphicsCardService.DeleteAsync(id);
            return RedirectToAction("Index");
        }

        //public IActionResult Search(string q) {
        //    var foundGraphicsCards = _graphicsCardService.GetByName(q);
        //    return View("Index", foundGraphicsCards);
        //}

        [HttpGet]
        public async Task<IActionResult> GetToDeleteAsync(int id) {
            var graphicsCardDetails = await _graphicsCardService.GetByIdAsync(id);
            return View(graphicsCardDetails);
        }

    }
}

