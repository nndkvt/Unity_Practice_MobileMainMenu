using System;
using System.Collections.Generic;

[Serializable]
public class SaveDataRewards
{
    public List<bool> RewardDays;
    public List<bool> ClaimedDays;
    public string LastClaimTime;

    private static SaveDataRewards _current;

    public static SaveDataRewards Current
    {
        get
        {
            if (_current == null)
            {
                _current = SerializationManagerRewards.Load();

                if (_current == null)
                {
                    _current = new SaveDataRewards();
                }
            }

            return _current;
        }
    }

    public SaveDataRewards()
    {
        RewardDays = new List<bool>() { true, false, false, false, false, false };
        ClaimedDays = new List<bool>() { false, false, false, false, false, false };
        LastClaimTime = "";
    }

    public void ResetRewards()
    {
        Current.RewardDays = new List<bool>() { true, false, false, false, false, false };
        Current.ClaimedDays = new List<bool>() { false, false, false, false, false, false };
        Current.LastClaimTime = "";
        SerializationManagerRewards.Save(Current);
    }

    public void SetNextRewardDay()
    {
        for (int i = 0; i < RewardDays.Count; i++)
        {
            if (RewardDays[i])
            {
                if (i + 1 !=  RewardDays.Count)
                {
                    Current.RewardDays[i + 1] = true;
                    Current.RewardDays[i] = false;
                    break;
                }
                else
                {
                    Current.RewardDays[0] = true;
                    Current.RewardDays[i] = false;
                    break;
                }
            }
        }
        SerializationManagerRewards.Save(Current);
    }

    public void RewardClaimed(int id)
    {
        Current.ClaimedDays[id] = true;
        SerializationManagerRewards.Save(Current);
    }

    public void SetLastTimeClaimed(string time)
    {
        Current.LastClaimTime = time;
        SerializationManagerRewards.Save(Current);
    }

    public int GetClaimedDaysCount()
    {
        int final = 0;

        for (int i = 0; i < Current.ClaimedDays.Count; i++)
        {
            if (Current.ClaimedDays[i])
                final++;
        }

        return final;
    }
}
