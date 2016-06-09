using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Drawing;
using System.Linq;
using System.Runtime.Serialization;

namespace Academy.BattleShip.Service.Models
{
    public class ShipMap
    {
        public List<Ship> Ships { get; set; } = new List<Ship>();

        //TODO implement dynamic map support
        //public ShipMap(int VerticatSize, int horizontalSize, ShipMapConfiguration configuration, bool allowShipCollizion) {}
        
        public void ParseShips(List<Point> shipPositions)
        {
            var cellCopy = shipPositions.OrderBy(t => t.X).ThenBy(t => t.Y).ToList();
            var shipDirections = Enum.GetValues(typeof(Ship.ShipDirection)).Cast<Ship.ShipDirection>().ToArray();
            
            Iterate((x, y) =>
            {
                var point = new Point(x, y);
                if (cellCopy.Any(t => t == point))
                {
                    for (var i = 4; i >= 1; i--)
                    {
                        foreach (var direction in shipDirections)
                        {
                            var ship = new Ship(i, point, direction);
                            List<Point> matched;
                            if (ship.Match(cellCopy, out matched))
                            {
                                matched.ForEach(t => cellCopy.Remove(t));
                                Ships.Add(ship);
                            }
                        }
                    }
                }
            });
        }

        public void Validate()
        {
            if (Ships.Count != 10) throw new MapValidationException("The map must contain 10 ships.");

            var shipLenghtMessageFormat = "The map must contain only {0} ship with {1} points length.";

            if (Ships.Count(t => t.ShipLenght == 4) != 1)
                throw new MapValidationException(string.Format(shipLenghtMessageFormat, 1, 4));

            if (Ships.Count(t => t.ShipLenght == 3) != 2)
                throw new MapValidationException(string.Format(shipLenghtMessageFormat, 2, 3));

            if (Ships.Count(t => t.ShipLenght == 2) != 3)
                throw new MapValidationException(string.Format(shipLenghtMessageFormat, 3, 2));

            if (Ships.Count(t => t.ShipLenght == 1) != 4)
                throw new MapValidationException(string.Format(shipLenghtMessageFormat, 4, 1));

            foreach (var ship1 in Ships)
            {
                var ship2 = Ships.FirstOrDefault(t => t.CollidesWith(ship1));
                if (ship2.IsNull == false)
                {
                    throw new ShipsCollisionException("The map configuration does not allow ship collisions.", ship1, ship2);
                }
            }
        }

        public static void Iterate(Action<int, int> callbackAction)
        {
            for (var i = 0; i < 10; i++)
            {
                for (var j = 0; j < 10; j++)
                {
                    callbackAction.Invoke(j, i);
                }
            }
        }
    }

    public class ShipsCollisionException : Exception
    {
        public Ship Ship1 { get; private set; }
        public Ship Ship2 { get; private set; }

        public ShipsCollisionException(string message, Ship ship1, Ship ship2) : base(message)
        {
            Ship1 = ship1;
            Ship2 = ship2;
        }
    }

    public class MapValidationException : Exception
    {
        public MapValidationException()
        {
        }

        public MapValidationException(string message) : base(message)
        {
        }

        public MapValidationException(string message, Exception innerException) : base(message, innerException)
        {
        }

        protected MapValidationException(SerializationInfo info, StreamingContext context) : base(info, context)
        {
        }
    }

}