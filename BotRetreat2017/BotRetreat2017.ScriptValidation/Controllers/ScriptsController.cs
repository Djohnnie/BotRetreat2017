using System.Threading.Tasks;
using BotRetreat2017.Contracts;
using BotRetreat2017.Scripting;
using Microsoft.AspNetCore.Mvc;

namespace BotRetreat2017.ScriptValidation.Controllers
{
    public class ScriptsController : Controller
    {
        [HttpPost, Route("scripts")]
        public async Task<IActionResult> ValidateScript([FromBody]ScriptDto script)
        {
            ScriptValidationDto scriptValidation = await BotScript.ValidateScript(script.Script);
            return Ok(scriptValidation);
        }
    }
}