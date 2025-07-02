using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using System.Diagnostics;
using WebAppSystemBuilder.Models;
using WebAppSystemBuilder.Models.Items.HWComponent.CPU;
using WebAppSystemBuilder.Models.Items.HWComponent.GPU;
using WebAppSystemBuilder.Models.Items.HWComponent.Mobo;
using WebAppSystemBuilder.Models.Items.HWComponent.RAM;
using WebAppSystemBuilder.Models.Items.HWComponent.Shared;

namespace WebAppSystemBuilder {
    public class AppDbContext(DbContextOptions<AppDbContext> options) : IdentityDbContext<AppUser>(options) {
        // param db
        public DbSet<CPUSocketModel> HW_CPUSockets { get; set; }
        public DbSet<RamTypeModel> HW_RamTypes { get; set; }
        public DbSet<ModuleTypeModel> HW_ModuleTypes { get; set; }
        public DbSet<ChipsetModel> HW_Chipsets { get; set; }
        public DbSet<GraphicsBaseModel> HW_GraphicsBases { get; set; }

        // item db
        public DbSet<ProcessorModel> HW_Processors { get; set; }
        public DbSet<MemoryModel> HW_Memories { get; set; }
        public DbSet<MotherboardModel> HW_Motherboards { get; set; }
        public DbSet<GraphicsCardModel> HW_GraphicsCards { get; set; }
    }
}

