using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// ���� ī�� ��ü ������ Ŭ����
/// </summary>
[RequireComponent(typeof(CardUI))]
[RequireComponent(typeof(CardMovement))]
public class Card : MonoBehaviour
{
    [field: SerializeField] public CardData _data { get; private set; }

    public void SetCard(CardData data)
    {
        _data = data;
    }
}


