using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
/// <summary>
/// 카드 마우스 호버와 선택 감지
/// </summary>
public class CardController : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler, IPointerDownHandler
{
    #region Fields and Properties

    [SerializeField] private CardUI cardUI;
    public Minion Minion { get; private set; }
    private CARD_STATE state;
    public CARD_STATE State{
        get { return state;}
        set {
            state = value;
            cardUI.UpdateState(state);
        }
    }

    #endregion

    #region Methods
    private void Awake()
    {
        State = CARD_STATE.DEFAULT;
    }
    public void OnPointerEnter(PointerEventData _eventData)
    {
        if (State == CARD_STATE.DEFAULT)
        {
            State = CARD_STATE.MOUSE_HOVER;
        }
    }
    public void OnPointerExit(PointerEventData _eventData)
    {
        if (State == CARD_STATE.MOUSE_HOVER)
        {
            State = CARD_STATE.DEFAULT;
        }
    }
    public void OnPointerDown(PointerEventData _eventData)
    {
        if(_eventData.button != PointerEventData.InputButton.Left) return;
        State = CARD_STATE.HIDE;
        CardPlaceManager.Instance.ActivateMovingCard(this);
    }

    public void Set(Minion _minion)
    {
        Minion = _minion;
        cardUI.Set(_minion);
    }

    #endregion
}
