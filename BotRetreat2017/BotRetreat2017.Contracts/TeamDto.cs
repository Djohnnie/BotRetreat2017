using System;
using System.Collections.Generic;
using BotRetreat2017.Contracts.Interfaces;

namespace BotRetreat2017.Contracts
{
    public class TeamDto : IDataTransferObject
    {
        public Guid Id { get; set; }

        public String Name { get; set; }

        public List<TeamStatisticDto> Statistics { get; set; }
    }
}