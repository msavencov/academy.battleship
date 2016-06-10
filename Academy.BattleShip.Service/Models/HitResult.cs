namespace Academy.BattleShip.Service.Models
{
    public class HitResult
    {
        public bool Hit { get; set; }
        public bool Completed { get; set; }

        public static HitResult Create(bool hit, bool completed)
        {
            return new HitResult {Completed = completed, Hit = hit};
        }
    }
}
