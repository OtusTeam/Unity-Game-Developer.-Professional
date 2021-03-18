namespace Foundation
{
    public interface IQuestManager
    {
        ObserverList<IOnQuestStarted> OnQuestStarted { get; }
        ObserverList<IOnQuestCompleted> OnQuestCompleted { get; }
        ObserverList<IOnQuestFailed> OnQuestFailed { get; }

        void StartQuest(Quest quest);
    }
}
