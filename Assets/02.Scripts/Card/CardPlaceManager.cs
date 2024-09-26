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
            //Tile Layer에 raycast 사용해서 마우스 위치의 타일 _selectedTile에 저장
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
    /// 선택한 카드와 동일한 움직임 전용 카드 활성화(위치, UI 일치)
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
    /// 움직임 전용 카드 비활성화, 원본 카드 상태 디폴트로 복구
    /// </summary>
    private void DeactivateMovingCard()
    {
        movingCard.gameObject.SetActive(false);
        selectedCard.State = CARD_STATE.DEFAULT;
    }
    /// <summary>
    /// 배치하여 소모한 카드 플레이어 덱에서 제거, 원본카드 갱신된 정보 반영하여 활성화
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
