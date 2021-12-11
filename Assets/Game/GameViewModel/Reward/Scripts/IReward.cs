using UnityEngine;

namespace Prototype
{
    public interface IReward
    {
        public Sprite Icon { get; }

        public string Text { get; }
    }

    public sealed class BaseReward : IReward
    {
        public Sprite Icon { get; }
       
        public string Text { get; }

        public BaseReward(Sprite icon, string text)
        {
            this.Icon = icon;
            this.Text = text;
        }
    }
}