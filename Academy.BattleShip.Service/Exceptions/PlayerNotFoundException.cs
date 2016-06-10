using System;

namespace Academy.BattleShip.Service.Exceptions
{
    public class PlayerNotFoundException : Exception
    {
        public PlayerNotFoundException(string message) : base(message)
        {
            
        }
    }
}