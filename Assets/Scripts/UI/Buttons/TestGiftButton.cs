using UnityEngine;
using UnityEngine.EventSystems;

public class TestGiftButton : MonoBehaviour, IPointerClickHandler
{
    public void OnPointerClick(PointerEventData eventData)
    {
        SaveData.Current.Tickets += 10;
        SerializationManagerPlayer.Save(SaveData.Current);

        SaveDataUpdate.TicketsUpdated?.Invoke(SaveData.Current.Tickets);
    }
}
