using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using WebAppSystemBuilder.DTO.Items.HWComponent.Shared;
using WebAppSystemBuilder.Services.hw_params;

namespace WebAppSystemBuilder.Areas.hw_params.Controllers {
    [Authorize]
    [Area("hw_params")]
    //[Route("Hardware/Parameters/[controller]/[action]")]
    public class RamTypeController(RamTypeService ramTypeService) : Controller {
        internal readonly RamTypeService _ramTypeService = ramTypeService;

        [AllowAnonymous]
        [HttpGet]
        public IActionResult Index() {
            var allDdrTypes = _ramTypeService.GetAll();
            return View(allDdrTypes);
        }

        [HttpGet]
        public IActionResult Create() {
            return View();
        }

        [Authorize(Roles = "Editor")]
        [HttpPost]
        public async Task<IActionResult> CreateAsync(RamTypeDTO newDdrType) {
            await _ramTypeService.CreateAsync(newDdrType);
            return RedirectToAction("Index");
        }

        [HttpGet]
        public async Task<IActionResult> EditAsync(int id) {
            var ramTypeToEdit = await _ramTypeService.GetByIdAsync(id);
            if (ramTypeToEdit == null) {
                return View("NotFound");
            }
            return View(ramTypeToEdit);
        }

        [Authorize(Roles = "Editor")]
        [HttpPost]
        public async Task<IActionResult> EditAsync(RamTypeDTO ramTypeDTO) {
            await _ramTypeService.UpdateAsync(ramTypeDTO);
            return RedirectToAction("Index");
        }

        [Authorize(Roles = "Admin")]
        [HttpPost]
        public async Task<IActionResult> DeleteAsync(int id) {
            await _ramTypeService.DeleteAsync(id);
            return RedirectToAction("Index");
        }

        [HttpGet]
        public async Task<IActionResult> GetToDeleteAsync(int id) {
            var ramTypeDetails = await _ramTypeService.GetByIdAsync(id);
            return View(ramTypeDetails);
        }
    }
}

