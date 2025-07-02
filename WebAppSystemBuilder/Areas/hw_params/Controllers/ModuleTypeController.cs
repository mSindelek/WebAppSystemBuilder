using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using WebAppSystemBuilder.DTO.Items.HWComponent.RAM;
using WebAppSystemBuilder.Services.hw_params;

namespace WebAppSystemBuilder.Areas.hw_params.Controllers {
    [Authorize]
    [Area("hw_params")]
    //[Route("Hardware/Parameters/[controller]/[action]")]
    public class ModuleTypeController(MemoryModuleTypeService moduleTypeService) : Controller {
        readonly MemoryModuleTypeService _moduleTypeService = moduleTypeService;

        [AllowAnonymous]
        [HttpGet]
        public IActionResult Index() {
            var allModuleTypes = _moduleTypeService.GetAll();
            return View(allModuleTypes);
        }

        [HttpGet]
        public IActionResult Create() {
            return View();
        }

        [Authorize(Roles = "Editor")]
        [HttpPost]
        public async Task<IActionResult> CreateAsync(ModuleTypeDTO newModuleType) {
            await _moduleTypeService.CreateAsync(newModuleType);
            return RedirectToAction("Index");
        }

        [HttpGet]
        public async Task<IActionResult> EditAsync(int id) {
            var moduleTypeToEdit = await _moduleTypeService.GetByIdAsync(id);
            if (moduleTypeToEdit == null) {
                return View("NotFound");
            }
            return View(moduleTypeToEdit);
        }

        [Authorize(Roles = "Editor")]
        [HttpPost]
        public async Task<IActionResult> EditAsync(ModuleTypeDTO ModuleTypeDTO) {
            await _moduleTypeService.UpdateAsync(ModuleTypeDTO);
            return RedirectToAction("Index");
        }

        [Authorize(Roles = "Admin")]
        [HttpPost]
        public async Task<IActionResult> DeleteAsync(int id) {
            await _moduleTypeService.DeleteAsync(id);
            return RedirectToAction("Index");
        }

        [HttpGet]
        public async Task<IActionResult> GetToDeleteAsync(int id) {
            var moduleTypeDetails = await _moduleTypeService.GetByIdAsync(id);
            return View(moduleTypeDetails);
        }
    }
}

