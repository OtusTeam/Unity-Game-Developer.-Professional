using System.Collections.Generic;

namespace Otus
{
    public interface IMultiplier<out T>
    {
        T GetValue();
    }

    public abstract class MultiplierGroup<T> : IMultiplier<T>
    {
        private readonly List<IMultiplier<T>> multipliers;

        public MultiplierGroup()
        {
            this.multipliers = new List<IMultiplier<T>>();
        }

        public void AddMultiplier(IMultiplier<T> multiplier)
        {
            this.multipliers.Add(multiplier);
        }

        public void RemoveMultiplier(IMultiplier<T> multiplier)
        {
            this.multipliers.Remove(multiplier);
        }

        public T GetValue()
        {
            return this.EvaluateValue(this.multipliers);
        }

        protected abstract T EvaluateValue(List<IMultiplier<T>> multipliers);
    }
}