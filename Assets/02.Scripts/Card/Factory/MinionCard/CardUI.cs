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

    [Header("Prefab Elements")] // 카드 프리팹 object들의 참조
    [SerializeField] private Image cardImage;
    [SerializeField] private Image characterImage;
    [SerializeField] private GameObject stackImage;

    [SerializeField] private TextMeshProUGUI IDText;
    [SerializeField] private TextMeshProUGUI nameText;
    [SerializeField] private TextMeshProUGUI staminaText;
    [SerializeField] private TextMeshProUGUI attackText;
    [SerializeField] private TextMeshProUGUI defenceText;
    [SerializeField] private TextMeshProUGUI speedText;
    [SerializeField] private TextMeshProUGUI amountText;
    [SerializeField] private TextMeshProUGUI probabilityText;
    [SerializeField] private TextMeshProUGUI stackNum;

    [SerializeField] private RectTransform rectTransform;
    [SerializeField] private CanvasGroup canvasGroup;

    [Header("Sprite Assets")] //속성별 카드 이미지
    [SerializeField] private Sprite passion;
    [SerializeField] private Sprite calm;
    [SerializeField] private Sprite wisdom;
   
    private Vector3 hoverScale = new Vector3(1.1f, 1.1f, 1);
    

    #endregion

    #region Methods

    /// <summary>
    /// 매개변수로 받은 state에 따라 카드 크기 및 알파값 조절
    /// </summary>
    /// <param name="_state"></param>
    public void SetUIState(CARD_STATE _state)
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
    /// <summary>
    /// 카드 수량이 1 이하이면 스택킹 표시를 끄고 
    /// 2 이상이면 스택킹 이미지를 숫자와 함께 표시
    /// </summary>
    /// <param name="_num"></param>
    public void UpdateUIStacking(int _num)
    {
        if(_num <= 1)
        {
            stackImage.SetActive(false);
        }
        else
        {
            stackImage.SetActive(true);
            stackNum.text = _num.ToString();
        }
    }
    /// <summary>
    /// UI의 텍스트, 캐릭터 이미지, 타입별 배경 이미지를 입력받은 카드 정보에 맞게 설정
    /// </summary>
    /// <param name="_data"></param>
    public void SetUIData(MinionStructs.CardData _data)
    {
        SetCardText(_data);
        SetCharacterImage(_data);
        SetTypeImage(_data);
    }


    private void SetCardText(MinionStructs.CardData _data)
    {
        IDText.text = _data.cid;
        nameText.text = _data.name;
        staminaText.text = _data.stamina.ToString();
        attackText.text = _data.attack.ToString();
        defenceText.text = _data.defence.ToString();
        speedText.text = _data.produceSpeed.ToString();
        amountText.text = _data.productYield.ToString();
        probabilityText.text = _data.goodsProbability.ToString();

    }

    private void SetCharacterImage(MinionStructs.CardData _data)
    {
        Sprite[] characterImages = Resources.LoadAll<Sprite>("Character/CharacterImage");
        if (0 == characterImages.Length)
        {
            Debug.LogError($"캐릭터 이미지가 없음 imagePath: {characterImages}");
            return;
        }

        foreach (var image in characterImages)
        {
            if (image.name == _data.cid)
            {
                characterImage.sprite = image;
                return;
            }
        }

        Debug.LogError($"스프라이트가 없음. imageName : {_data.cid}");
        return;
    }
    private void SetTypeImage(MinionStructs.CardData _data)
    {
        switch (_data.type)
        {
            case MinionEnums.MINION_TYPE.PASSION:
                cardImage.sprite = passion;
                break;
            case MinionEnums.MINION_TYPE.CALM:
                cardImage.sprite = calm;
                break;
            case MinionEnums.MINION_TYPE.WISDOM:
                cardImage.sprite = wisdom;
                break;
        }
    }

    #endregion
}

public enum CARD_STATE
{
    DEFAULT,
    MOUSE_HOVER,
    HIDE
}

