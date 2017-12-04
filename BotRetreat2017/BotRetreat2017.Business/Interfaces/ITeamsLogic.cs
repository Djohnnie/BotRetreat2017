using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using BotRetreat2017.Contracts;

namespace BotRetreat2017.Business.Interfaces
{
    public interface ITeamsLogic : ILogic
    {
        Task<List<TeamDto>> GetAllTeams();

        Task<TeamDto> GetTeam(String name, String password);

        Task<TeamDto> CreateTeam(TeamRegistrationDto team);

        Task<TeamDto> EditTeam(Guid teamId, String password, TeamRegistrationDto team);

        Task RemoveTeam(Guid teamId, String password);
    }
}