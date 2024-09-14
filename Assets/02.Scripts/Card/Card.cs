using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// 실제 카드 객체 생성용 클래스
/// </summary>
[RequireComponent(typeof(CardUI))]
public class Card : MonoBehaviour
{
    [field: SerializeField] public CardData _data { get; private set; }

    /// <summary>
    /// 카드 데이터 삽입, UI 설정, 스택킹 표시
    /// </summary>
    /// <param name="data"></param>
    /// <param name="num"></param>
    public void SetCard(CardData data,int num)
    {
        _data = data;
        GetComponent<CardUI>().SetUIData(_data);
        GetComponent<CardUI>().SetUIStacking(num);
    }
    /// <summary>
    /// 카드 데이터 삽입, UI 설정
    /// </summary>
    /// <param name="data"></param>
    public void SetCard(CardData data)
    {
        _data = data;
        GetComponent<CardUI>().SetUIData(_data);
    }
    /// <summary>
    /// 스택킹 표시 갱신
    /// </summary>
    /// <param name="num"></param>
    public void SetCard(int num)
    {
        GetComponent<CardUI>().SetUIStacking(num);
    }
    /// <summary>
    /// 카드 상태 갱신
    /// </summary>
    /// <param name="state"></param>
    public void SetCardState(CARD_STATE state)
    {
        GetComponent<CardUI>().SetUIState(state);
    }

}
public enum CARD_STATE
{
    DEFAULT,
    MOUSE_HOVER,
    MOVING,
    HIDE
}

