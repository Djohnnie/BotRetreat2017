using AutoMapper;
using BotRetreat2017.Contracts;
using BotRetreat2017.Model;

namespace BotRetreat2017.Mappers
{
    public class ArenaListMapper : Mapper<Arena, ArenaListDto>
    {
        private readonly IMapper _mapper;

        public ArenaListMapper()
        {
            MapperConfiguration config = new MapperConfiguration(cfg =>
            {
                cfg.CreateMap<Arena, ArenaListDto>();
                cfg.CreateMap<ArenaListDto, Arena>();
            });
            _mapper = config.CreateMapper();
        }

        public override ArenaListDto Map(Arena entity)
        {
            return _mapper.Map<ArenaListDto>(entity);
        }

        public override Arena Map(ArenaListDto dataTransferObject)
        {
            return _mapper.Map<Arena>(dataTransferObject);
        }
    }
}