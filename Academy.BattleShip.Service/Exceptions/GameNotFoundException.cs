using System;

namespace Academy.BattleShip.Service.Exceptions
{
    public class GameNotFoundException : Exception
    {
        public GameNotFoundException(string message) : base (message){}
    }
}