using System;
using BotRetreat2017.Contracts.Interfaces;

namespace BotRetreat2017.Contracts
{
    public class HistoryDto : IDataTransferObject
    {
        public Guid Id { get; set; }

        public String Name { get; set; }

        public String Description { get; set; }

        public HistoryTypeDto Type { get; set; }

        public String ArenaName { get; set; }

        public String BotName { get; set; }

        public DateTime DateTime { get; set; }
    }
}