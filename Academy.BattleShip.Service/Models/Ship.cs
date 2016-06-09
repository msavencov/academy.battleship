using System;
using System.Collections.Generic;
using System.Data.SqlTypes;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Academy.BattleShip.Service.Models
{
    public struct Ship : INullable
    {
        public int ShipLenght { get; private set; }
        
        public Ship(int shipLenght, Point startPoint, ShipDirection direction)
        {
            ShipLenght = shipLenght;
            ShipPoints = new List<Point>();
            var shipNearbyPoints = new List<Point>();

            for (int i = 0; i < shipLenght; i++)
            {
                var point = new Point();
                switch (direction)
                {
                        case ShipDirection.Horizontal: point.Offset(startPoint.X + i, startPoint.Y); break;
                        case ShipDirection.Vertical: point.Offset(startPoint.X, startPoint.Y + i); break;
                }

                // ooo - top
                // oxo - middle
                // ooo - bottom
                ShipPoints.Add(point);
                //todo Please refactor this terrible code :)
                {
                    // top line
                    shipNearbyPoints.Add(new Point(point.X - 1, point.Y - 1));
                    shipNearbyPoints.Add(new Point(point.X, point.Y - 1));
                    shipNearbyPoints.Add(new Point(point.X + 1, point.Y - 1));
                }
                {
                    // middle line
                    shipNearbyPoints.Add(new Point(point.X - 1, point.Y));
                    shipNearbyPoints.Add(new Point(point.X + 1, point.Y));
                }
                {
                    // bottom line
                    shipNearbyPoints.Add(new Point(point.X - 1, point.Y + 1));
                    shipNearbyPoints.Add(new Point(point.X, point.Y + 1));
                    shipNearbyPoints.Add(new Point(point.X + 1, point.Y + 1));
                }
            }

            var points = ShipPoints; 
            shipNearbyPoints.RemoveAll(t => t.X < 0 || t.Y < 0 || points.Contains(t));
            ShipNearbyPoints = shipNearbyPoints.Distinct().ToList();
        }
        
        public List<Point> ShipPoints { get; }
        
        public List<Point> ShipNearbyPoints { get; }


        public bool Match(List<Point> mapPoints, out List<Point> matched)
        {
            matched = new List<Point>();
            foreach (var shipPoint in ShipPoints)
            {
                if (mapPoints.Any(t => t == shipPoint))
                {
                    matched.Add(shipPoint);
                }
            }
            return matched.Count == ShipPoints.Count;
        }
        
        // TODO: fix this method, 
        public bool CollidesWith(Ship ship)
        {
            return this != ship && ShipNearbyPoints.Intersect(ship.ShipPoints).Any();
        }
        
        public enum ShipDirection { Vertical, Horizontal }

        public static bool operator !=(Ship left, Ship right)
        {
            return left == right == false; // :)
        }
        public static bool operator ==(Ship left, Ship right)
        {
            return left.ShipPoints.Except(right.ShipPoints).Any() == false;
        }

        public bool IsNull => ShipPoints == null || ShipPoints.Any() == false;
        
        public override bool Equals(object obj)
        {
            if (obj == null) return false;
            try
            {
                return this == (Ship) obj;
            }
            catch (Exception)
            {
                return false;
            }
        }

        public override int GetHashCode()
        {
            return base.GetHashCode() + ShipPoints.Sum(t=>t.GetHashCode());
        }
    }

}
