using System;
using BotRetreat2017.Contracts.Interfaces;

namespace BotRetreat2017.Contracts
{
    public class BotDto : IDataTransferObject
    {
        public Guid Id { get; set; }

        public Boolean Predator { get; set; }

        public String Name { get; set; }

        public PositionDto Location { get; set; }

        public OrientationDto Orientation { get; set; }

        public HealthDto PhysicalHealth { get; set; }

        public HealthDto Stamina { get; set; }

        public LastActionDto LastAction { get; set; }

        public PositionDto LastAttackLocation { get; set; }

        public String Script { get; set; }
    }
}