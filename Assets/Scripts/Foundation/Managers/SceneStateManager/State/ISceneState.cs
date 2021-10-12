namespace Foundation
{
    public interface ISceneState
    {
        bool IsTopmost { get; } // Находится в вершине стека
        bool IsVisible { get; } // Активный
        int SortingOrder { get; }

        bool UpdateParentState { get; } //Можно ли родительскому классу апдейтить другие состояния

        ObserverList<IOnUpdate> OnUpdate { get; }
        ObserverList<IOnUpdateDuringPause> OnUpdateDuringPause { get; }
        ObserverList<IOnFixedUpdate> OnFixedUpdate { get; }
        ObserverList<IOnLateUpdate> OnLateUpdate { get; }

        ObserverList<IOnStateActivate> OnActivate { get; }
        ObserverList<IOnStateDeactivate> OnDeactivate { get; }

        ObserverList<IOnStateBecomeTopmost> OnBecomeTopmost { get; }
        ObserverList<IOnStateResignTopmost> OnResignTopmost { get; }

        ObserverList<IOnStateSortingOrderChanged> OnSortingOrderChanged { get; }
    }
}