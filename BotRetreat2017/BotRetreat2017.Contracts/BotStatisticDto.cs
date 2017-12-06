using System;
using BotRetreat2017.Contracts.Interfaces;

namespace BotRetreat2017.Contracts
{
    public class BotStatisticDto : IDataTransferObject
    {
        public Guid BotId { get; set; }

        public String BotName { get; set; }

        public Guid ArenaId { get; set; }

        public String ArenaName { get; set; }

        public HealthDto PhysicalHealth { get; set; }

        public HealthDto Stamina { get; set; }

        public LastActionDto LastAction { get; set; }

        public PositionDto Location { get; set; }

        public OrientationDto Orientation { get; set; }

        public String Script { get; set; }

        public Int32 TotalPhysicalDamageDone { get; set; }

        public Int32 TotalNumberOfKills { get; set; }

        public TimeSpan BotLife { get; set; }

    }
}