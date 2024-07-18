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
    private bool isPlaceable;
    private GameObject prevTile;
    private Vector3 prevPos;

    private readonly string CANVAS_TAG = "CardCanvas";

    #endregion

    #region Methods

    private void Start()
    {
        _cardCanvas = GameObject.FindGameObjectWithTag(CANVAS_TAG).GetComponent<Canvas>();
        _rectTransform = GetComponent<RectTransform>();
        _card = GetComponent<Card>();
        isPlaceable = false;
    }
    public void OnBeginDrag(PointerEventData eventData)
    {
        _isBeingDragged = true;
        prevPos = _rectTransform.position;
    }
    public void OnDrag(PointerEventData eventData)
    {
        _rectTransform.position = Input.mousePosition;
        isPlaceable = IsCardPlaceable();
        _card.SetUI(true);
    }

    public void OnEndDrag(PointerEventData eventData)
    {
        _isBeingDragged = false;
        _card.SetUI(false);
        if (isPlaceable)
        {
            Deck.Instance.PlaceCard(_card);
            prevTile.GetComponent<TileSetter>().ActivateTileMinion(_card.CardData.MinionData);
            prevTile.GetComponent<TileSetter>().SetTileType(TILE_STATE.PLACED);
        }
        else
        {
            _rectTransform.position = prevPos;
        }
        
    }

    public bool IsCardPlaceable()
    {
        Vector2 mousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        LayerMask layer = LayerMask.NameToLayer("Tile");
        RaycastHit2D hit = Physics2D.Raycast(mousePos, Vector2.zero, Mathf.Infinity, layer);


        if (hit.collider != null && hit.collider.gameObject.tag == "Tile")
        {

            hit.collider.gameObject.GetComponent<TileSetter>().SetTileType(TILE_STATE.SELECTED);
            prevTile = hit.collider.gameObject;
            return true;

        }
        else
        {
            if(prevTile != null)
            {
                prevTile.GetComponent<TileSetter>().SetTileType(TILE_STATE.DEFAULT);
            }
            
            
        }
        return false;

    }
    #endregion

}
