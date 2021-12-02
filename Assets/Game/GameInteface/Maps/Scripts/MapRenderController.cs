using GameElements;
using GameElements.Unity;
using UnityEngine;

namespace Prototype.GameInterface
{
    public sealed class MapRenderController : MonoGameController
    {
        [SerializeField]
        private Transform plane;
        
        [SerializeField]
        private MapRenderConfig config;

        [Range(0.01f, 1.0f)]
        [SerializeField]
        private float renderPeriod = 0.25f;

        private float currentTime;

        private IMapRenderer mapRenderer;

        private void Awake()
        {
            this.enabled = false;
            this.mapRenderer = this.config.CreateRenderer();
        }

        protected override void OnSetuped(IGameSystem system)
        {
            base.OnSetuped(system);
            if (this.mapRenderer is IGameElement gameElement)
            {
                gameElement.Setup(system);
            }
        }

        protected override void OnStartGame(object sender)
        {
            base.OnStartGame(sender);
            
            this.mapRenderer.Render(this.plane);
            this.currentTime = this.renderPeriod;
            this.enabled = true;
        }

        private void LateUpdate()
        {
            this.currentTime -= Time.deltaTime;
            if (this.currentTime > 0)
            {
                return;
            }

            this.mapRenderer.Render(this.plane);
            this.currentTime += this.renderPeriod;
        }

        protected override void OnFinishGame(object sender)
        {
            base.OnFinishGame(sender);
            this.enabled = false;
        }
        
        protected override void OnDisposed()
        {
            base.OnDisposed();
            if (this.mapRenderer is IGameElement gameElement)
            {
                gameElement.Dispose();
            }
        }
    }
}