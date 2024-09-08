using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// 실제 카드 객체 생성용 클래스
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


