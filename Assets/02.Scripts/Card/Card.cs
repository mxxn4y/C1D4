using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.EventSystems;

/// <summary>
/// ���� ī�� ��ü ������ Ŭ����
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
    /// ī�� ������ ����
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
    /// ����ŷ ǥ�� ����
    /// </summary>
    /// <param name="num"></param>
    public void UpdateCard(int num)
    {
        _cardUI.UpdateUIStacking(num);
    }   

}


