using System;
using System.Threading.Tasks;
using BotRetreat2017.Contracts;

namespace BotRetreat2017.Client.Interfaces
{
    public interface IScriptClient
    {
        Task<ScriptValidationDto> ValidateScript(ScriptDto script);
    }
}