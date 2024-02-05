using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.Events;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class MuteSoundButton : MonoBehaviour, IPointerClickHandler
{
    [SerializeField] private Image _buttonImage;
    [SerializeField] private Sprite _soundOnSprite;
    [SerializeField] private Sprite _soundOffSprite;

    [SerializeField] private AudioMixer _audioSet;
    [SerializeField] private string _settingsName;

    private float _defaultVolumeLevel;
    private bool _isSoundOff = false;
    private UnityAction AudioChanged;

    private void Awake()
    {
        if (_audioSet.GetFloat(_settingsName, out float volumeLevel))
        {
            _defaultVolumeLevel = volumeLevel;
        }
    }

    private void OnEnable()
    {
        AudioChanged += SetAudio;
    }

    private void OnDisable()
    {
        AudioChanged -= SetAudio;
    }

    public void OnPointerClick(PointerEventData eventData)
    {
        _isSoundOff = !_isSoundOff;
        AudioChanged?.Invoke();
    }

    private void SetAudio()
    {
        if (_isSoundOff)
        {
            _buttonImage.sprite = _soundOffSprite;
            _audioSet.SetFloat(_settingsName, -80.0f);
        }
        else
        {
            _buttonImage.sprite = _soundOnSprite;
            _audioSet.SetFloat(_settingsName, _defaultVolumeLevel);
        }
    }
}