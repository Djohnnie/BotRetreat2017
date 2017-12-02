using System;
using System.Threading.Tasks;
using BotRetreat2017.Business.Exceptions;
using BotRetreat2017.Business.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace BotRetreat2017.WebApi.Helpers
{
    public class ApiController<TLogic> : Controller where TLogic : ILogic
    {
        private readonly TLogic _logic;

        protected ApiController(TLogic logic)
        {
            _logic = logic;
        }

        protected async Task<IActionResult> Ok<TResult>(Func<TLogic, Task<TResult>> logicCall)
        {
            return await Try(async () =>
            {
                var result = await logicCall(_logic);
                return result != null ? Ok(result) : (ActionResult)NotFound();
            });
        }

        protected async Task<IActionResult> Ok(Func<TLogic, Task> logicCall)
        {
            return await Try(async () =>
            {
                await logicCall(_logic);
                return Ok();
            });
        }

        private async Task<IActionResult> Try(Func<Task<IActionResult>> action)
        {
            try
            {
                return await action();
            }
            catch (BusinessException ex)
            {
                return BadRequest(ex.Message);
            }
        }
    }
}