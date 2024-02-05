using System;
using TMPro;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class RewardButton : MonoBehaviour, IPointerClickHandler
{
    [SerializeField] private int _day;
    [SerializeField] private int _rewardTickets;

    [SerializeField] private TextMeshProUGUI _dayText;
    [SerializeField] private TextMeshProUGUI _rewardText;
    [SerializeField] private Image _claimedImage;
    [SerializeField] private TextMeshProUGUI _whenNextRewardText;

    private Button _rewardButton;

    public void OnPointerClick(PointerEventData eventData)
    {
        TryGetReward();
    }

    private void OnEnable()
    {
        SaveDataUpdate.RewardUpdated += SetButtonView;

        _whenNextRewardText.text = "";
        _rewardButton = GetComponent<Button>();

        HasMuchTimePassed();
        SetButtonView();
    }

    private void OnDisable()
    {
        SaveDataUpdate.RewardUpdated -= SetButtonView;
    }

    private void TryGetReward()
    {
        if (CanGetReward())
        {
            ClaimReward();
            _whenNextRewardText.text = "";
        }
        else
        {
            WhyNoReward();
        }
    }

    private void ClaimReward()
    {
        SaveData.Current.Tickets += _rewardTickets;
        SerializationManagerPlayer.Save(SaveData.Current);
        SaveDataUpdate.TicketsUpdated?.Invoke(SaveData.Current.Tickets);

        _rewardButton.interactable = false;
        SaveDataRewards.Current.SetNextRewardDay();
        SaveDataRewards.Current.RewardClaimed(_day - 1);
        SaveDataRewards.Current.SetLastTimeClaimed(DateTime.Now.ToString());

        SaveDataUpdate.RewardUpdated?.Invoke();
    }

    private bool CanGetReward()
    {
        bool isTodayRewardDay = SaveDataRewards.Current.RewardDays[_day - 1];
        bool isRewardTaken = SaveDataRewards.Current.ClaimedDays[_day - 1];

        string lastClaim = SaveDataRewards.Current.LastClaimTime;

        DateTime lastClaimTime = StringToDateTime(lastClaim);

        bool hasTimePassed = DateTime.Today > lastClaimTime;

        return hasTimePassed && isTodayRewardDay && !isRewardTaken;
        //return isTodayRewardDay && !isRewardTaken;
    }

    private void SetButtonView()
    {
        _dayText.text = "Day" + _day.ToString();
        _rewardText.text = "x" + _rewardTickets.ToString();

        bool isRewardTaken = SaveDataRewards.Current.ClaimedDays[_day - 1];

        if (isRewardTaken)
        {
            _rewardButton.interactable = false;
            _claimedImage.gameObject.SetActive(true);
        }
        else
        {
            _rewardButton.interactable = true;
            _claimedImage.gameObject.SetActive(false);
        }
    }

    private void HasMuchTimePassed()
    {
        string lastClaim = SaveDataRewards.Current.LastClaimTime;

        DateTime lastClaimTime = StringToDateTime(lastClaim);

        double daysPassed = (DateTime.Today - lastClaimTime).TotalDays;

        bool isFirstDayRewardDay = SaveDataRewards.Current.RewardDays[0];

        if ((daysPassed > 1) && !isFirstDayRewardDay)
        {
            SaveDataRewards.Current.ResetRewards();
        }
    }

    private void WhyNoReward()
    {
        if (!SaveDataRewards.Current.ClaimedDays[_day - 1] && SaveDataRewards.Current.RewardDays[_day - 1])
        {
            int hours = Mathf.FloorToInt((float)(DateTime.Today.AddDays(1) - DateTime.Now).TotalHours);
            int minutes = Mathf.FloorToInt((float)(DateTime.Today.AddDays(1) - DateTime.Now).TotalMinutes) % 60;

            _whenNextRewardText.text = hours + "h. " + minutes + "m. till next reward";
        }
        else if (SaveDataRewards.Current.ClaimedDays[_day - 1])
        {
            _whenNextRewardText.text = "Reward already claimed";
        }
        else if (!SaveDataRewards.Current.ClaimedDays[_day - 1] && !SaveDataRewards.Current.RewardDays[_day - 1])
        {
            _whenNextRewardText.text = "Collect previous rewards first";
        }
    }

    private DateTime StringToDateTime(string str)
    {
        if (!string.IsNullOrEmpty(str))
        {
            return DateTime.Parse(str);
        }
        else
        {
            return DateTime.MinValue;
        }
    }
}
