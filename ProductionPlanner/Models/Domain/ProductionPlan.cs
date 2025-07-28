namespace ProductionPlanner.Models.Domain
{
    public class ProductionPlan
    {
        public List<int> InitialPlan { get; set; }
        public List<int> AdjustedPlan { get; set; }
    }
}
