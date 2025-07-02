using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using WebAppSystemBuilder.DTO.Items.HWComponent.RAM;
using WebAppSystemBuilder.Services.hw_items;

namespace WebAppSystemBuilder.Areas.hw_items.Controllers {
    [Authorize]
    [Area("hw_items")]
    //[Route("Hardware/Items/[controller]/[action]")]
    public class MemoryController(MemoryService memoryService) : Controller {
        internal readonly MemoryService _memoryService = memoryService;
        private void FillDropdowns() {
            ViewBag.RamType = new SelectList(_memoryService.GetRamTypeDropdownData(), "Id", "Name");
            ViewBag.ModuleType = new SelectList(_memoryService.GetModuleTypeDropdownData(), "Id", "Name");
        }

        [AllowAnonymous]
        [HttpGet]
        public IActionResult Index() {
            var allMemorys = _memoryService.GetAll();
            return View(allMemorys);
        }

        [HttpGet]
        public IActionResult Create() {
            FillDropdowns();
            return View();
        }
        [Authorize(Roles = "Editor")]
        [HttpPost]
        public async Task<IActionResult> CreateAsync(MemoryDTO newMemory) {
            await _memoryService.CreateAsync(newMemory);
            return RedirectToAction("Index");
        }

        [HttpGet]
        public async Task<IActionResult> EditAsync(int id) {
            var memoryToEdit = await _memoryService.GetByIdAsync(id);
            if (memoryToEdit == null) {
                return View("NotFound");
            }
            FillDropdowns();
            return View(memoryToEdit);
        }
        [Authorize(Roles = "Editor")]
        [HttpPost]
        public async Task<IActionResult> EditAsync(MemoryDTO memoryDTO) {
            await _memoryService.UpdateAsync(memoryDTO);
            return RedirectToAction("Index");
        }

        [Authorize(Roles = "Admin")]
        [HttpPost]
        public async Task<IActionResult> DeleteAsync(int id) {
            await _memoryService.DeleteAsync(id);
            return RedirectToAction("Index");
        }

        //public IActionResult Search(string q) {
        //    var foundMemorys = _memoryService.GetByName(q);
        //    return View("Index", foundMemorys);
        //}

        [HttpGet]
        public async Task<IActionResult> GetToDeleteAsync(int id) {
            var memoryDetails = await _memoryService.GetByIdAsync(id);
            return View(memoryDetails);
        }

    }
}

