using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using UnityEngine.Serialization;

public class CardUI : MonoBehaviour
{
    #region Fields and Properties

    [Header("Prefab Elements")] // 카드 프리팹 object들의 참조
    [SerializeField] private Image cardImage;
    [SerializeField] private Image characterImage;

    [SerializeField] private TextMeshProUGUI IDText;
    [SerializeField] private TextMeshProUGUI nameText;
    [SerializeField] private TextMeshProUGUI staminaText;
    [SerializeField] private TextMeshProUGUI speedText;
    [SerializeField] private TextMeshProUGUI efficiencyText;
    [SerializeField] private TextMeshProUGUI probabilityText;

    [SerializeField] private RectTransform rectTransform;
    [SerializeField] private CanvasGroup canvasGroup;

    [Header("Sprite Assets")] //속성별 카드 이미지 -> 이거 불러오는걸로 바꾸자
    [SerializeField] private Sprite passion;
    [SerializeField] private Sprite calm;
    [SerializeField] private Sprite wisdom;
   
    private Vector3 hoverScale = new Vector3(1.1f, 1.1f, 1);

    #endregion

    #region Methods

    // SetUIData 이걸로 변경
    public void Set(Minion _minion)
    {
        SetText(_minion.BaseData);
        SetCharacterImage(_minion.BaseData);
        SetTypeImage(_minion.BaseData);
    }
    
    /// <summary>
    /// 매개변수로 받은 state에 따라 카드 크기 및 알파값 조절
    /// </summary>
    public void UpdateState(CARD_STATE _state)
    {
        switch (_state)
        {
            case CARD_STATE.DEFAULT:
                rectTransform.localScale = new Vector3(1.0f, 1.0f, 1.0f);
                canvasGroup.alpha = 1.0f;
                return;
            case CARD_STATE.MOUSE_HOVER:
                rectTransform.localScale = hoverScale;
                return;
            case CARD_STATE.HIDE:
                canvasGroup.alpha = 0f;
                return;
        }
    }
    //스택킹 이미지 관련 -> 삭제
    // public void UpdateUIStacking(int _num)
    // {
    //     if(_num <= 1)
    //     {
    //         stackImage.SetActive(false);
    //     }
    //     else
    //     {
    //         stackImage.SetActive(true);
    //         stackNum.text = _num.ToString();
    //     }
    // }
    
    private void SetText(MinionBaseData _baseData)
    {
        IDText.text = _baseData.mid;
        nameText.text = _baseData.name;
        staminaText.text = _baseData.stamina.ToString();
        speedText.text = _baseData.speed.ToString();
        efficiencyText.text = _baseData.efficiency.ToString();
        probabilityText.text = _baseData.sGemProb.ToString();

    }

    private void SetCharacterImage(MinionBaseData _baseData)
    {
        Sprite[] characterImages = Resources.LoadAll<Sprite>("Character/CharacterImage");
        if (0 == characterImages.Length)
        {
            Debug.LogError($"캐릭터 이미지가 없음 imagePath: {characterImages}");
            return;
        }

        foreach (var image in characterImages)
        {
            if (image.name == _baseData.mid)
            {
                characterImage.sprite = image;
                return;
            }
        }

        Debug.LogError($"스프라이트가 없음. imageName : {_baseData.mid}");
    }
    private void SetTypeImage(MinionBaseData _baseData)
    {
        Sprite[] cardImages = Resources.LoadAll<Sprite>("Image/CardImage");
        if (0 == cardImages.Length)
        {
            Debug.LogError($"카드 이미지가 없음 imagePath: {cardImages}");
            return;
        }

        var type = _baseData.type switch
        {
            MinionEnums.TYPE.PASSION => "passion",
            MinionEnums.TYPE.CALM => "calm",
            MinionEnums.TYPE.WISDOM => "wisdom",
            _ => "unknown"
        };
        
        foreach (var image in cardImages.Where(_image => _image.name == type))
        {
            cardImage.sprite = image;
            return;
        }
        
        Debug.LogError($"존재하지 않는 타입: {_baseData.type}");
    }

    #endregion
}

public enum CARD_STATE
{
    DEFAULT,
    MOUSE_HOVER,
    HIDE
}

