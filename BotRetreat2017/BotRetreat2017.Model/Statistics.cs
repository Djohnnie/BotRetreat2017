using System;

namespace BotRetreat2017.Model
{
    public class Statistics
    {
        public Int32 PhysicalDamageDone { get; set; }

        public Int32 Kills { get; set; }

        public DateTime TimeOfBirth { get; set; }

        public DateTime? TimeOfDeath { get; set; }
    }
}