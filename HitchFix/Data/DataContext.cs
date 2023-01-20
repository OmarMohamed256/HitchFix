using HitchFix.Models;
using Microsoft.EntityFrameworkCore;
using System.Text.RegularExpressions;

namespace HitchFix.Data
{
    public class DataContext : DbContext
    {
        public DataContext(DbContextOptions options) : base(options)
        {
        }
        public DbSet<Device> Devices { get; set; }
        public DbSet<DeviceProblem> DeviceProblems { get; set; }
        public DbSet<DeviceType> DeviceTypes { get; set; }
        public DbSet<Order> Orders { get; set; }
        public DbSet<OrderProblem> OrderProblems { get; set; }

        protected override void OnModelCreating(ModelBuilder builder)
        { 
            base.OnModelCreating(builder);
        }
    }
}