using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CardPlaceManager : MonoBehaviour
{
    #region Singleton

    private static CardPlaceManager instance = null;
    public static CardPlaceManager Instance
    {
        get
        {
            if (null == instance)
            {
                return null;
            }
            return instance;
        }
    }

    #endregion

    #region Fields and Properties

    [SerializeField] private Card _movingCard;
    public Card _selectedCard { get; private set; }
    public bool _isCardMoving { get; private set; }
    public TileInfo _selectedTile { get; private set; }

    public Action<Card> OnCardSelect {  get; set; }
    public Action OnCardMove { get; set; }
    public Action OnCardPlace { get; set;}
    public Action OnCardMoveCancel { get; set;}

    #endregion

    #region Methods
    private void Awake()
    {
        if (null == instance)
        {
            instance = this;
        }
        else
        {
            Destroy(this.gameObject);
        }

        OnCardSelect += ActivateMovingCard;
        OnCardMove += DragMovingCard;
        OnCardPlace += DeactivateMovingCard;
        OnCardPlace += UpdateSelectedCard;
        OnCardMoveCancel += DeactivateMovingCard;

    }
    private void Update()
    {
        if (_isCardMoving)
        {
            var mousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            var hit = Physics2D.Raycast(mousePos, Vector2.zero, Mathf.Infinity, LayerMask.GetMask("Tile"));
            _selectedTile = hit.collider?.gameObject.GetComponent<TileInfo>();
            OnCardMove?.Invoke();

            if (Input.GetMouseButtonUp(0))
            {
                _isCardMoving = false;
                if (_selectedTile?._tileState == TILE_STATE.SELECTED)
                {
                    OnCardPlace?.Invoke();
                }
                else
                {
                    OnCardMoveCancel?.Invoke();
                }
                
            }
        }

    }

    private void ActivateMovingCard(Card selectedCard)
    {
        _selectedCard = selectedCard;
        _movingCard.gameObject.SetActive(true);
        _movingCard.SetCard(_selectedCard._data);
        _movingCard.GetComponent<RectTransform>().position = _selectedCard.GetComponent<RectTransform>().position;
        _isCardMoving = true;
    }

    private void DragMovingCard()
    {
        _movingCard.GetComponent<RectTransform>().position = Input.mousePosition;
    }

    private void DeactivateMovingCard()
    {
        _movingCard.gameObject.SetActive(false);
        _selectedCard._state = CARD_STATE.DEFAULT;
    }

    private void UpdateSelectedCard()
    {
        DeckManager.Instance.UpdatePlayerDeck(_selectedCard);
    }
    #endregion
}
