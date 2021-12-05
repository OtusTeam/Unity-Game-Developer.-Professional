using System;
using System.Collections.Generic;
using UnityEngine;

namespace GameElements.Unity
{
    public sealed class MonoGameElementDirectory : MonoBehaviour, IGameElementGroup
    {
        [SerializeField]
        private Transform[] containers;

        private HashSet<IGameElement> elements;

        public IEnumerable<IGameElement> GetElements()
        {
            return this.elements;
        }

        private void Awake()
        {
            this.elements = new HashSet<IGameElement>();
            this.LoadElements();
        }

        private void LoadElements()
        {
            foreach (var container in this.containers)
            {
                foreach (Transform child in container)
                {
                    var gameElements = child.GetComponents<IGameElement>();
                    foreach (var element in gameElements)
                    {
                        this.elements.Add(element);
                    }
                }
            }
        }
    }
}