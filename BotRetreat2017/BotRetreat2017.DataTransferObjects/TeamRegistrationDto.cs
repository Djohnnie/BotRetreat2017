using System;
using BotRetreat2017.DataTransferObjects.Interfaces;

namespace BotRetreat2017.DataTransferObjects
{
    public class TeamRegistrationDto : IDataTransferObject
    {
        public String Name { get; set; }

        public String Password { get; set; }
    }
}