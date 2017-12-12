using System;
using System.Collections.Generic;
using System.Linq;
using BotRetreat2017.Model;
using BotRetreat2017.Utilities;

namespace BotRetreat2017.Scripting.Extensions
{
    public static class BotExtensions
    {
        public static void UpdateBot(this Bot bot, ScriptGlobals coreGlobals)
        {
            bot.LocationX = coreGlobals.LocationX;
            bot.LocationY = coreGlobals.LocationY;
            bot.Orientation = coreGlobals.Orientation;
            bot.LastAction = coreGlobals.CurrentAction;
            bot.CurrentPhysicalHealth = coreGlobals.PhysicalHealth;
            bot.CurrentStamina = coreGlobals.Stamina;
            bot.LastAttackLocationX = coreGlobals.LastAttackLocation.X;
            bot.LastAttackLocationY = coreGlobals.LastAttackLocation.Y;
            bot.LastAttackBotId = coreGlobals.LastAttackBotId;
            bot.PhysicalDamageDone += coreGlobals.PhysicalDamageDone;
            bot.Kills += coreGlobals.Kills;
            if (bot.LastAction == LastAction.SelfDestruct)
            {
                bot.TimeOfDeath = DateTime.UtcNow;
            }
            bot.Memory = coreGlobals.Memory.Serialize();
        }

        public static List<BotStat> GetBotStats(this List<Bot> bots)
        {
            return bots.Select(bot => new BotStat
            {
                BotId = bot.Id,
                PhysicalHealth = bot.CurrentPhysicalHealth,
                Stamina = bot.CurrentStamina
            }).ToList();
        }

        public static void UpdateStatDrains(this List<Bot> bots, List<BotStat> botStats)
        {
            foreach (var bot in bots)
            {
                var botStat = botStats.Single(s => s.BotId == bot.Id);
                bot.PhysicalHealthDrain = (Int16)(botStat.PhysicalHealth - bot.CurrentPhysicalHealth);
                bot.StaminaDrain = (Int16)(botStat.Stamina - bot.CurrentStamina);
            }
        }

        public static void UpdateLastAttackLocation(this List<Bot> bots)
        {
            foreach (var bot in bots)
            {
                if (bot.LastAttackBotId.HasValue)
                {
                    var attackedBot = bots.SingleOrDefault(x => x.Id == bot.LastAttackBotId.Value);
                    if (attackedBot != null)
                    {
                        bot.LastAttackLocationX = attackedBot.LocationX;
                        bot.LastAttackLocationY = attackedBot.LocationY;
                    }
                }
            };
        }
    }
}