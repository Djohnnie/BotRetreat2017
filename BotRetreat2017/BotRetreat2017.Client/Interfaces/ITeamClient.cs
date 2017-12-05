using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using BotRetreat2017.Contracts;

namespace BotRetreat2017.Client.Interfaces
{
    public interface ITeamClient
    {
        Task<List<TeamDto>> GetAllTeams();

        Task<TeamDto> GetTeam(String name, String password);

        Task<TeamDto> CreateTeam(TeamRegistrationDto team);

        Task<TeamDto> EditTeam(TeamRegistrationDto team);

        Task RemoveTeam(Guid teamId);
    }
}