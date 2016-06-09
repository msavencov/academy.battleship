using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Data.Entity.Validation;
using System.Drawing;
using System.Linq;
using System.Runtime.Serialization;
using Academy.BattleShip.Entity;
using Academy.BattleShip.Entity.Models;
using Academy.BattleShip.Service.Models;
using Player = Academy.BattleShip.Service.Models.Player;

namespace Academy.BattleShip.Service.Services
{
    public partial class PlayerService : IPlayerService
    {
        public Player Find(string key)
        {
            if (Player.KeyRegex.Match(key ?? string.Empty).Success == false) return null;

            var x = _entities.Players.Include(t => t.Cells).FirstOrDefault(t => t.Key == key);

            if (x == null) return null;

            var player = new Player
            {
                Key = x.Key,
                Name = x.Name
            };
            player.Cells = x.Cells.Select(c => new Point {X = c.X, Y = c.Y});

            return player;
        }

        public Player Register(string name)
        {
            var key = _entities.Keys.Select(t => t.Key)
                .Except(_entities.Players.Select(t => t.Key))
                .OrderBy(t => Guid.NewGuid()).FirstOrDefault();

            var player = new Entity.Models.Player
            {
                Key = key,
                Name = name
            };
            
            _entities.Players.Add(player);

            try
            {
                _entities.SaveChanges();
            }
            catch (DbEntityValidationException exception)
            {
                throw new EntityValidationException(exception);
            }

            return new Player
            {
                Key = player.Key,
                Name = player.Name
            };
        }

        /// <summary>
        /// Verifies and stores the map in the database
        /// </summary>
        /// <param name="key">Player secret key</param>
        /// <param name="cells">Selected ship cells on the map</param>
        public void UpdateMap(string key, List<Point> cells)
        {
            var player = _entities.Players.Include(t=>t.Cells).FirstOrDefault(t => t.Key == key);
            if (player == null)
            {
                throw new KeyNotFoundException("Player with key " + key + " not found.");
            }
            var shipMap = new ShipMap();
            shipMap.ParseShips(cells);
            shipMap.Validate();
            
            var newCells = cells.Select(t => new ShipCell(t.X, t.Y) {PlayerId = player.Id});

            _entities.Cells.RemoveRange(player.Cells);
            _entities.Cells.AddRange(newCells);

            using (var transaction = _entities.Database.BeginTransaction())
            {
                try
                {
                    _entities.SaveChanges();
                    transaction.Commit();
                }
                catch (DbEntityValidationException exception)
                {
                    transaction.Rollback();
                    throw new EntityValidationException(exception);
                }
            }
        }
        
    }

    public partial class PlayerService
    {
        private readonly BattleShipEntities _entities;

        public PlayerService(DbContext entities)
        {
            _entities = (BattleShipEntities) entities;
        }

        public void Dispose()
        {
            _entities.Dispose();
        }
    }
    
    public class EntityValidationException : Exception
    {
        public DbValidationError[] Errors { get; private set; }
        public EntityValidationException(DbEntityValidationException exception) : this("See Errors property for details.")
        {
            Errors = exception.EntityValidationErrors.SelectMany(t => t.ValidationErrors).ToArray();
        }

        public EntityValidationException(string message) : base(message)
        {
        }

        public EntityValidationException(string message, Exception innerException) : base(message, innerException)
        {
        }

        protected EntityValidationException(SerializationInfo info, StreamingContext context) : base(info, context)
        {
        }
    }
    
}
