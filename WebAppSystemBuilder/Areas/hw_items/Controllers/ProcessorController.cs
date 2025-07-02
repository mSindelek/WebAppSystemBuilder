using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using WebAppSystemBuilder.DTO.Items.HWComponent.CPU;
using WebAppSystemBuilder.Services.hw_items;

namespace WebAppSystemBuilder.Areas.hw_items.Controllers {
    [Authorize]
    [Area("hw_items")]
    //[Route("Hardware/Items/[controller]/[action]")]
    public class ProcessorController(ProcessorService processorService) : Controller {
        internal readonly ProcessorService _processorService = processorService;
        private void FillDropdowns() {
            ViewBag.Sockets = new SelectList(_processorService.GetSocketDropdownData(), "Id", "Name");
        }

        [AllowAnonymous]
        [HttpGet]
        public IActionResult Index() {
            var allProcessors = _processorService.GetAll();
            return View(allProcessors);
        }

        [HttpGet]
        public IActionResult Create() {
            FillDropdowns();
            return View();
        }

        [Authorize(Roles = "Editor")]
        [HttpPost]
        public async Task<IActionResult> CreateAsync(ProcessorDTO newProcessor) {
            await _processorService.CreateAsync(newProcessor);
            return RedirectToAction("Index");
        }

        [HttpGet]
        public async Task<IActionResult> EditAsync(int id) {
            var processorToEdit = await _processorService.GetByIdAsync(id);
            if (processorToEdit == null) {
                return View("NotFound");
            }
            FillDropdowns();
            return View(processorToEdit);
        }

        [Authorize(Roles = "Editor")]
        [HttpPost]
        public async Task<IActionResult> EditAsync(ProcessorDTO processorDTO) {
            await _processorService.UpdateAsync(processorDTO);
            return RedirectToAction("Index");
        }

        [Authorize(Roles = "Admin")]
        [HttpPost]
        public async Task<IActionResult> DeleteAsync(int id) {
            await _processorService.DeleteAsync(id);
            return RedirectToAction("Index");
        }

        //public IActionResult Search(string q) {
        //    var foundProcessors = _processorService.GetByName(q);
        //    return View("Index", foundProcessors);
        //}

        [HttpGet]
        public async Task<IActionResult> GetToDeleteAsync(int id) {
            var processorDetails = await _processorService.GetByIdAsync(id);
            return View(processorDetails);
        }

    }
}

