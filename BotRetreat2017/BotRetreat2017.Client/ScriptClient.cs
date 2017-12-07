using System;
using System.Threading.Tasks;
using BotRetreat2017.Client.Base;
using BotRetreat2017.Client.Interfaces;
using BotRetreat2017.Contracts;
using BotRetreat2017.Routes;

namespace BotRetreat2017.Client
{
    public class ScriptClient : ClientBase, IScriptClient
    {
        public ScriptClient() : base("http://scriptvalidation.azurewebsites.net") { }

        public Task<ScriptValidationDto> ValidateScript(ScriptDto script)
        {
            return Post<ScriptValidationDto, ScriptDto>(
               $"{BaseUri}/{RouteConstants.PREFIX}/{RouteConstants.POST_SCRIPT_VALIDATION}", script);
        }
    }
}