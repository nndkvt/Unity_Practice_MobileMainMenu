using UnityEngine.Events;

public static class SaveDataUpdate
{
    public static UnityAction LevelUpdated;
    public static UnityAction<int> TicketsUpdated;
    public static UnityAction RewardUpdated;
}
