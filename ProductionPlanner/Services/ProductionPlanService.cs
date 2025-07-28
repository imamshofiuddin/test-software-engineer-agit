using ProductionPlanner.Models.Domain;
using ProductionPlanner.Services.Interface;

namespace ProductionPlanner.Services
{
    public class ProductionPlanService : IProductionPlanService
    {
        public ProductionPlan AdjustPlan(List<int> inputPlan)
        {
            int total = inputPlan.Sum();
            int days = inputPlan.Count;
            int average = total / days;
            int rest = total % days;

            var sortedIndexes = inputPlan
                .Select((val, idx) => new { val, idx })
                .OrderByDescending(x => x.val)
                .Select(x => x.idx)
                .ToList();

            var adjusted = Enumerable.Repeat(average, days).ToList();
            for (int i = 0; i < rest; i++)
            {
                adjusted[sortedIndexes[i]] += 1;
            }

            return new ProductionPlan
            {
                InitialPlan = inputPlan,
                AdjustedPlan = adjusted
            };
        }
    }
}
