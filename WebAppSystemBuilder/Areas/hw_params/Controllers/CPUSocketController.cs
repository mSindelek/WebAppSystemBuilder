using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using WebAppSystemBuilder.DTO.Items.HWComponent.Shared;
using WebAppSystemBuilder.Services.hw_params;

namespace WebAppSystemBuilder.Areas.hw_params.Controllers {
    [Authorize]
    [Area("hw_params")]
    //[Route("Hardware/Parameters/[controller]/[action]")]
    public class CPUSocketController(CPUSocketService cpuSocketService) : Controller {
        internal readonly CPUSocketService _cpuSocketService = cpuSocketService;

        [AllowAnonymous]
        [HttpGet]
        public IActionResult Index() {
            var allCpuSockets = _cpuSocketService.GetAll();
            return View(allCpuSockets);
        }

        [HttpGet]
        public IActionResult Create() {
            return View();
        }

        [Authorize(Roles = "Editor")]
        [HttpPost]
        public async Task<IActionResult> CreateAsync(CPUSocketDTO newCpuSocket) {
            await _cpuSocketService.CreateAsync(newCpuSocket);
            return RedirectToAction("Index");
        }

        [HttpGet]
        public async Task<IActionResult> EditAsync(int id) {
            var cpuSocketToEdit = await _cpuSocketService.GetByIdAsync(id);
            if (cpuSocketToEdit == null) {
                return View("NotFound");
            }
            return View(cpuSocketToEdit);
        }

        [Authorize(Roles = "Editor")]
        [HttpPost]
        public async Task<IActionResult> EditAsync(CPUSocketDTO cpuSocket) {
            await _cpuSocketService.UpdateAsync(cpuSocket);
            return RedirectToAction("Index");
        }

        [Authorize(Roles = "Admin")]
        [HttpPost]
        public async Task<IActionResult> DeleteAsync(int id) {
            await _cpuSocketService.DeleteAsync(id);
            return RedirectToAction("Index");
        }

        [HttpGet]
        public async Task<IActionResult> GetToDeleteAsync(int id) {
            var cpuSocketDetails = await _cpuSocketService.GetByIdAsync(id);
            return View(cpuSocketDetails);
        }
    }
}

