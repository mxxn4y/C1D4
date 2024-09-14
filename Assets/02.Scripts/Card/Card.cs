using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// ���� ī�� ��ü ������ Ŭ����
/// </summary>
[RequireComponent(typeof(CardUI))]
public class Card : MonoBehaviour
{
    [field: SerializeField] public CardData _data { get; private set; }

    /// <summary>
    /// ī�� ������ ����, UI ����, ����ŷ ǥ��
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
    /// ī�� ������ ����, UI ����
    /// </summary>
    /// <param name="data"></param>
    public void SetCard(CardData data)
    {
        _data = data;
        GetComponent<CardUI>().SetUIData(_data);
    }
    /// <summary>
    /// ����ŷ ǥ�� ����
    /// </summary>
    /// <param name="num"></param>
    public void SetCard(int num)
    {
        GetComponent<CardUI>().SetUIStacking(num);
    }
    /// <summary>
    /// ī�� ���� ����
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

