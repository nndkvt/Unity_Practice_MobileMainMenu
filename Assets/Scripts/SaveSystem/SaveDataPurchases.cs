using System;
using System.Collections.Generic;

[Serializable]
public class SaveDataPurchases
{
    public List<bool> Characters;
    public List<bool> Locations;

    private static SaveDataPurchases _current;

    public static SaveDataPurchases Current
    {
        get
        {
            if (_current == null)
            {
                _current = SerializationManagerPurchases.Load();

                if (_current == null)
                {
                    _current = new SaveDataPurchases();
                }
            }

            return _current;
        }
    }

    public SaveDataPurchases()
    {
        Characters = new List<bool> { false, false, false};
        Locations = new List<bool> { false, false, false };
    }

    public void SomethingPurchased(PurchaseType purchaseType, int id)
    {
        switch (purchaseType)
        {
            case PurchaseType.Character:
                Characters[id] = true; break;

            case PurchaseType.Location:
                Locations[id] = true; break;
        }
    }
}
