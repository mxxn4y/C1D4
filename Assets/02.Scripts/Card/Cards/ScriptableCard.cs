using System.Collections;
using System.Collections.Generic;
using UnityEngine;

///<summary>
///개별 카드의 모든 데이터 가짐
///</summary>
///
[CreateAssetMenu(menuName ="CardData")]
public class ScriptableCard : ScriptableObject
{
    [field:SerializeField] public string CardNum { get; private set; }
    [field:SerializeField] public string CardName { get; private set; }
    [field:SerializeField] public int CardATK { get; private set; }
    [field:SerializeField] public int CardDEF { get; private set; }
    [field:SerializeField] public int CardSUP { get; private set; }
    [field: SerializeField] public Sprite Image { get; private set; }
    [field:SerializeField] public CardElement Element { get; private set; }
    [field:SerializeField] public CardType Type { get; private set; }
    [field:SerializeField] public CardRarity Rarity { get; private set; }
}

public enum CardElement
{
    Basic,
    Fire,
    Water,
    Grass
}

public enum CardType
{
    Attack,
    Defence,
    Support
}

public enum CardRarity
{
    Common,
    Rare,
    Epic,
    Legendary
}
