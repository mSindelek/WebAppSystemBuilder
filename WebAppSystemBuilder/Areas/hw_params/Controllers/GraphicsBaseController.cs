using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using WebAppSystemBuilder.DTO.Items.HWComponent.GPU;
using WebAppSystemBuilder.Services.hw_params;

namespace WebAppSystemBuilder.Areas.hw_params.Controllers {
    [Authorize]
    [Area("hw_params")]
    //[Route("Hardware/Parameters/[controller]/[action]")]
    public class GraphicsBaseController(GraphicsBaseService gpuBaseService) : Controller {
        internal readonly GraphicsBaseService _gpuBaseService = gpuBaseService;

        [AllowAnonymous]
        [HttpGet]
        public IActionResult Index() {
            var allGpuBases = _gpuBaseService.GetAll();
            return View(allGpuBases);
        }

        [Authorize(Roles = "Editor")]
        [HttpGet]
        public IActionResult Create() {
            return View();
        }
        [HttpPost]
        public async Task<IActionResult> CreateAsync(GraphicsBaseDTO newGpuBase) {
            await _gpuBaseService.CreateAsync(newGpuBase);
            return RedirectToAction("Index");
        }

        [HttpGet]
        public async Task<IActionResult> EditAsync(int id) {
            var gpuBaseToEdit = await _gpuBaseService.GetByIdAsync(id);
            if (gpuBaseToEdit == null) {
                return View("NotFound");
            }
            return View(gpuBaseToEdit);
        }

        [Authorize(Roles = "Editor")]
        [HttpPost]
        public async Task<IActionResult> EditAsync(GraphicsBaseDTO graphicsBaseDTO) {
            await _gpuBaseService.UpdateAsync(graphicsBaseDTO);
            return RedirectToAction("Index");
        }

        [Authorize(Roles = "Admin")]
        [HttpPost]
        public async Task<IActionResult> DeleteAsync(int id) {
            await _gpuBaseService.DeleteAsync(id);
            return RedirectToAction("Index");
        }

        //public IActionResult Search(string q) {
        //    var foundStudents = _gpuBaseService.GetByName(q);
        //    return View("Index", foundGpuBases);
        //}

        [HttpGet]
        public async Task<IActionResult> GetToDeleteAsync(int id) {
            var gpuBaseDetails = await _gpuBaseService.GetByIdAsync(id);
            return View(gpuBaseDetails);
        }
    }
}

