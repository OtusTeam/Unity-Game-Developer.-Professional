using GameElements;
using GameElements.Unity;
using UnityEngine;

namespace Prototype.GameInterface
{
    public sealed class MapRenderController : MonoGameController
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

        protected override void OnBindGame(IGameSystem system)
        {
            if (this.mapRenderer is IGameElement gameElement)
            {
                gameElement.BindGame(system);
            }
        }
        
        protected override void OnStartGame()
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

        protected override void OnFinishGame()
        {
            base.OnFinishGame();
            this.enabled = false;
        }

        protected override void OnUnbindGame()
        {
            if (this.mapRenderer is IGameElement gameElement)
            {
                gameElement.UnbindGame();
            }

            foreach (Transform child in this.layer)
            {
                Destroy(child.gameObject);
            }
        }
    }
}