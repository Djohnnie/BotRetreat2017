using System.Collections.Generic;

namespace BotRetreat2017.Contracts
{
    public class GameDto
    {
        public ArenaDto Arena { get; set; }

        public List<BotDto> Bots { get; set; }

        public List<HistoryDto> History { get; set; }
    }
}