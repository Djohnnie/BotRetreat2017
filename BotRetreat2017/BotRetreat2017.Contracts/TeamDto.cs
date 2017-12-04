using System;
using BotRetreat2017.Contracts.Interfaces;

namespace BotRetreat2017.Contracts
{
    public class TeamDto : IDataTransferObject
    {
        public Guid Id { get; set; }

        public String Name { get; set; }
    }
}