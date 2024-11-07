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
    public Card SelectedCard { get; private set; }
    public TileInfo SelectedTile { get; private set; }
    private bool isCardMoving;

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
            //Tile Layer에 raycast 사용 -> 마우스 위치의 타일 _selectedTile에 저장
            var mousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            var hit = Physics2D.Raycast(mousePos, Vector2.zero, Mathf.Infinity, LayerMask.GetMask("Tile"));
            SelectedTile = hit.collider?.gameObject.GetComponent<TileInfo>();

            OnCardMove?.Invoke();

            if (Input.GetMouseButtonUp(0))
            {
                isCardMoving = false;
                if (SelectedTile?.TileState == TILE_STATE.SELECTED)
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
    /// 선택한 카드와 동일한 움직임 전용 카드 활성화(위치, UI 일치)
    /// </summary>
    private void ActivateMovingCard(Card _selectedCard)
    {
        this.SelectedCard = _selectedCard;
        movingCard.gameObject.SetActive(true);
        movingCard.SetCard(this.SelectedCard.data);
        movingCard.GetComponent<RectTransform>().position = this.SelectedCard.GetComponent<RectTransform>().position;
        isCardMoving = true;
    }

    private void DragMovingCard()
    {
        movingCard.GetComponent<RectTransform>().position = Input.mousePosition;
    }
    /// <summary>
    /// 움직임 전용 카드 비활성화, 원본 카드 상태 디폴트로 복구
    /// </summary>
    private void DeactivateMovingCard()
    {
        movingCard.gameObject.SetActive(false);
        SelectedCard.State = CARD_STATE.DEFAULT;
    }
    /// <summary>
    /// 배치하여 소모한 카드 플레이어 덱에서 제거, 원본카드 갱신된 정보 반영하여 활성화
    /// </summary>
    private void RemoveUsedCard()
    {
        var playerDeck = PlayerInfoManager.playerCards;
        if (playerDeck.ContainsKey(SelectedCard.data.cid))
        {

            if (playerDeck[SelectedCard.data.cid] <= 1)
            {
                PlayerInfoManager.playerCards.Remove(SelectedCard.data.cid);
                Destroy(SelectedCard.gameObject);
            }
            else
            {
                PlayerInfoManager.playerCards[SelectedCard.data.cid] -= 1;
                SelectedCard.UpdateCard(playerDeck[SelectedCard.data.cid]);
            }

        }
    }
    #endregion
}
