using System;
using BotRetreat2017.DataTransferObjects.Interfaces;

namespace BotRetreat2017.DataTransferObjects
{
    public class TeamDto : IDataTransferObject
    {
        public Guid Id { get; set; }

        public String Name { get; set; }
    }
}