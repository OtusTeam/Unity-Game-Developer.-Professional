using System;
using GameElements.Unity;
using UnityEngine;

namespace Prototype.GUI
{
    public sealed class MapRenderController : MonoGameController
    {
        private IMapRenderer mapRenderer;

        [SerializeField]
        private Parameters parameters;
        
        private void Awake()
        {
            this.mapRenderer = this.parameters.mapRenderer.GetComponent<IMapRenderer>();
            this.enabled = false;
        }

        protected override void OnReadyGame(object sender)
        {
            base.OnReadyGame(sender);
            this.enabled = true;
        }

        private void LateUpdate()
        {
            this.mapRenderer.Render(this.parameters.mapPlane);
        }

        protected override void OnFinishGame(object sender)
        {
            base.OnFinishGame(sender);
            this.enabled = false;
        }

        [Serializable]
        public sealed class Parameters
        {
            [SerializeField]
            public Transform mapPlane;
            
            [SerializeField]
            public GameObject mapRenderer;
        }
    }
}