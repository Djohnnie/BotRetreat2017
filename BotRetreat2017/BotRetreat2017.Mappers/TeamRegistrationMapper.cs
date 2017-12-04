using AutoMapper;
using BotRetreat2017.Contracts;
using BotRetreat2017.Model;

namespace BotRetreat2017.Mappers
{
    public class TeamRegistrationMapper : Mapper<Team, TeamRegistrationDto>
    {
        private readonly IMapper _mapper;

        public TeamRegistrationMapper()
        {
            MapperConfiguration config = new MapperConfiguration(cfg =>
            {
                cfg.CreateMap<Team, TeamRegistrationDto>();
                cfg.CreateMap<TeamRegistrationDto, Team>();
            });
            _mapper = config.CreateMapper();
        }

        public override TeamRegistrationDto Map(Team entity)
        {
            return _mapper.Map<TeamRegistrationDto>(entity);
        }

        public override Team Map(TeamRegistrationDto dataTransferObject)
        {
            return _mapper.Map<Team>(dataTransferObject);
        }
    }
}