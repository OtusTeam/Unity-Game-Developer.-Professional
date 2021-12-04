using System.Collections.Generic;

namespace GameElements
{
    public interface IGameElement
    {
    }
    
    public interface IGameElementGroup : IGameElement, IEnumerable<IGameElement>
    {
    }
    
    public interface IGameInitElement : IGameElement
    {
        void InitGame(IGameSystem gameSystem);
    }
    
    public interface IGameReadyElement : IGameElement
    {
        void ReadyGame(IGameSystem gameSystem);
    }

    public interface IGameStartElement : IGameElement
    {
        void StartGame(IGameSystem gameSystem);
    }

    public interface IGamePauseElement : IGameElement
    {
        void PauseGame(IGameSystem gameSystem);

        void ResumeGame(IGameSystem gameSystem);
    }

    public interface IGameFinishElement
    {
        void FinishGame(IGameSystem gameSystem);
    }

    public interface IGameDisposeElement : IGameElement
    {
        void DisposeGame(IGameSystem gameSystem);
    }
}