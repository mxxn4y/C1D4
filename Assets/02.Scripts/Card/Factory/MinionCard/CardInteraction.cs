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

    [SerializeField] private Card card;

    #endregion

    #region Methods
    private void Awake()
    {
        card.State = CARD_STATE.DEFAULT;
    }
    public void OnPointerEnter(PointerEventData eventData)
    {
        if (card.State == CARD_STATE.DEFAULT)
        {
            card.State = CARD_STATE.MOUSE_HOVER;
        }
    }
    public void OnPointerExit(PointerEventData eventData)
    {
        if (card.State == CARD_STATE.MOUSE_HOVER)
        {
            card.State = CARD_STATE.DEFAULT;
        }
    }
    public void OnPointerDown(PointerEventData eventData)
    {
        card.State = CARD_STATE.HIDE;
        CardPlaceManager.Instance.OnCardSelect?.Invoke(card);
    }

    #endregion
}
