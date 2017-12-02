using System;
using BotRetreat2017.Model;

namespace BotRetreat2017.Scripting.Interfaces
{
    public interface IBot
    {
        Orientation Orientation { get; }

        Position Location { get; }

        String Name { get; }
    }
}