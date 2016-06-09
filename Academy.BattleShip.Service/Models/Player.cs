using System.Collections;
using System.Collections.Generic;
using System.Drawing;
using System.Text.RegularExpressions;

namespace Academy.BattleShip.Service.Models
{
    public class Player
    {
        public string Name { get; set; }
        public string Key { get; set; }

        public IEnumerable<Point> Cells { get; set; }

        public static Regex KeyRegex = new Regex(@"^\d{3}-\d{2}$");
    }
}
