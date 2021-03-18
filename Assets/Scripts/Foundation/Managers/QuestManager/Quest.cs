using System;
using System.Collections.Generic;
using UnityEngine;

namespace Foundation
{
    [CreateAssetMenu(menuName = "OTUS/Crafting Recipe")]
    public sealed class Quest : ScriptableObject
    {
        public LocalizedString Name;
        public List<QuestCondition> Conditions;
    }
}
