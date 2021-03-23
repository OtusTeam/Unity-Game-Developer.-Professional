using UnityEngine;

namespace Foundation
{
    [CreateAssetMenu(menuName = "OTUS/Quest Condition")]
    public abstract class QuestCondition : ScriptableObject
    {
        public abstract bool IsTrue(QuestManager questManager);
    }
}
