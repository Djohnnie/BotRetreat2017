using System.Threading.Tasks;
using BotRetreat2017.Contracts;
using BotRetreat2017.Scripting;
using Microsoft.AspNetCore.Mvc;
using BotRetreat2017.Routes;

namespace BotRetreat2017.ScriptValidation.Controllers
{
    [Route(RouteConstants.PREFIX)]
    public class ScriptsController : Controller
    {
        [HttpPost, Route(RouteConstants.POST_SCRIPT_VALIDATION)]
        public async Task<IActionResult> ValidateScript([FromBody]ScriptDto script)
        {
            ScriptValidationDto scriptValidation = await BotScript.ValidateScript(script.Script);
            return Ok(scriptValidation);
        }
    }
}