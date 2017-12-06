using AutoMapper;
using BotRetreat2017.Contracts;
using BotRetreat2017.Model;

namespace BotRetreat2017.Mappers
{
   public class HistoryMapper : Mapper<History, HistoryDto>
    {
        private readonly IMapper _mapper;

        public HistoryMapper()
        {
            MapperConfiguration config = new MapperConfiguration(cfg =>
            {
                cfg.CreateMap<History, HistoryDto>();
                cfg.CreateMap<HistoryDto, History>();
            });
            _mapper = config.CreateMapper();
        }

        public override HistoryDto Map(History entity)
        {
            return _mapper.Map<HistoryDto>(entity);
        }

        public override History Map(HistoryDto dataTransferObject)
        {
            return _mapper.Map<History>(dataTransferObject);
        }
    }
}