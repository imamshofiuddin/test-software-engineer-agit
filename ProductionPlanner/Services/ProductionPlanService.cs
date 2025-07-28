using System;
using Microsoft.EntityFrameworkCore;
using ProductionPlanner.DB;
using ProductionPlanner.Models.Domain;
using ProductionPlanner.Services.Interface;

namespace ProductionPlanner.Services
{
    public class ProductionPlanService : IProductionPlanService
    {
        private readonly ProductionPlannerDbContext _context;

        public ProductionPlanService(ProductionPlannerDbContext context)
        {
            _context = context;
        }
        public List<int> GetAdjustedPlan(List<int> inputPlan)
        {
            int total = inputPlan.Where(p => p > 0).Sum();
            int activeDays = inputPlan.Count(p => p > 0);
            int avg = total / activeDays;
            int rest = total % activeDays;

            var activeIndexes = inputPlan
                .Select((val, idx) => new { val, idx })
                .Where(x => x.val > 0)
                .OrderByDescending(x => x.val)
                .Select(x => x.idx)
                .ToList();

            var adjusted = inputPlan.Select(x => x == 0 ? 0 : avg).ToList();
            for (int i = 0; i < rest; i++)
            {
                adjusted[activeIndexes[i]] += 1;
            }

            return adjusted;
        }

        public async Task SaveToDatabaseAsync(List<int> initial, List<int> adjusted)
        {
            var history = new ProductionPlanHistory
            {
                PlanningInitial = string.Join(",", initial),
                PlanningAdjusted = string.Join(",", adjusted),
                CreatedAt = DateTime.Now
            };
            _context.ProductionPlanHistories.Add(history);
            await _context.SaveChangesAsync();
        }

        public async Task<List<ProductionPlanHistory>> GetAllHistoryAsync()
        {
            return await _context.ProductionPlanHistories.OrderByDescending(x => x.CreatedAt).ToListAsync();
        }
    }
}
