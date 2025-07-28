using ProductionPlanner.Models.Domain;

namespace ProductionPlanner.Services.Interface
{
    public interface IProductionPlanService
    {
        List<int> GetAdjustedPlan(List<int> inputPlan);
        Task SaveToDatabaseAsync(List<int> original, List<int> adjusted);
        Task<List<ProductionPlanHistory>> GetAllHistoryAsync();
    }
}
