using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class LastReward : MonoBehaviour
{
    [SerializeField] private Image _fillBar;
    [SerializeField] private TextMeshProUGUI _fillCounter;
    [SerializeField] private Button _rewardButton;
    [SerializeField] private TextMeshProUGUI _rewardText;
    [SerializeField] private int _rewardTickets;

    private void OnEnable()
    {
        SaveDataUpdate.RewardUpdated += UpdateFillBar;
        UpdateFillBar();
    }

    private void OnDisable()
    {
        SaveDataUpdate.RewardUpdated -= UpdateFillBar;
    }

    private void UpdateFillBar()
    {
        int allRewards = SaveDataRewards.Current.ClaimedDays.Count;
        int claimedRewards = SaveDataRewards.Current.GetClaimedDaysCount();

        _fillCounter.text = claimedRewards.ToString() + "/" + allRewards.ToString();
        _fillBar.fillAmount = (float)claimedRewards / allRewards;

        if (claimedRewards == allRewards)
        {
            _rewardButton.gameObject.SetActive(true);
        }
        else
        {
            _rewardButton.gameObject.SetActive(false);
        }

        _rewardText.text = "x" + _rewardTickets.ToString();
    }

    public void GiveReward()
    {
        SaveData.Current.Tickets += _rewardTickets;
        SerializationManagerPlayer.Save(SaveData.Current);
        SaveDataUpdate.TicketsUpdated(SaveData.Current.Tickets);

        SaveDataRewards.Current.ResetRewards();
        SaveDataUpdate.RewardUpdated?.Invoke();
    }
}
