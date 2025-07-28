using System.ComponentModel.DataAnnotations;

namespace ProductionPlanner.Models.Domain
{
    public class ProductionPlanHistory
    {
        [Key]
        public Guid Id { get; set; }
        public string PlanningInitial { get; set; }
        public string PlanningAdjusted { get; set; }
        public DateTime CreatedAt { get; set; }
    }
}
