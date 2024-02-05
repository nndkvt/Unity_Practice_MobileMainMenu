using TMPro;
using UnityEngine;
using UnityEngine.Events;

public class TicketCount : MonoBehaviour
{
    private TextMeshProUGUI _counter;

    public UnityAction<int> TicketsUpdated;

    private void OnEnable()
    {
        SaveDataUpdate.TicketsUpdated += UpdateTicketsOnGUI;

        _counter = GetComponent<TextMeshProUGUI>();

        SaveData save = SerializationManagerPlayer.Load();
        UpdateTicketsOnGUI(save.Tickets);
    }

    private void OnDisable()
    {
        SaveDataUpdate.TicketsUpdated -= UpdateTicketsOnGUI;
    }

    private void UpdateTicketsOnGUI(int tickets)
    {
        _counter.text = tickets.ToString();
    }
}
