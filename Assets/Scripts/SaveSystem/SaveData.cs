using System;

[Serializable]
public class SaveData
{
    public int Level { get; set; }
    public int Tickets { get; set; }

    private static SaveData _current;

    public static SaveData Current 
    { 
        get 
        {
            if (_current == null)
            {
                _current = SerializationManagerPlayer.Load();

                if (_current == null)
                {
                    _current = new SaveData();
                }
            }

            return _current; 
        } 
    }

    public SaveData()
    {
        Level = 1;
        Tickets = 0;
    }

    public void AddTickets(int tickets)
    {
        Current.Tickets += tickets;
    }
}
