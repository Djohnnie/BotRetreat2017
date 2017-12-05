using System;
using System.Threading.Tasks;
using BotRetreat2017.Client.Base;
using BotRetreat2017.Client.Interfaces;
using BotRetreat2017.Client.Routes;
using BotRetreat2017.Contracts;

namespace BotRetreat2017.Client
{
    public class ScriptClient : ClientBase, IScriptClient
    {
        //public ScriptClient() : base("http://localhost/BotRetreat.Web.ScriptValidation") { }
        public ScriptClient() : base("http://botretreat.cloudapp.net:8080") { }

        public Task<ScriptValidationDto> ValidateScript(String script)
        {
            return Post<ScriptValidationDto, String>(
               $"{BaseUri}/{RouteConstants.PREFIX}/{RouteConstants.POST_SCRIPT_VALIDATION}", script);
        }
    }
}