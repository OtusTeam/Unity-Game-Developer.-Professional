using Prototype.GameManagment;
using UnityEngine;

namespace Prototype.UI
{
    //TODO: Скрипт пример
    public sealed class GameScreen : MonoBehaviour
    {
        private const string 
        
        [SerializeField]
        private GameManager gameManager;

        private void OnEnable()
        {
            this.gameManager.OnGameLoaded += this.OnGameLoaded;
            this.gameManager.OnGameStarted += this.OnGameStarted;
        }

        private void OnDisable()
        {
            this.gameManager.OnGameLoaded -= this.OnGameLoaded;
            this.gameManager.OnGameStarted -= this.OnGameStarted;
        }

        private void OnGameLoaded()
        {
            var gameInteface = Instantiate(this.gameInterfacePrefab, this.transform);
            
        }

        private void OnGameStarted()
        {
            
        }

        private void Start()
        {
            
        }
    }
}