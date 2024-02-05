using System;

[Serializable]
public struct PurchaseInfo
{
    public PurchaseType type;
    public int id;
    public int price;
    public int requiredLevel;
}

public enum PurchaseType
{
    Character,
    Location,
}
