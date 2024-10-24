using System;
using System.Collections;
using UnityEngine;

/// <summary>
/// 카드 프리팹이 가지고 있고 카드 정보와 상태 저장
/// 실제 카드 객체(gameObject) 생성할 때 사용
/// </summary>
[RequireComponent(typeof(CardUI))]
public class Card : MonoBehaviour
{
    #region Fields and Properties
    [SerializeField] CardUI cardUI;
    public CardData data { get; private set; }
    private CARD_STATE state;
    public CARD_STATE State{
        get { return state;}
        set {
            state = value;
            cardUI.SetUIState(state);
        }
    }
    #endregion

    #region Methods
    /// <summary>
    /// 카드 데이터 삽입, UI 갱신
    /// </summary>
    /// <param name="_data"></param>
    public void SetCard(CardData _data)
    {
        this.data = _data;
        cardUI.SetUIData(this.data);
    }
    /// <summary>
    /// 카드 데이터 삽입, UI 갱신
    /// </summary>
    /// <param name="_data"></param>
    /// <param name="_num"></param>
    public void SetCard(CardData _data, int _num)
    {
        this.data = _data;
        cardUI.SetUIData(this.data);
        cardUI.UpdateUIStacking(_num);

    }

    /// <summary>
    /// 스택킹 표시 갱신
    /// </summary>
    /// <param name="_num"></param>
    public void UpdateCard(int _num)
    {
        cardUI.UpdateUIStacking(_num);
    }
    #endregion
}


