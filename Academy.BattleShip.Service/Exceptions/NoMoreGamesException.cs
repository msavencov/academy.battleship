using System;

namespace Academy.BattleShip.Service.Exceptions
{
    public class NoMoreGamesException : Exception
    {
        public NoMoreGamesException(string message) : base(message) {}
    }
}