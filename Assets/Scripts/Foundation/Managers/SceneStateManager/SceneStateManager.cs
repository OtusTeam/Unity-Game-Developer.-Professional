using UnityEngine;
using System.Collections.Generic;

namespace Foundation
{
    sealed class SceneStateManager : AbstractService<ISceneStateManager>, ISceneStateManager
    {
        public List<SceneState> InitialStates;

        public ISceneState CurrentState { get; private set; }
        readonly List<ISceneState> states = new List<ISceneState>();
        
        //Оптимизация
        readonly List<ISceneState> statesCache = new List<ISceneState>(); 
        bool statesListChanged;

        new void Start()
        {
            foreach (var state in InitialStates)
                Push(state);
        }

        public void Push(ISceneState state)
        {
            DebugOnly.Check(!states.Contains(state), $"GameState is already on the stack.");

            states.Add(state);
            statesListChanged = true;
            (state as ISceneStateInternal)?.InternalActivate(); // Паттерн мост..
        }

        public void Pop(ISceneState state)
        {
            statesListChanged = true;
            states.Remove(state);

            if (CurrentState == state) {
                (CurrentState as ISceneStateInternal)?.InternalResignTopmost();
                CurrentState = null;
            }

            (state as ISceneStateInternal)?.InternalDeactivate();

            DebugOnly.Check(states.Count != 0, "GameState stack is empty.");
        }

        IEnumerable<ISceneState> CachedGameStates()
        {
            int n;
            
            //Если список не менялся, то ничего не делаем
            if (!statesListChanged)
                n = statesCache.Count;
            
            //Иначе меняем 
            else {
                statesListChanged = false;
                statesCache.Clear();
                statesCache.AddRange(states);
                n = statesCache.Count;

                if (n == 0) {
                    
                    //Если список стал пустым, то убираем текущее состояние
                    if (CurrentState != null) {
                        (CurrentState as ISceneStateInternal)?.InternalResignTopmost();
                        CurrentState = null;
                    }
                    
                    
                } else {

                    //Обновляем текущее состояние, если оно перестало быть в вершине стека
                    if (CurrentState != statesCache[n - 1]) {
                        var oldState = CurrentState;
                        CurrentState = statesCache[n - 1];
                        (CurrentState as ISceneStateInternal)?.InternalBecomeTopmost();
                        if (oldState != null)
                            (oldState as ISceneStateInternal)?.InternalResignTopmost();
                    }
                }

                //Переставляем порядок соритировки:
                int index = 0;
                foreach (var it in statesCache)
                    (it as ISceneStateInternal)?.InternalSetSortingOrder(index++);
            }

            //Итерируемся по списку с конца!!!
            while (n-- > 0) {
                var state = statesCache[n];
                yield return state;
            }
        }

        void Update()
        {
            float timeDelta = Time.deltaTime;
            bool update = true;

            //Идем от верхнего элемента (последнего) к нижнему (первому)
            foreach (var state in CachedGameStates()) {
                
                //Если можно апдейтить состояние
                if (update) {
                    
                    foreach (var ticker in state.OnUpdate.Enumerate())
                        ticker.Do(timeDelta);
                } 
                
                //Апдейтим в паузе
                else {
                    foreach (var ticker in state.OnUpdateDuringPause.Enumerate())
                        ticker.Do(timeDelta);
                }

                update = update && state.UpdateParentState;
            }
        }

        void FixedUpdate()
        {
            foreach (var state in CachedGameStates()) {
                foreach (var ticker in state.OnFixedUpdate.Enumerate())
                    ticker.Do();
                if (!state.UpdateParentState)
                    break;
            }
        }

        void LateUpdate()
        {
            float timeDelta = Time.deltaTime;
            foreach (var state in CachedGameStates()) {
                foreach (var ticker in state.OnLateUpdate.Enumerate())
                    ticker.Do(timeDelta);
                if (!state.UpdateParentState)
                    break;
            }
        }
    }
}
