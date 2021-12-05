using Prototype.UI;

namespace Prototype.GameEngine
{
    public sealed class Trader : ITrader
    {
        public int WoodPrice
        {
            get { return this.point.WoodPrice; }
        }

        public int StonePrice
        {
            get { return this.point.StonePrice; }
        }

        private readonly TradePoint point;

        public Trader(TradePoint point)
        {
            this.point = point;
        }
    }
}