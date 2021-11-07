using UnityEngine;

namespace Otus
{
    public interface IConditionProvider
    {
        bool IsTrue();
    }

    public abstract class ConditionProvider : MonoBehaviour, IConditionProvider
    {
        public abstract bool IsTrue();
    }
}