using System;
using BotRetreat2017.Contracts.Interfaces;

namespace BotRetreat2017.Contracts
{
    public class PositionDto : IDataTransferObject
    {
        public Int16 X { get; set; }
        public Int16 Y { get; set; }
    }
}