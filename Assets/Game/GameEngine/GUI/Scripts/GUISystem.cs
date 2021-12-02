using System;
using System.Collections.Generic;
using GameElements.Unity;
using UnityEngine;

namespace GameElements
{
    public sealed class GUISystem : UnityGameElementGroup
    {
        [SerializeField]
        private GUIElement[] gameElements;

        
        
        private Dictionary<GUIName, object> publicElementMap;

        public GUISystem()
        {
            this.publicElementMap = new Dictionary<GUIName, object>();
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
                var guiElement = this.gameElements[i];
                var interfaceElement = guiElement.gameElement;
                this.AddElement(interfaceElement);

                var name = guiElement.name;
                if (name != GUIName.NONE)
                {
                    this.elementMap.Add(name, interfaceElement);
                }
            }
        }

        [Serializable]
        private struct GUIElement
        {
            [SerializeField]
            public GUIName name;
            
            [SerializeField]
            public MonoBehaviour gameElement;
        }

        public override IEnumerator<object> GetEnumerator()
        {
            throw new NotImplementedException();
        }

        public override int ElementCount { get; }
        
    }
}