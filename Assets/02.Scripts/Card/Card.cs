using System;
using System.Collections;
using UnityEngine;

/// <summary>
/// ī�� �������� ������ �ְ� ī�� ������ ���� ����
/// ���� ī�� ��ü(gameObject) ������ �� ���
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
    /// ī�� ������ ����, UI ����
    /// </summary>
    /// <param name="_data"></param>
    public void SetCard(CardData _data)
    {
        this.data = _data;
        cardUI.SetUIData(this.data);
    }
    /// <summary>
    /// ī�� ������ ����, UI ����
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
    /// ����ŷ ǥ�� ����
    /// </summary>
    /// <param name="_num"></param>
    public void UpdateCard(int _num)
    {
        cardUI.UpdateUIStacking(_num);
    }
    #endregion
}


