using AutoMapper;
using BotRetreat2017.Contracts;
using BotRetreat2017.Model;

namespace BotRetreat2017.Mappers
{
    public class ArenaMapper : Mapper<Arena, ArenaDto>
    {
        private readonly IMapper _mapper;

        public ArenaMapper()
        {
            MapperConfiguration config = new MapperConfiguration(cfg =>
            {
                cfg.CreateMap<Arena, ArenaDto>();
                cfg.CreateMap<ArenaDto, Arena>();
            });
            _mapper = config.CreateMapper();
        }

        public override ArenaDto Map(Arena entity)
        {
            return _mapper.Map<ArenaDto>(entity);
        }

        public override Arena Map(ArenaDto dataTransferObject)
        {
            return _mapper.Map<Arena>(dataTransferObject);
        }
    }
}