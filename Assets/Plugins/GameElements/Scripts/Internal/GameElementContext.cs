using System.Collections.Generic;

namespace GameElements
{
    internal sealed class GameElementContext
    {
        private readonly IGameSystem gameSystem;

        private readonly HashSet<IGameElement> gameElements;

        private readonly List<IGameElement> cache;
        
        internal GameElementContext(IGameSystem gameSystem)
        {
            this.gameSystem = gameSystem;
            this.gameElements = new HashSet<IGameElement>();
            this.cache = new List<IGameElement>();
        }
        
        internal void AddElement(IGameElement element)
        {
            var addedElements = new HashSet<IGameElement>();
            this.AddRecursively(element, ref addedElements);
            
            foreach (var addedElement in addedElements)
            {
                this.ActivateElement(addedElement);
            }
        }

        internal void RemoveElement(IGameElement element)
        {
            this.RemoveRecursively(element);
        }
        
        internal void InitGame()
        {
            this.cache.Clear();
            this.cache.AddRange(this.gameElements);

            for (int i = 0, count = this.cache.Count; i < count; i++)
            {
                var element = this.cache[i];
                if (element is IGameInitElement initElement)
                {
                    initElement.InitGame(this.gameSystem);
                }
            }
        }

        internal void ReadyGame()
        {
            this.cache.Clear();
            this.cache.AddRange(this.gameElements);
            
            for (int i = 0, count = this.cache.Count; i < count; i++)
            {
                var element = this.cache[i];
                if (element is IGameReadyElement initElement)
                {
                    initElement.ReadyGame(this.gameSystem);
                }
            }
        }

        internal void StartGame()
        {
            this.cache.Clear();
            this.cache.AddRange(this.gameElements);
            
            for (int i = 0, count = this.cache.Count; i < count; i++)
            {
                var element = this.cache[i];
                if (element is IGameStartElement startElement)
                {
                    startElement.StartGame(this.gameSystem);
                }
            }
        }

        internal void PauseGame()
        {
            this.cache.Clear();
            this.cache.AddRange(this.gameElements);
            
            for (int i = 0, count = this.cache.Count; i < count; i++)
            {
                var element = this.cache[i];
                if (element is IGamePauseElement startElement)
                {
                    startElement.PauseGame(this.gameSystem);
                }
            }
        }

        internal void ResumeGame()
        {
            this.cache.Clear();
            this.cache.AddRange(this.gameElements);
            
            for (int i = 0, count = this.cache.Count; i < count; i++)
            {
                var element = this.cache[i];
                if (element is IGamePauseElement startElement)
                {
                    startElement.ResumeGame(this.gameSystem);
                }
            }
        }

        internal void FinishGame()
        {
            this.cache.Clear();
            this.cache.AddRange(this.gameElements);
            
            for (int i = 0, count = this.cache.Count; i < count; i++)
            {
                var element = this.cache[i];
                if (element is IGameFinishElement finishElement)
                {
                    finishElement.FinishGame(this.gameSystem);
                }
            }
        }

        private void AddRecursively(IGameElement element, ref HashSet<IGameElement> addedElements)
        {
            if (this.gameElements.Add(element))
            {
                addedElements.Add(element);
            }
            
            if (element is IGameElementGroup elementGroup)
            {
                foreach (var child in elementGroup.GetElements())
                {
                    this.AddRecursively(child, ref addedElements);
                }
            }
        }

        private void RemoveRecursively(IGameElement element)
        {
            this.gameElements.Remove(element);
            if (element is IGameElementGroup elementGroup)
            {
                foreach (var child in elementGroup.GetElements())
                {
                    this.RemoveRecursively(child);
                }
            }
        }
        
        private void ActivateElement(IGameElement element)
        {
            var gameState = this.gameSystem.State;
            if (gameState >= GameState.FINISH)
            {
                return;
            }

            if (gameState < GameState.INITIALIZED)
            {
                return;
            }

            if (element is IGameInitElement initElement)
            {
                initElement.InitGame(this.gameSystem);
            }

            if (gameState < GameState.READY)
            {
                return;
            }

            if (element is IGameReadyElement readyElement)
            {
                readyElement.ReadyGame(this.gameSystem);
            }
            
            if (gameState < GameState.PLAY)
            {
                return;
            }

            if (element is IGameStartElement startElement)
            {
                startElement.StartGame(this.gameSystem);
            }
            
            if (gameState == GameState.PAUSE && element is IGamePauseElement pauseElement)
            {
                pauseElement.PauseGame(this.gameSystem);
            }
        }
    }
}