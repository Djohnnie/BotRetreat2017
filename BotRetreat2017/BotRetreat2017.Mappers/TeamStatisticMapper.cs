using AutoMapper;
using BotRetreat2017.Contracts;
using BotRetreat2017.Model;

namespace BotRetreat2017.Mappers
{
    public class TeamStatisticMapper : Mapper<Team, TeamStatisticDto>
    {
        private readonly IMapper _mapper;

        public TeamStatisticMapper()
        {
            MapperConfiguration config = new MapperConfiguration(cfg =>
            {
                cfg.CreateMap<Team, TeamStatisticDto>();
                cfg.CreateMap<TeamStatisticDto, Team>();
            });
            _mapper = config.CreateMapper();
        }

        public override TeamStatisticDto Map(Team entity)
        {
            return _mapper.Map<TeamStatisticDto>(entity);
        }

        public override Team Map(TeamStatisticDto dataTransferObject)
        {
            return _mapper.Map<Team>(dataTransferObject);
        }
    }
}