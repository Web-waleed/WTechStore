using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using System.Configuration;
using WTechStore.Models;
using WTechStore.Models.ViewModels;

namespace WTechStore.Data
{
    public class AppDbContext: IdentityDbContext<ApplicationUser>
    {
        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options){}
        public DbSet<advertisement> advertisements { get; set; }
        public DbSet<Category> Categories { get; set; }
        public DbSet<product> products { get; set; }
        public DbSet<Feedback> feedbacks { get; set; }
        public DbSet<Slider> Sliders { get; set; }
        public DbSet<Contact>contacts { get; set; }
        public DbSet<CartItem> CartItems { get; set; }
        public DbSet<OrderProduct> orderProducts { get; set; }
       



    }
}
