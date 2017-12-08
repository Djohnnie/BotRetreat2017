using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;
using BotRetreat2017.DataAccess;
using BotRetreat2017.Model;
using Microsoft.CodeAnalysis.Scripting;
using Microsoft.EntityFrameworkCore;
using BotRetreat2017.Scripting;
using BotRetreat2017.Scripting.Extensions;
using BotRetreat2017.Utilities;

namespace BotRetreat2017.Processing
{
    public class Processor
    {
        private readonly BotScriptCache _cache = new BotScriptCache();

        public Task GoPublic()
        {
            return Go(arena => !arena.Private);
        }

        public Task GoPrivate()
        {
            return Go(arena => arena.Private);
        }

        public async Task Go(Expression<Func<Arena, Boolean>> arenaPredicate)
        {
            using (var dbContext = new BotRetreatDbContext())
            {
                List<Arena> arenas = await dbContext.Arenas.Where(arenaPredicate).ToListAsync();
                foreach (Arena arena in arenas)
                {
                    List<Bot> bots = await dbContext.Bots.Where(x =>
                            !x.TimeOfDeath.HasValue && x.Deployments.Any(d => d.Arena.Id == arena.Id))
                            .Include(x => x.Deployments).ThenInclude(x => x.Team).ToListAsync();

                    // Get all health statistics from all bots before the iteration.
                    var botStats = bots.GetBotStats();

                    await Task.WhenAll(bots.Select(bot => GoBot(bot, arena, bots)));

                    // Update last attack location
                    bots.UpdateLastAttackLocation();

                    // Update all health statistics from all bots after the iteration.
                    bots.UpdateStatDrains(botStats);

                    await dbContext.SaveChangesAsync();
                }
            }
        }

        private async Task GoBot(Bot bot, Arena arena, List<Bot> bots)
        {
            Script botScript = await GetCompiledBotScript(bot);
            ScriptGlobals globals = new ScriptGlobals(arena, bot, bots);

            try
            {
                await botScript.RunAsync(globals);
                bot.UpdateBot(globals);
            }
            catch
            {
                // Even if there are errors in the botscript, persist memory.
                bot.Memory = globals.Memory.Serialize();
                // Last action was a script error.
                bot.LastAction = LastAction.ScriptError;
            }
        }

        private async Task<Script> GetCompiledBotScript(Bot bot)
        {
            if (!_cache.ScriptStored(bot))
            {
                var botScript = await BotScript.PrepareScript(bot.Script);
                botScript.Compile();
                _cache.StoreScript(bot, botScript);
            }

            return _cache.LoadScript(bot);
        }
    }
}