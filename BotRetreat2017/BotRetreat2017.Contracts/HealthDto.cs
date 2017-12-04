using System;
using BotRetreat2017.Contracts.Interfaces;

namespace BotRetreat2017.Contracts
{
    public class HealthDto : IDataTransferObject
    {
        public Int16 Maximum { get; set; }

        public Int16 Current { get; set; }

        public Int16 Drain { get; set; }
    }
}