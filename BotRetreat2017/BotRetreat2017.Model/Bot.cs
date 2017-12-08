using System;
using System.Collections.Generic;
using BotRetreat2017.Model.Interfaces;

namespace BotRetreat2017.Model
{
    public class Bot : IEntity
    {
        public Guid Id { get; set; }

        public Int32 SysId { get; set; }

        public Boolean Predator { get; set; }

        public String Name { get; set; }

        public Int16 LocationX { get; set; }

        public Int16 LocationY { get; set; }

        public Orientation Orientation { get; set; }

        public Int16 MaximumPhysicalHealth { get; set; }

        public Int16 CurrentPhysicalHealth { get; set; }

        public Int16 PhysicalHealthDrain { get; set; }

        public Int16 MaximumStamina { get; set; }

        public Int16 CurrentStamina { get; set; }

        public Int16 StaminaDrain { get; set; }

        public LastAction LastAction { get; set; }

        public Int16 LastAttackLocationX { get; set; }

        public Int16 LastAttackLocationY { get; set; }

        public Guid? LastAttackBotId { get; set; }

        public String Script { get; set; }

        public String Memory { get; set; }

        public Int32 PhysicalDamageDone { get; set; }

        public Int32 Kills { get; set; }

        public DateTime TimeOfBirth { get; set; }

        public DateTime? TimeOfDeath { get; set; }

        public virtual ICollection<Deployment> Deployments { get; set; }
    }
}