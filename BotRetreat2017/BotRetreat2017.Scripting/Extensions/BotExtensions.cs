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
            bot.Location.X = coreGlobals.Location.X;
            bot.Location.Y = coreGlobals.Location.Y;
            bot.Orientation = coreGlobals.Orientation;
            bot.LastAction = coreGlobals.CurrentAction;
            bot.PhysicalHealth.Current = coreGlobals.PhysicalHealth;
            bot.Stamina.Current = coreGlobals.Stamina;
            bot.LastAttackLocation.X = coreGlobals.LastAttackLocation.X;
            bot.LastAttackLocation.Y = coreGlobals.LastAttackLocation.Y;
            bot.LastAttackBotId = coreGlobals.LastAttackBotId;
            bot.Statistics.PhysicalDamageDone += coreGlobals.PhysicalDamageDone;
            bot.Statistics.Kills += coreGlobals.Kills;
            if (bot.LastAction == LastAction.SelfDestruct)
            {
                bot.Statistics.TimeOfDeath = DateTime.UtcNow;
            }
            bot.Memory = coreGlobals.Memory.Serialize();
        }

        public static List<BotStat> GetBotStats(this List<Bot> bots)
        {
            return bots.Select(bot => new BotStat
            {
                BotId = bot.Id,
                PhysicalHealth = bot.PhysicalHealth.Current,
                Stamina = bot.Stamina.Current
            }).ToList();
        }

        public static void UpdateStatDrains(this List<Bot> bots, List<BotStat> botStats)
        {
            foreach (var bot in bots)
            {
                var botStat = botStats.Single(s => s.BotId == bot.Id);
                bot.PhysicalHealth.Drain = (Int16)(botStat.PhysicalHealth - bot.PhysicalHealth.Current);
                bot.Stamina.Drain = (Int16)(botStat.Stamina - bot.Stamina.Current);
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
                        bot.LastAttackLocation.X = attackedBot.Location.X;
                        bot.LastAttackLocation.Y = attackedBot.Location.Y;
                    }
                }
            };
        }
    }
}