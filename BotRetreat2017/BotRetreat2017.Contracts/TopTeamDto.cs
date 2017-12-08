using System;
using BotRetreat2017.Contracts.Interfaces;

namespace BotRetreat2017.Contracts
{
    public class TopTeamDto : IDataTransferObject
    {
        public String TeamName { get; set; }
        public Int32 NumberOfKills { get; set; }
        public String AverageBotLife { get; set; }
    }
}