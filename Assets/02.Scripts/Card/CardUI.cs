using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using static UnityEditorInternal.ReorderableList;

/// <summary>
/// �������� ī�� UI ����
/// </summary>
public class CardUI : MonoBehaviour
{
    #region Fields and Properties

    [Header("Prefab Elements")] // ī�� ������ object���� ����
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

    [SerializeField] private RectTransform _rectTransform;
    [SerializeField] private CanvasGroup _canvasGroup;

    [Header("Sprite Assets")] //�Ӽ��� ī�� �̹���
    [SerializeField] private Sprite _passion;
    [SerializeField] private Sprite _calm;
    [SerializeField] private Sprite _wisdom;
   
    private Vector3 _hoverScale = new Vector3(1.1f, 1.1f, 1);
    

    #endregion

    #region Methods

    /// <summary>
    /// �Ű������� ���� state�� ���� ī�� ũ�� �� ���İ� ����
    /// </summary>
    /// <param name="state"></param>
    public void SetUIState(CARD_STATE state)
    {
        switch (state)
        {
            case CARD_STATE.DEFAULT:
                _rectTransform.localScale = new Vector3(1.0f, 1.0f, 1.0f);
                _canvasGroup.alpha = 1.0f;
                return;
            case CARD_STATE.MOUSE_HOVER:
                _rectTransform.localScale = _hoverScale;
                return;
            case CARD_STATE.HIDE:
                _canvasGroup.alpha = 0f;
                return;
        }
    }

    public void UpdateUIStacking(int num)
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
    public void SetUIData(CardData data)
    {
        SetCardText(data);
        SetCardImage(data);
    }
    private void SetCardText(CardData data)
    {
        _IDText.text = data._cid;
        _nameText.text = data._name;
        _staminaText.text = data._stamina.ToString();
        _attackText.text = data._attack.ToString();
        _defenceText.text = data._defence.ToString();
        _speedText.text = data._productSpeed.ToString();
        _amountText.text = data._productYield.ToString();
        _probabilityText.text = data._goodsProbability.ToString();

    }

    private void SetCardImage(CardData data)
    {
        Sprite[] characterImages = Resources.LoadAll<Sprite>("Character/CharacterImage");
        if (0 == characterImages.Length)
        {
            Debug.LogError($"ĳ���� �̹����� ���� imagePath: {characterImages}");
            return;
        }

        foreach (var image in characterImages)
        {
            if (image.name == data._cid)
            {
                _characterImage.sprite = image;
                return;
            }
        }

        Debug.LogError($"��������Ʈ�� ����. imageName : {data._cid}");
        return;
    }

    #endregion
}

public enum CARD_STATE
{
    DEFAULT,
    MOUSE_HOVER,
    HIDE
}

