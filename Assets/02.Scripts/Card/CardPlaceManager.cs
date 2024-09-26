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

    [SerializeField] private Card movingCard;
    public Card selectedCard { get; private set; }
    public bool isCardMoving { get; private set; }
    public TileInfo selectedTile { get; private set; }

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
        OnCardPlace += RemoveUsedCard;
        OnCardMoveCancel += DeactivateMovingCard;

    }
    private void Update()
    {
        if (isCardMoving)
        {
            //Tile Layer�� raycast ����ؼ� ���콺 ��ġ�� Ÿ�� _selectedTile�� ����
            var mousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            var hit = Physics2D.Raycast(mousePos, Vector2.zero, Mathf.Infinity, LayerMask.GetMask("Tile"));
            selectedTile = hit.collider?.gameObject.GetComponent<TileInfo>();

            OnCardMove?.Invoke();

            if (Input.GetMouseButtonUp(0))
            {
                isCardMoving = false;
                if (selectedTile?.TileState == TILE_STATE.SELECTED)
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

    /// <summary>
    /// ������ ī��� ������ ������ ���� ī�� Ȱ��ȭ(��ġ, UI ��ġ)
    /// </summary>
    /// <param name="_selectedCard"></param>
    private void ActivateMovingCard(Card _selectedCard)
    {
        this.selectedCard = _selectedCard;
        movingCard.gameObject.SetActive(true);
        movingCard.SetCard(this.selectedCard.data);
        movingCard.GetComponent<RectTransform>().position = this.selectedCard.GetComponent<RectTransform>().position;
        isCardMoving = true;
    }

    private void DragMovingCard()
    {
        movingCard.GetComponent<RectTransform>().position = Input.mousePosition;
    }
    /// <summary>
    /// ������ ���� ī�� ��Ȱ��ȭ, ���� ī�� ���� ����Ʈ�� ����
    /// </summary>
    private void DeactivateMovingCard()
    {
        movingCard.gameObject.SetActive(false);
        selectedCard.State = CARD_STATE.DEFAULT;
    }
    /// <summary>
    /// ��ġ�Ͽ� �Ҹ��� ī�� �÷��̾� ������ ����, ����ī�� ���ŵ� ���� �ݿ��Ͽ� Ȱ��ȭ
    /// </summary>
    private void RemoveUsedCard()
    {
        var playerDeck = WorkSceneManager.Instance.tempPlayerCards;
        if (playerDeck.ContainsKey(selectedCard.data.cid))
        {

            if (playerDeck[selectedCard.data.cid] <= 1)
            {
                playerDeck.Remove(selectedCard.data.cid);
                Destroy(selectedCard.gameObject);
            }
            else
            {
                playerDeck[selectedCard.data.cid] -= 1;
                selectedCard.UpdateCard(playerDeck[selectedCard.data.cid]);
            }

        }
    }
    #endregion
}
