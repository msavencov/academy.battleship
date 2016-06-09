using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Drawing;
using Academy.BattleShip.Service.Models;

namespace Academy.BattleShip.Web.Models
{
    public class RegisterModel
    {
        public string Name { get; set; }
        public string Key { get; set; }
        public List<List<bool>> ShipMap { get; set; } = new List<List<bool>>();

        public RegisterModel Init(List<Point> cells)
        {
            for (int i = 0; i < 10; i++)
            {
                var row = new List<bool>();
                for (int j = 0; j < 10; j++)
                {
                    row.Add(cells.Exists(t=>t.X == j && t.Y == i));
                }
                ShipMap.Add(row);
            }
            return this;
        }
    }
}