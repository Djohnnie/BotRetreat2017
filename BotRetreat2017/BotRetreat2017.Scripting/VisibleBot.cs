using System;
using System.Collections.Generic;
using System.Text;
using BotRetreat2017.Model;
using BotRetreat2017.Scripting.Interfaces;

namespace BotRetreat2017.Scripting
{
    public class VisibleBot : IBot
    {
        public Orientation Orientation { get; }

        public Position Location { get; }

        public String Name { get; }

        public VisibleBot(Bot bot)
        {
            Orientation = bot.Orientation;
            Location = bot.Location;
            Name = bot.Name;
        }

    }
}