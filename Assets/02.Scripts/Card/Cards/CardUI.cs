using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class CardUI : MonoBehaviour
{
    #region Fields and Properties

    private Card _card;

    [Header("Prefab Elements")] // 카드 프리팹 object들의 참조
    [SerializeField] private Image _cardWorkerImage;
    [SerializeField] private Image _cardElementBackground;
    [SerializeField] private Image _cardRarity;
    [SerializeField] private Color _colorFire = new Color(255,0,78);
    [SerializeField] private Color _colorWater = new Color(62, 104, 255);
    [SerializeField] private Color _colorGrass = new Color(37, 183, 24);

    [SerializeField] private TextMeshProUGUI _cardNum;
    [SerializeField] private TextMeshProUGUI _cardName;
    [SerializeField] private TextMeshProUGUI _cardATK;
    [SerializeField] private TextMeshProUGUI _cardDEF;
    [SerializeField] private TextMeshProUGUI _cardSUP;

    [Header("Sprite Assets")]
    [SerializeField] private Sprite _rareRarityBackgroud;
    [SerializeField] private Sprite _epicRarityBackgroud;
    [SerializeField] private Sprite _legendaryRarityBackgroud;

    //ivate readonly string TYPE_Attack = "Attack";
    //ivate readonly string TYPE_Defence = "Defence";
    //ivate readonly string TYPE_Work = "Work";

    #endregion

    #region Methods

    private void Awake()
    {
        _card = GetComponent<Card>();

    }


    public void SetCardUI()
    {
        //카드가 널이 아니고 데이터가 입력되었는지 체크
        if(_card != null && _card.CardData != null)
        {
            SetCardTexts();
            SetRarity();
            SetElementColor();
            SetCardImage();
        }
    }

    public void SetCardMovingUI()
    {
        if (_card != null && _card.CardData != null)
        {
            
        }
    }

    private void SetCardTexts()
    {
        SetCardTypeText();

        _cardNum.text = _card.CardData.CardNum;
        _cardName.text = _card.CardData.CardName;
        _cardATK.text = _card.CardData.CardATK.ToString();
        _cardDEF.text = _card.CardData.CardDEF.ToString();
        _cardSUP.text = _card.CardData.CardSUP.ToString();

    }


    //이건 쓸지말지 고민(카드 종류가 여러개라면)
    private void SetCardTypeText()
    {
        switch (_card.CardData.Type)
        {
            case CardType.Attack:
                
                break;
            case CardType.Defence:

                break;
            case CardType.Support:

                break;
        }
    }

    private void SetRarity()
    {
        switch (_card.CardData.Rarity)
        {
            case CardRarity.Common:
                //별 x
                break;
            case CardRarity.Rare:
                _cardRarity.sprite = _rareRarityBackgroud;
                break;
            case CardRarity.Epic:
                _cardRarity.sprite = _epicRarityBackgroud;
                break;
            case CardRarity.Legendary:
                _cardRarity.sprite = _legendaryRarityBackgroud;
                break;
        }
    }

    private void SetElementColor()
    {
        switch (_card.CardData.Element)
        {
            case CardElement.Basic:

                break;
            case CardElement.Fire:
                _cardElementBackground.color = _colorFire;
                break;
            case CardElement.Water:
                _cardElementBackground.color = _colorWater;
                break;
            case CardElement.Grass:
                _cardElementBackground.color = _colorGrass;
                break;
        }
    }

    private void SetCardImage()
    {
        _cardWorkerImage.sprite = _card.CardData.Image;
    }

    #endregion
}
