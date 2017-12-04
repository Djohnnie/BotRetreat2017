using System;
using BotRetreat2017.Contracts.Interfaces;

namespace BotRetreat2017.Contracts
{
    public class TeamRegistrationDto : IDataTransferObject
    {
        public String Name { get; set; }

        public String Password { get; set; }
    }
}