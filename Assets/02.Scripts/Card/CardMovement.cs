using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
/// <summary>
/// ī�� �巡�� �� ���
/// </summary>
public class CardMovement : MonoBehaviour, IDragHandler, IEndDragHandler, IBeginDragHandler, IPointerEnterHandler, IPointerExitHandler
{

    #region Fields and Properties

    private RectTransform _rectTransform;
    public CardData _cardData;
    private CardUI _cardUI;
    private bool _isPlaceable;
    private GameObject _prevTile;
    private Vector3 _prevPos;

    #endregion

    #region Methods

    private void Start()
    {
        _rectTransform = GetComponent<RectTransform>();
        _cardData = GetComponent<Card>()._data;
        _cardUI = GetComponent<CardUI>();
        _isPlaceable = false;
    }

    //�巡�� ���۽� ȣ�� (�巡�� ���� ��ġ prevPos�� ����)
    public void OnBeginDrag(PointerEventData eventData)
    {
        _prevPos = _rectTransform.position;
        _cardUI.SetCardUI(CARD_UI_STATE.DEFAULT);
    }
    //�巡�� �� ȣ�� (���콺 ���� �����̱�, ��ġ ������ �������� Ȯ��, �����̴� UI�� ��ü)
    public void OnDrag(PointerEventData eventData)
    {
        _rectTransform.position = Input.mousePosition;
        _isPlaceable = IsCardPlaceable();
        _cardUI.SetCardUI(CARD_UI_STATE.MOVING);
    }
    //�巡�� ���� �� ȣ�� (��ġ ������ ���� / �ƴ� ���·� ������)
    public void OnEndDrag(PointerEventData eventData)
    {

        _cardUI.SetCardUI(CARD_UI_STATE.DEFAULT);
        _rectTransform.position = _prevPos;
        if (_isPlaceable)
        {
            DeckManager.Instance.PlaceCard(GetComponent<Card>());
            _prevTile.GetComponentInChildren<Minion>().SetMinion(_cardData);
            _prevTile.GetComponent<TileInfo>().SetState(TILE_STATE.OCCUPIED);
            _prevTile = null;
        }
        
    }


    public void OnPointerEnter(PointerEventData eventData)
    {
        _cardUI.SetCardUI(CARD_UI_STATE.MOUSE_HOVER);
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        _cardUI.SetCardUI(CARD_UI_STATE.DEFAULT);
    }

    public bool IsCardPlaceable()
    {
        Vector2 mousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        LayerMask layer = LayerMask.NameToLayer("Tile");
        RaycastHit2D hit = Physics2D.Raycast(mousePos, Vector2.zero, Mathf.Infinity);

        if (_prevTile != null) 
        {
            _prevTile.GetComponent<TileInfo>().SetState(TILE_STATE.DEFAULT);
            _prevTile = null;
        }
        if (hit.collider != null && hit.collider.gameObject.GetComponent<TileInfo>()._tileState == TILE_STATE.DEFAULT)
        {
            _prevTile = hit.collider.gameObject;
            _prevTile.GetComponent<TileInfo>().SetState(TILE_STATE.SELECTED);
            return true;

        }
        return false;

    }
    #endregion


}
