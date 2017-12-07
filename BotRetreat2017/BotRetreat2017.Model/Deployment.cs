using System;

namespace BotRetreat2017.Model
{
    public class Deployment
    {
        public Guid Id { get; set; }

        public Int32 SysId { get; set; }

        public Guid BotId { get; set; }

        public virtual Bot Bot { get; set; }

        public Guid TeamId { get; set; }

        public virtual Team Team { get; set; }

        public Guid ArenaId { get; set; }

        public virtual Arena Arena { get; set; }

        public DateTime DeploymentDateTime { get; set; }
    }
}