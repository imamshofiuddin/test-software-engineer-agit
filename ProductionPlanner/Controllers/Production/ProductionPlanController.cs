using Microsoft.AspNetCore.Mvc;
using ProductionPlanner.Models.Domain;
using ProductionPlanner.Services.Interface;

namespace ProductionPlanner.Controllers.Production
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
        public async Task<IActionResult> Adjust(List<int> plan)
        {
            if (plan.Count != 7)
            {
                ModelState.AddModelError("", "Input harus 7 hari (Senin–Minggu).");
                return View("Index");
            }

            var adjusted = _productionService.GetAdjustedPlan(plan);
            await _productionService.SaveToDatabaseAsync(plan, adjusted);

            var model = new ProductionPlan
            {
                InitialPlan = plan,
                AdjustedPlan = adjusted
            };

            return View("AdjustedPlan", model);
        }

        public async Task<IActionResult> History()
        {
            var history = await _productionService.GetAllHistoryAsync();
            return View(history);
        }
    }
}
