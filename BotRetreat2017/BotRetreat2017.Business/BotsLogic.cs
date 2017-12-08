using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using BotRetreat2017.Business.Base;
using BotRetreat2017.Business.Exceptions;
using BotRetreat2017.Business.Interfaces;
using BotRetreat2017.Contracts;
using BotRetreat2017.DataAccess;
using BotRetreat2017.Mappers.Interfaces;
using BotRetreat2017.Model;
using Microsoft.EntityFrameworkCore;

namespace BotRetreat2017.Business
{
    public class BotsLogic : Logic<IBotRetreatDbContext>, IBotsLogic
    {
        private readonly IMapper<Bot, BotDto> _botMapper;

        public BotsLogic(IBotRetreatDbContext dbContext, IMapper<Bot, BotDto> botMapper) : base(dbContext)
        {
            _botMapper = botMapper;
        }

        public async Task<List<BotDto>> GetAllBots()
        {
            return _botMapper.Map(await _dbContext.Bots.ToListAsync());
        }

        public async Task<BotDto> CreateBot(BotDto bot)
        {
            var existingBot = await _dbContext.Bots.SingleOrDefaultAsync(x => x.Name == bot.Name);
            if (existingBot != null) { throw new BusinessException($"Bot with name {bot.Name} already exists!"); }
            var botEntity = _botMapper.Map(bot);
            botEntity.TimeOfBirth = DateTime.UtcNow;
            _dbContext.Bots.Add(botEntity);
            await _dbContext.SaveChangesAsync();
            return _botMapper.Map(
                await _dbContext.Bots.SingleOrDefaultAsync(x => x.Id == botEntity.Id));
        }

        public async Task<BotDto> EditBot(BotDto bot)
        {
            _dbContext.Bots.Attach(_botMapper.Map(bot));
            await _dbContext.SaveChangesAsync();
            return _botMapper.Map(
                await _dbContext.Bots.SingleOrDefaultAsync(x => x.Id == bot.Id));
        }

        public async Task RemoveBot(Guid botId)
        {
            var existingBot = await _dbContext.Bots.SingleOrDefaultAsync(x => x.Id == botId);
            if (existingBot != null)
            {
                _dbContext.Bots.Remove(existingBot);
                await _dbContext.SaveChangesAsync();
            }
        }
    }
}