using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using BotRetreat2017.Client.Base;
using BotRetreat2017.Client.Interfaces;
using BotRetreat2017.Contracts;
using BotRetreat2017.Routes;

namespace BotRetreat2017.Client
{
    public class DeploymentClient : ClientBase, IDeploymentClient
    {
        public Task<DeploymentDto> Deploy(DeploymentDto deployment)
        {
            return Post<DeploymentDto, DeploymentDto>(
               $"{BaseUri}/{RouteConstants.PREFIX}/{RouteConstants.POST_DEPLOYMENT}", deployment);
        }

        public Task<Boolean> Available(String teamName, String arenaName)
        {
            var parameters = new Dictionary<String, String>
            {
                [nameof(teamName)] = teamName,
                [nameof(arenaName)] = arenaName
            };
            return Get<Boolean>(
              $"{BaseUri}/{RouteConstants.PREFIX}/{RouteConstants.POST_DEPLOYMENT_AVAILABLE}", parameters);
        }
    }
}