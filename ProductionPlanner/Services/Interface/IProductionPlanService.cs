using ProductionPlanner.Models.Domain;

namespace ProductionPlanner.Services.Interface
{
    public interface IProductionPlanService
    {
        ProductionPlan AdjustPlan(List<int> inputPlan);
    }
}
