using CbtAdminPanel.Models.MasterModel;
using CbtAdminPanel.Models.MasterModel.MasterSeries;
using Microsoft.EntityFrameworkCore;
using System.Data;

namespace CbtAdminPanel.Models
{
    public class MyDbcontext : DbContext
    {
        public MyDbcontext(DbContextOptions<MyDbcontext> options)
        : base(options)
        {
        }

        public DbSet<LocationSeries> LocationSeries { get; set; }
        public DbSet<LocationMaster> LocationMaster { get; set; }
        public DbSet<ProjectMaster> ProjectMaster { get; set; }
        public DbSet<ModuleMaster> ModuleMaster { get; set; }
        public DbSet<Roles> Roles { get; set; }
        public DbSet<Users> Users { get; set; }
       
       // public DbSet<TP_CityMaster> TP_CityMaster { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<Roles>().HasData(
                new Roles() { Id = 1, Name = "admin",CreatedBy=1,CreatedDate=DateTime.Now }
            );

            modelBuilder.Entity<Users>().HasData(
                new Users() { Id = 1,FullName="admin",UserName="admin",Email="Admin@techbinary.com",Password="admin@123",AccountStatus=0,LocationId="1",CreatedBy=1,CreatedDate=DateTime.Now, Role=1 }
                );

            // Configure your model here
        }
    }
}
