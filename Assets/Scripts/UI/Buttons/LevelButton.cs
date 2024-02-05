using TMPro;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class LevelButton : MonoBehaviour, IPointerClickHandler
{
    [SerializeField] private int _levelNum;
    [SerializeField] private TextMeshProUGUI _text;
    [SerializeField] private Image _lockedImage;

    private bool _isLocked = true;
    private bool _isPassed;

    public void OnPointerClick(PointerEventData eventData)
    {
        if (!_isPassed && !_isLocked)
        {
            _isPassed = true;

            SaveData.Current.Level++;
            SerializationManagerPlayer.Save(SaveData.Current);
            SaveDataUpdate.LevelUpdated?.Invoke();
        }
        else if (_isPassed)
        {
            Debug.Log("Level is passed");
        }
    }

    private void OnEnable()
    {
        SaveDataUpdate.LevelUpdated += UpdateLocked;

        _text.text = _levelNum.ToString();

        UpdateLocked();
    }

    private void OnDisable()
    {
        SaveDataUpdate.LevelUpdated -= UpdateLocked;
    }

    private bool CheckLevel()
    {
        return SaveData.Current.Level < _levelNum;
    }

    private bool CheckIfPassed()
    {
        return SaveData.Current.Level > _levelNum && !_isLocked;
    }

    private void UpdateLocked()
    {
        _isLocked = CheckLevel();
        _isPassed = CheckIfPassed();
        _text.gameObject.SetActive(!_isLocked);
        _lockedImage.gameObject.SetActive(_isLocked);
    }
}
