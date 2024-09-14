using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// 카드 이동 담당
/// </summary>
public class CardMoveManager : MonoBehaviour
{
    #region Singleton

    private static CardMoveManager instance = null;

    void Awake()
    {
        if (null == instance)
        {
            instance = this;
        }
        else
        {
            Destroy(this.gameObject);
        }
    }

    public static CardMoveManager Instance
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
    private Card _originCard;
    private bool _isDrag = false;

    #endregion

    #region Methods
    

    private void Update()
    {
        var hit = Physics2D.Raycast(Input.mousePosition, Vector2.zero, Mathf.Infinity, LayerMask.GetMask("Card"));
        if (hit.collider != null && !_isDrag)
        {
            _originCard = hit.collider.GetComponent<Card>();
            _originCard.SetCardState(CARD_STATE.MOUSE_HOVER);
            if (Input.GetMouseButtonDown(0))
            {
                _isDrag = true;
                _originCard.SetCardState(CARD_STATE.HIDE);
                _movingCard.SetCard(_originCard._data);
                _movingCard.SetCardState(CARD_STATE.MOVING);
                _movingCard.GetComponent<RectTransform>().position = _originCard.GetComponent<RectTransform>().position;
            }
            
        }
        if (_isDrag)
        {
            _movingCard.GetComponent<RectTransform>().position = Input.mousePosition;
            if (Input.GetMouseButtonUp(0))
            {
                _isDrag = false;
                _originCard.SetCardState(CARD_STATE.DEFAULT);
                _movingCard.SetCardState(CARD_STATE.HIDE);
            }
        }

    }

    private void CheckCardDrag()
    {

    }

    #endregion
}
