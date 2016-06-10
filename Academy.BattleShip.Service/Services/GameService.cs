using System;
using System.Data.Entity;
using System.Linq;
using Academy.BattleShip.Entity;
using Academy.BattleShip.Entity.Models;
using Academy.BattleShip.Service.Exceptions;
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

            foreach (var game in player.MyGames.Where(t=>t.Completed == false))
            {
                game.Completed = true;
            }
            
            var query = from p1 in _entities.Players
                        from p2 in _entities.Players
                        join g in _entities.Games on new {p1 = p1.Id, p2 = p2.Id} equals new {p1 = g.PlayerId1, p2 = g.PlayerId2} into games
                        where p1.Id == player.Id && p1.Id != p2.Id && games.Any() == false
                        select p2.Id;

            var newPlayerId = query.FirstOrDefault();

            if (newPlayerId == 0) throw new NoMoreGamesException("No more availble games.");
            
            var newGame = new Game
            {
                Id = Guid.NewGuid(), PlayerId1 = player.Id, PlayerId2 = newPlayerId
            };

            _entities.Games.Add(newGame);

            using (var transaction = _entities.Database.BeginTransaction())
            {
                try
                {
                    _entities.SaveChanges();
                    transaction.Commit();
                }
                catch (Exception)
                {
                    transaction.Rollback();
                    throw;
                }
            }

            return newGame.Id;
        }

        public Guid CurrentGame(string playerKey)
        {
            var currentGame = _entities.Players.Include(t => t.MyGames)
                                       .SelectMany(t => t.MyGames)
                                       .FirstOrDefault(t => t.Completed == false);
            if (currentGame == null)
            {
                throw new GameNotFoundException("Use GetNew method to start new game.");
            }
            return currentGame.Id;
        }

        public HitResult Hit(Guid gameId, int x, int y)
        {
            if (x > 9 || x < 0 || y > 9 || y < 0)
            {
                throw new ArgumentException("Value of x and y must be between 0 and 9 (inclusive)");
            }

            var game = _entities.Games.Include(t=>t.GameHits).FirstOrDefault(t => t.Id == gameId);

            if (game == null)
            {
                throw new GameNotFoundException("Game with id '" + gameId + "' not found.");
            }

            if (game.Completed)
            {
                return HitResult.Create(false, true);
            }
            
            var newHit = new GameHit
            {
                GameId = game.Id, X = x, Y = y
            };
            game.GameHits.Add(newHit);
            _entities.SaveChanges();

            var result= new HitResult();
            result.Hit = game.Player2.Cells.Any(t => t.X == x && t.Y == y);

            if (game.GameHits.Count() == 100)
            {
                result.Completed = true;
                game.Completed = true;
                _entities.SaveChanges();
            }

            return result;
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