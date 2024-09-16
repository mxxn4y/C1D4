using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.EventSystems;

/// <summary>
/// 실제 카드 객체 생성용 클래스
/// </summary>
[RequireComponent(typeof(CardUI))]
public class Card : MonoBehaviour
{
    [SerializeField] CardUI _cardUI;
    public CardData _data { get; private set; }
    private CARD_STATE state;
    public CARD_STATE _state{
        get { return state;}
        set {
            state = value;
            _cardUI.SetUIState(state);
        }
    }
    /// <summary>
    /// 카드 데이터 삽입
    /// </summary>
    /// <param name="data"></param>
    public void SetCard(CardData data)
    {
        _data = data;
        _cardUI.SetUIData(data);
    }
    public void SetCard(CardData data, int num)
    {
        _data = data;
        _cardUI.SetUIData(data);
        _cardUI.UpdateUIStacking(num);

    }

    /// <summary>
    /// 스택킹 표시 갱신
    /// </summary>
    /// <param name="num"></param>
    public void UpdateCard(int num)
    {
        _cardUI.UpdateUIStacking(num);
    }   

}


