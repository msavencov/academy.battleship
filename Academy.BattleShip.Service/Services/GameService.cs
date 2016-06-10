using System;
using System.Data.Entity;
using System.Linq;
using Academy.BattleShip.Entity;
using Academy.BattleShip.Service.Models;

namespace Academy.BattleShip.Service.Services
{
    public partial class GameService : IGameService
    {
        public Guid NewGame(string playerKey)
        {
            var player = _entities.Players.FirstOrDefault(t => t.Key == playerKey);
            if (player == null)
            {
                throw new PlayerNotFoundException("Player with key '" + playerKey + "' not found.");
            }

            foreach (var game in player.MyGames)
            {
                game.Completed = true;
            }

            //from g in player.MyGames
            //join p in _entities.Players

            //var played = player.MyGames;
            //var available = _entities.Players.se
            //var newGame = 

            throw new NotImplementedException();
        }

        public Guid CurrentGame(string playerKey)
        {
            throw new NotImplementedException();
        }

        public HitResult Hit(Guid gameId, int x, int y)
        {
            throw new NotImplementedException();
        }
        
    }


    public partial class GameService
    {
        private readonly BattleShipEntities _entities;
        private readonly IPlayerService _playerService;

        public GameService(DbContext entities, IPlayerService playerService)
        {
            _playerService = playerService;
            _entities = (BattleShipEntities)entities;
        }

        public void Dispose()
        {
            _entities.Dispose();
        }
    }
}