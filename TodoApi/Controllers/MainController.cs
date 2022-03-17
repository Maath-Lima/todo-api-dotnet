using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using System.Linq;

namespace TodoApi.Controllers
{
    [ApiController]
    public abstract class MainController : ControllerBase
    {
        protected ActionResult InvalidModelStateResponseError(ModelStateDictionary modelState)
        {
            return BadRequest(new {
                success = false,
                Errors = modelState.Values.SelectMany(ms => ms.Errors.Select(e => e.ErrorMessage))
            });
        }
    }
}
