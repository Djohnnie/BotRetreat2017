using AutoMapper;
using BotRetreat2017.Contracts;
using BotRetreat2017.Model;

namespace BotRetreat2017.Mappers
{
    class BotStatisticMapper : Mapper<Bot, BotStatisticDto>
    {
        private readonly IMapper _mapper;

        public BotStatisticMapper()
        {
            MapperConfiguration config = new MapperConfiguration(cfg =>
            {
                cfg.CreateMap<Bot, BotStatisticDto>();
                cfg.CreateMap<BotStatisticDto, Bot>();
            });
            _mapper = config.CreateMapper();
        }

        public override BotStatisticDto Map(Bot entity)
        {
            return _mapper.Map<BotStatisticDto>(entity);
        }

        public override Bot Map(BotStatisticDto dataTransferObject)
        {
            return _mapper.Map<Bot>(dataTransferObject);
        }
    }
}