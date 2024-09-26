using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

/// <summary>
/// �������� ī�� UI ����
/// </summary>
public class CardUI : MonoBehaviour
{
    #region Fields and Properties

    [Header("Prefab Elements")] // ī�� ������ object���� ����
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

    [Header("Sprite Assets")] //�Ӽ��� ī�� �̹���
    [SerializeField] private Sprite passion;
    [SerializeField] private Sprite calm;
    [SerializeField] private Sprite wisdom;
   
    private Vector3 hoverScale = new Vector3(1.1f, 1.1f, 1);
    

    #endregion

    #region Methods

    /// <summary>
    /// �Ű������� ���� state�� ���� ī�� ũ�� �� ���İ� ����
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
    /// ī�� ������ 1 �����̸� ����ŷ ǥ�ø� ���� 
    /// 2 �̻��̸� ����ŷ �̹����� ���ڿ� �Բ� ǥ��
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
    /// UI�� �ؽ�Ʈ, ĳ���� �̹���, Ÿ�Ժ� ��� �̹����� �Է¹��� ī�� ������ �°� ����
    /// </summary>
    /// <param name="_data"></param>
    public void SetUIData(CardData _data)
    {
        SetCardText(_data);
        SetCharacterImage(_data);
        SetTypeImage(_data);
    }


    private void SetCardText(CardData _data)
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

    private void SetCharacterImage(CardData _data)
    {
        Sprite[] characterImages = Resources.LoadAll<Sprite>("Character/CharacterImage");
        if (0 == characterImages.Length)
        {
            Debug.LogError($"ĳ���� �̹����� ���� imagePath: {characterImages}");
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

        Debug.LogError($"��������Ʈ�� ����. imageName : {_data.cid}");
        return;
    }
    private void SetTypeImage(CardData _data)
    {
        switch (_data.type)
        {
            case CARD_TYPE.PASSION:
                cardImage.sprite = passion;
                break;
            case CARD_TYPE.CALM:
                cardImage.sprite = calm;
                break;
            case CARD_TYPE.WISDOM:
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

