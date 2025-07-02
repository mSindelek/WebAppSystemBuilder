using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using WebAppSystemBuilder.DTO.Items.HWComponent.Mobo;
using WebAppSystemBuilder.Services.hw_params;

namespace WebAppSystemBuilder.Areas.hw_params.Controllers {
    [Authorize]
    [Area("hw_params")]
    //[Route("Hardware/Parameters/[controller]/[action]")]
    public class ChipsetController(ChipsetService chipsetService) : Controller {
        internal readonly ChipsetService _chipsetService = chipsetService;
        private void FillDropdowns() {
            ViewBag.Sockets = new SelectList(_chipsetService.GetSocketDropdownData(), "Id", "Name");
            ViewBag.RamTypes = new SelectList(_chipsetService.GetDdrDropdownData(), "Id", "Name");
        }

        [AllowAnonymous]
        [HttpGet]
        public IActionResult Index() {
            var allChipsets = _chipsetService.GetAll();
            return View(allChipsets);
        }

        [HttpGet]
        public IActionResult Create() {
            FillDropdowns();
            return View();
        }

        [Authorize(Roles = "Editor")]
        [HttpPost]
        public async Task<IActionResult> CreateAsync(ChipsetDTO newChipset) {
            await _chipsetService.CreateAsync(newChipset);
            return RedirectToAction("Index");
        }

        [HttpGet]
        public async Task<IActionResult> EditAsync(int id) {
            var chipsetToEdit = await _chipsetService.GetByIdAsync(id);
            if (chipsetToEdit == null) {
                return View("NotFound");
            }
            FillDropdowns();
            return View(chipsetToEdit);
        }

        [Authorize(Roles = "Editor")]
        [HttpPost]
        public async Task<IActionResult> EditAsync(ChipsetDTO chipsetDTO) {
            await _chipsetService.UpdateAsync(chipsetDTO);
            return RedirectToAction("Index");
        }

        [Authorize(Roles = "Admin")]
        [HttpPost]
        public async Task<IActionResult> DeleteAsync(int id) {
            await _chipsetService.DeleteAsync(id);
            return RedirectToAction("Index");
        }

        //public IActionResult Search(string q) {
        //    var foundChipsets = _chipsetService.GetByName(q);
        //    return View("Index", foundChipsets);
        //}

        [HttpGet]
        public async Task<IActionResult> GetToDeleteAsync(int id) {
            var chipsetDetails = await _chipsetService.GetByIdAsync(id);
            return View(chipsetDetails);
        }

    }
}

