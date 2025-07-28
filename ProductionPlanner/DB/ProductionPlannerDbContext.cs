using Microsoft.EntityFrameworkCore;
using ProductionPlanner.Models.Domain;
using System;

namespace ProductionPlanner.DB
{
    public class ProductionPlannerDbContext : DbContext
    {
        public ProductionPlannerDbContext(DbContextOptions<ProductionPlannerDbContext> options) : base(options) { }

        public DbSet<ProductionPlanHistory> ProductionPlanHistories { get; set; }
    }
}
