using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
/// <summary>
/// 카드 마우스 호버와 선택 감지
/// </summary>
public class CardInteraction : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler, IPointerDownHandler
{
    #region Fields and Properties

    [SerializeField] private Card _card;

    #endregion

    #region Methods
    private void Awake()
    {
        _card._state = CARD_STATE.DEFAULT;
    }
    public void OnPointerEnter(PointerEventData eventData)
    {
        if (_card._state == CARD_STATE.DEFAULT)
        {
            _card._state = CARD_STATE.MOUSE_HOVER;
        }
    }
    public void OnPointerExit(PointerEventData eventData)
    {
        if (_card._state == CARD_STATE.MOUSE_HOVER)
        {
            _card._state = CARD_STATE.DEFAULT;
        }
    }
    public void OnPointerDown(PointerEventData eventData)
    {
        _card._state = CARD_STATE.HIDE;
        CardPlaceManager.Instance.OnCardSelect?.Invoke(_card);
    }

    #endregion
}
