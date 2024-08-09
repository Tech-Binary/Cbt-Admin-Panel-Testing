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
        public DbSet<AssignRights> AssignRights { get; set; }
        public DbSet<FormPages> FormPages { get; set; }
       
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
            modelBuilder.Entity<FormPages>().HasData(
                new FormPages() { Id=1,FormId=101,Name="ProjectMaster",CreatedBy=1,CreatedDate=DateTime.Now},
                new FormPages() { Id=2,FormId=102,Name="LocationMaster",CreatedBy=1,CreatedDate=DateTime.Now},
                new FormPages() { Id=3,FormId=103,Name="ModuleMaster",CreatedBy=1,CreatedDate=DateTime.Now},
                new FormPages() { Id=4,FormId=104,Name="Role",CreatedBy=1,CreatedDate=DateTime.Now},
                new FormPages() { Id=5,FormId=105,Name="USER",CreatedBy=1,CreatedDate=DateTime.Now},
                new FormPages() { Id=6,FormId=106,Name="USERPersmission",CreatedBy=1,CreatedDate=DateTime.Now},
                new FormPages() { Id=7,FormId=107,Name="AssignLoaction",CreatedBy=1,CreatedDate=DateTime.Now},
                new FormPages() { Id=8,FormId=108,Name="LocationSeries",CreatedBy=1,CreatedDate=DateTime.Now}
                );

            // Configure your model here
        }
    }
}
