using System;
using System.Collections.Generic;
using GameElements;
using GameElements.Unity;
using UnityEngine;

namespace GameEngine
{
    public sealed class GameSubsystem : UnityGameElementGroup
    {
        [SerializeField]
        private Element[] gameElements;

        private HashSet<object> allElements;
        
        private Dictionary<GameElementId, object> publicElementMap;

        public GameSubsystem()
        {
            this.allElements = new HashSet<object>();
            this.publicElementMap = new Dictionary<GameElementId, object>();
        }

        protected override void OnRegistered()
        {
            base.OnRegistered();
            this.RegisterElements();
        }

        private void RegisterElements()
        {
            for (int i = 0, count = this.gameElements.Length; i < count; i++)
            {
                var element = this.gameElements[i];
                this.AddElement(element.name, element.value);
            }
        }

        public void AddElement(GameElementId elementId, object element)
        {
            
            this.AddElement(interfaceElement);

            var name = guiElement.name;
            if (name != GUIName.NONE)
            {
                this.elementMap.Add(name, interfaceElement);
            }
        }

        [Serializable]
        private struct Element
        {
            [SerializeField]
            public GameElementId name;
            
            [SerializeField]
            public MonoBehaviour value;
        }

        public override IEnumerator<object> GetEnumerator()
        {
            throw new NotImplementedException();
        }

        public override int ElementCount { get; }
    }
}