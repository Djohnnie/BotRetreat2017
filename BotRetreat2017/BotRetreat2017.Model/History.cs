using System;
using BotRetreat2017.Model.Interfaces;

namespace BotRetreat2017.Model
{
    public class History : IEntity
    {
        public Guid Id { get; set; }
        public Int32 SysId { get; set; }

        public String Name { get; set; }

        public String Description { get; set; }

        public HistoryType Type { get; set; }

        public Guid? ArenaId { get; set; }

        public Guid? DeploymentId { get; set; }

        public Guid? BotId { get; set; }

        public DateTime DateTime { get; set; }
    }
}