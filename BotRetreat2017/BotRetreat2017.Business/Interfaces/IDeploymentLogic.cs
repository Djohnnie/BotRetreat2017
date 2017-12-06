using System;
using System.Threading.Tasks;
using BotRetreat2017.Contracts;

namespace BotRetreat2017.Business.Interfaces
{
    public interface IDeploymentLogic : ILogic
    {
        Task<DeploymentDto> Deploy(DeploymentDto deployment);

        Task<Boolean> Available(String teamName, String arenaName);
    }
}