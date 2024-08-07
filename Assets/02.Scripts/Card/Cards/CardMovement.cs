using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
/// <summary>
/// 카드 드래그 앤 드롭
/// </summary>
public class CardMovement : MonoBehaviour, IDragHandler, IEndDragHandler, IBeginDragHandler
{

    #region Fields and Properties

    private bool _isBeingDragged;
    private Canvas _cardCanvas;
    private RectTransform _rectTransform;
    private Card _card;

    private readonly string CANVAS_TAG = "CardCanvas";

    #endregion

    #region Methods

    private void Start()
    {
        _cardCanvas = GameObject.FindGameObjectWithTag(CANVAS_TAG).GetComponent<Canvas>();
        _rectTransform = GetComponent<RectTransform>();
        _card = GetComponent<Card>();
    }

    #endregion
    public void OnBeginDrag(PointerEventData eventData)
    {
        _isBeingDragged = true;
    }
    public void OnDrag(PointerEventData eventData)
    {
        _rectTransform.anchoredPosition += (eventData.delta / _cardCanvas.scaleFactor);
    }

    public void OnEndDrag(PointerEventData eventData)
    {
        _isBeingDragged = false;
        Deck.Instance.PlaceCard(_card);
    }
}
