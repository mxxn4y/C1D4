using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// 일반적인 CardData 오브젝트들의 모음 
/// </summary>
[CreateAssetMenuAttribute(menuName = "CardCollection")]
public class CardCollection : ScriptableObject
{
    [field: SerializeField] public List<ScriptableCard> CardsInCollection { get; private set; }

    //카드 덱에서 제거
    public void RemoveCardFromCollection(ScriptableCard card)
    {
        if (CardsInCollection.Contains(card))
        {
            CardsInCollection.Remove(card);
        }
        else
        {
            //카드가 덱에 없을때 어떻게 할 지
        }
    }

    //카드 덱에 추가
    public void AddCardToCollection(ScriptableCard card)
    {
        CardsInCollection.Add(card);
    }
}
