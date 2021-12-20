using Microsoft.AspNetCore.Mvc;
using MultiTenancy_ODEV1.Attribute;
using MultiTenancy_ODEV1.Models;
using static MultiTenancy_ODEV1.Attribute.FilterEnum;

namespace MultiTenancy_ODEV1.Controllers
{
    public class WeatherForecastController : ControllerBase
    {
        [FilterParameter("entity",FilterType.ClassVariable)] 
        public IActionResult Get(TenantEntity entity)
        {
            return Ok("Gönderdiðin parametre : " + entity.TenantId);
        }
    }
}