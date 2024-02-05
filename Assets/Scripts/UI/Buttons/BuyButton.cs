using TMPro;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class BuyButton : MonoBehaviour, IPointerClickHandler
{
    [SerializeField] private PurchaseInfo _purchaseInfo;
    [SerializeField] private Image _boughtImage;
    [SerializeField] private TextMeshProUGUI _text;

    [SerializeField] private Image _canBuyImg;
    [SerializeField] private Image _cannotBuyImg;

    [SerializeField] private TextMeshProUGUI _requieredLevelText;

    private bool _isBought;
    private bool _canBuy;

    private void OnEnable()
    {
        SaveDataUpdate.LevelUpdated += SetCanBuy;

        SetInfo();
    }

    private void OnDisable()
    {
        SaveDataUpdate.LevelUpdated -= SetCanBuy;
    }

    public void OnPointerClick(PointerEventData eventData)
    {
        TryBuy();
    }

    private void SetInfo()
    {
        _text.text = _purchaseInfo.price.ToString() + " tkts";
        _requieredLevelText.text = "LV. " + _purchaseInfo.requiredLevel.ToString();
        SetIsBought();
        SetCanBuy();
    }

    private void TryBuy()
    {
        if (SaveData.Current.Tickets >= _purchaseInfo.price && !_isBought && _canBuy)
        {
            SaveData.Current.Tickets -= _purchaseInfo.price;
            SerializationManagerPlayer.Save(SaveData.Current);

            SaveDataPurchases.Current.SomethingPurchased(_purchaseInfo.type, _purchaseInfo.id);
            SerializationManagerPurchases.Save(SaveDataPurchases.Current);
            SetIsBought();

            SaveDataUpdate.TicketsUpdated?.Invoke(SaveData.Current.Tickets);
        }
    }

    private void CheckIsBought()
    {
        _text.gameObject.SetActive(!_isBought);
        _boughtImage.gameObject.SetActive(_isBought);
    }

    private void CheckCanBuy()
    {
        _canBuyImg.gameObject.SetActive(_canBuy);
        _cannotBuyImg.gameObject.SetActive(!_canBuy);
    }

    private void SetIsBought()
    {
        switch (_purchaseInfo.type)
        {
            case PurchaseType.Character:
                _isBought = SaveDataPurchases.Current.Characters[_purchaseInfo.id];
                break;

            case PurchaseType.Location:
                _isBought = SaveDataPurchases.Current.Locations[_purchaseInfo.id];
                break;

            default:
                break;
        }

        CheckIsBought();
    }

    private void SetCanBuy()
    {
        _canBuy = SaveData.Current.Level > _purchaseInfo.requiredLevel;
        CheckCanBuy();
    }
}
