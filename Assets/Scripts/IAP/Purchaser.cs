using UnityEngine;
using UnityEngine.Purchasing;

public class Purchaser : MonoBehaviour
{
    public void OnPurchaseCompleted(Product product)
    {
        switch (product.definition.id)
        {
            case "com.testtask.iap.tickets.500":
                AddTickets(500);
                break;

            case "com.testtask.iap.tickets.1200":
                AddTickets(1200);
                break;
        }
    }

    private void AddTickets(int tickets)
    {
        SaveData.Current.Tickets += tickets;
        SerializationManagerPlayer.Save(SaveData.Current);
        SaveDataUpdate.TicketsUpdated?.Invoke(SaveData.Current.Tickets);
    }
}
