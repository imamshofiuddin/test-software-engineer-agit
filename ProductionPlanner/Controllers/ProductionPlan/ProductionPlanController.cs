using Microsoft.AspNetCore.Mvc;
using ProductionPlanner.Services.Interface;

namespace ProductionPlanner.Controllers.ProductionPlan
{
    public class ProductionPlanController : Controller
    {
        private readonly IProductionPlanService _productionService;

        public ProductionPlanController(IProductionPlanService productionService)
        {
            _productionService = productionService;
        }

        public IActionResult Index()
        {
            return View();
        }

        [HttpPost]
        public IActionResult Adjust(List<int> plan)
        {
            if (plan.Count != 5)
            {
                ModelState.AddModelError("", "Input harus berisi 5 hari (Senin–Jumat).");
                return View("Index");
            }

            var result = _productionService.AdjustPlan(plan);
            return View("AdjustedPlan", result);
        }
    }
}
