using System;
using System.Collections.Generic;
using BotRetreat2017.Model.Interfaces;

namespace BotRetreat2017.Model
{
    public class Team : IEntity
    {
        public Guid Id { get; set; }

        public Int32 SysId { get; set; }

        public String Name { get; set; }

        public String Password { get; set; }

        public Boolean Predator { get; set; }

        public virtual Arena PersonalArena { get; set; }

        public virtual ICollection<Deployment> Deployments { get; set; }
    }
}