using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using BotRetreat2017.Client.Base;
using BotRetreat2017.Client.Interfaces;
using BotRetreat2017.Contracts;
using BotRetreat2017.Routes;

namespace BotRetreat2017.Client
{
    public class TeamClient : ClientBase, ITeamClient
    {
        public Task<List<TeamDto>> GetAllTeams()
        {
            return Get<List<TeamDto>>(
                $"{BaseUri}/{RouteConstants.PREFIX}/{RouteConstants.GET_TEAMS}");
        }

        public Task<TeamDto> GetTeam(String name, String password)
        {
            var parameters = new Dictionary<String, String>
            {
                [nameof(name)] = name,
                [nameof(password)] = password
            };
            return Get<TeamDto>(
                $"{BaseUri}/{RouteConstants.PREFIX}/{RouteConstants.GET_TEAM}", parameters);
        }

        public Task<TeamDto> CreateTeam(TeamRegistrationDto team)
        {
            return Post<TeamDto, TeamRegistrationDto>(
               $"{BaseUri}/{RouteConstants.PREFIX}/{RouteConstants.POST_TEAM}", team);
        }

        public Task<TeamDto> EditTeam(TeamRegistrationDto team)
        {
            return Put<TeamDto, TeamRegistrationDto>(
               $"{BaseUri}/{RouteConstants.PREFIX}/{RouteConstants.PUT_TEAM}", team);
        }

        public Task RemoveTeam(Guid teamId)
        {
            return Delete(
               $"{BaseUri}/{RouteConstants.PREFIX}/{RouteConstants.DELETE_TEAM}", teamId);
        }
    }
}