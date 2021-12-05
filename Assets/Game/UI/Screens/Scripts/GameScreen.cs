using Prototype.GameManagment;
using UnityEngine;

namespace Prototype.UI
{
    //TODO: Скрипт пример
    public sealed class GameScreen : MonoBehaviour
    {
        private const string GAME_INTERFACE = "GameInterface";
        
        [SerializeField]
        private GameManager gameManager;

        private void OnEnable()
        {
            this.gameManager.OnGameLoaded += this.OnGameLoaded;
        }
        
        private void Start()
        {
            this.gameManager.LoadGame();
        }
        
        private void OnGameLoaded()
        {
            this.LoadGameInterface();
            this.gameManager.StartGame();
        }

        private void OnDisable()
        {
            this.gameManager.OnGameLoaded -= this.OnGameLoaded;
        }
        
        private void LoadGameInterface()
        {
            var prefab = Resources.Load<GameObject>(GAME_INTERFACE);
            var gameInteface = Instantiate(prefab, this.transform);
            var gameComponents = gameInteface.GetComponents<MonoBehaviour>();
            foreach (var component in gameComponents)
            {
                this.gameManager.AddGameComponent(component);
            }
        }
    }
}