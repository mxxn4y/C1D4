using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

/// <summary>
/// 보여지는 카드 UI 제어
/// </summary>
public class CardUI : MonoBehaviour
{
    #region Fields and Properties

    private Card _card;

    [Header("Prefab Elements")] // 카드 프리팹 object들의 참조
    [SerializeField] private Image _cardImage;
    [SerializeField] private Image _characterImage;
    [SerializeField] private GameObject _stackImage;

    [SerializeField] private TextMeshProUGUI _IDText;
    [SerializeField] private TextMeshProUGUI _nameText;
    [SerializeField] private TextMeshProUGUI _staminaText;
    [SerializeField] private TextMeshProUGUI _attackText;
    [SerializeField] private TextMeshProUGUI _defenceText;
    [SerializeField] private TextMeshProUGUI _speedText;
    [SerializeField] private TextMeshProUGUI _amountText;
    [SerializeField] private TextMeshProUGUI _probabilityText;
    [SerializeField] private TextMeshProUGUI _stackNum;

    [Header("Sprite Assets")] //속성별 카드 이미지
    [SerializeField] private Sprite _passion;
    [SerializeField] private Sprite _calm;
    [SerializeField] private Sprite _wisdom;

    public CARD_UI_STATE cardUIState = CARD_UI_STATE.DEFAULT;
    private Vector3 hoverValue = new Vector3(0.6f, 0.6f, 1);

    #endregion

    #region Methods

    private void Awake()
    {
        _card = GetComponent<Card>();

    }
    private void Start()
    {
        SetCardTexts();
        SetCardImage();
    }

    /// <summary>
    /// 매개변수로 받은 state에 따라 카드 크기 및 알파값 조절
    /// </summary>
    /// <param name="state"></param>
    public void SetCardUI(CARD_UI_STATE state)
    {
        //카드가 널이 아니고 데이터가 입력되었는지 체크
        if(_card != null)
        {
            switch (state)
            {
                case CARD_UI_STATE.DEFAULT:
                    GetComponent<CanvasGroup>().alpha = 1.0f;
                    GetComponent<RectTransform>().localScale = new Vector3(0.5f, 0.5f, 0.5f);
                    return;
                case CARD_UI_STATE.MOUSE_HOVER:
                    GetComponent<RectTransform>().localScale = hoverValue;
                    return;
                case CARD_UI_STATE.MOVING:
                    GetComponent<CanvasGroup>().alpha = 0.5f;
                    return;
            }

        }
    }

    public void ActivateStacking(int num)
    {
        if(num <= 1)
        {
            _stackImage.SetActive(false);
        }
        else
        {
            _stackImage.SetActive(true);
            _stackNum.text = num.ToString();
        }
    }

    private void SetCardTexts()
    {
        _IDText.text = _card._data._cid;
        _nameText.text = _card._data._name;
        _staminaText.text = _card._data._stamina.ToString();
        _attackText.text = _card._data._attack.ToString();
        _defenceText.text = _card._data._defence.ToString();
        _speedText.text = _card._data._productSpeed.ToString();
        _amountText.text = _card._data._productYield.ToString();
        _probabilityText.text = _card._data._goodsProbability.ToString();

    }

    private void SetCardImage()
    {
        Sprite[] characterImages = Resources.LoadAll<Sprite>("Character/CharacterImage");
        if (0 == characterImages.Length)
        {
            Debug.LogError($"캐릭터 이미지가 없음 imagePath: {characterImages}");
            return;
        }

        foreach (var image in characterImages)
        {
            if (image.name == _card._data._cid)
            {
                _characterImage.sprite = image;
                return;
            }
        }

        Debug.LogError($"스프라이트가 없음. imageName : {_card._data._cid}");
        return;
    }

    #endregion
}

public enum CARD_UI_STATE
{
    DEFAULT,
    MOUSE_HOVER,
    MOVING
}
