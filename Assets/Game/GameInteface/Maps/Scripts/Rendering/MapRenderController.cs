using GameElements;
using UnityEngine;

namespace Prototype.GameInterface
{
    public sealed class MapRenderController : MonoBehaviour, 
        IGameInitElement,
        IGameStartElement,
        IGameFinishElement
    {
        [SerializeField]
        private RectTransform layer;

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

        void IGameInitElement.InitGame(IGameSystem system)
        {
            if (this.mapRenderer is IGameElement gameElement)
            {
                system.AddElement(gameElement);
            }
        }

        void IGameStartElement.StartGame(IGameSystem system)
        {
            this.mapRenderer.Render(this.layer);
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

            this.mapRenderer.Render(this.layer);
            this.currentTime += this.renderPeriod;
        }

        void IGameFinishElement.FinishGame(IGameSystem system)
        {
            this.enabled = false;
        }
    }
}