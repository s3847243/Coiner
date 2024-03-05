using Expenses.Core;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Expenses.WebApi.Controllers
{
    [Authorize]
    [ApiController]
    [Route("[controller]")]  // this means that it will drop Controller and the Route will be /Statistics
    public class StatisticsController : ControllerBase
    {
        private readonly IStatisticsServices _statisticsServices;

        public StatisticsController(IStatisticsServices statisticsServices)
        {
            _statisticsServices = statisticsServices;
        }

        [HttpGet]
        public IActionResult GetExpenseAmountPerCategory()
        {
            return Ok(_statisticsServices.GetExpenseAmountPerCategory());
        }
    }
}
