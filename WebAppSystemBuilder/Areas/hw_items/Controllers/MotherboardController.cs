using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using WebAppSystemBuilder.DTO.Items.HWComponent.Mobo;
using WebAppSystemBuilder.Services.hw_items;

namespace WebAppSystemBuilder.Areas.hw_items.Controllers {
    [Authorize]
    [Area("hw_items")]
    //[Route("Hardware/Items/[controller]/[action]")]
    public class MotherboardController(MotherboardService motherboardService) : Controller {
        internal readonly MotherboardService _motherboardService = motherboardService;
        private void FillDropdowns() {
            ViewBag.Chipsets = new SelectList(_motherboardService.GetChipsetDropdownData(), "Id", "Name", "Socket.Name");
        }

        [AllowAnonymous]
        [HttpGet]
        public IActionResult Index() {
            var allMotherboards = _motherboardService.GetAll();
            return View(allMotherboards);
        }

        [HttpGet]
        public IActionResult Create() {
            FillDropdowns();
            return View();
        }

        [Authorize(Roles = "Editor")]
        [HttpPost]
        public async Task<IActionResult> CreateAsync(MotherboardDTO newMotherboard) {
            await _motherboardService.CreateAsync(newMotherboard);
            return RedirectToAction("Index");
        }

        [HttpGet]
        public async Task<IActionResult> EditAsync(int id) {
            var motherboardToEdit = await _motherboardService.GetByIdAsync(id);
            if (motherboardToEdit == null) {
                return View("NotFound");
            }
            FillDropdowns();
            return View(motherboardToEdit);
        }

        [Authorize(Roles = "Editor")]
        [HttpPost]
        public async Task<IActionResult> EditAsync(MotherboardDTO motherboard) {
            await _motherboardService.UpdateAsync(motherboard);
            return RedirectToAction("Index");
        }

        [Authorize(Roles = "Admin")]
        [HttpPost]
        public async Task<IActionResult> DeleteAsync(int id) {
            await _motherboardService.DeleteAsync(id);
            return RedirectToAction("Index");
        }

        //public IActionResult Search(string q) {
        //    var foundMotherboards = _motherboardService.GetByName(q);
        //    return View("Index", foundMotherboards);
        //}

        [HttpGet]
        public async Task<IActionResult> GetToDeleteAsync(int id) {
            var motherboardDetails = await _motherboardService.GetByIdAsync(id);
            return View(motherboardDetails);
        }

    }
}

