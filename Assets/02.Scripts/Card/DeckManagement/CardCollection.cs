using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// �Ϲ����� CardData ������Ʈ���� ���� 
/// </summary>
[CreateAssetMenuAttribute(menuName = "CardCollection")]
public class CardCollection : ScriptableObject
{
    [field: SerializeField] public List<ScriptableCard> CardsInCollection { get; private set; }

    //ī�� ������ ����
    public void RemoveCardFromCollection(ScriptableCard card)
    {
        if (CardsInCollection.Contains(card))
        {
            CardsInCollection.Remove(card);
        }
        else
        {
            //ī�尡 ���� ������ ��� �� ��
        }
    }

    //ī�� ���� �߰�
    public void AddCardToCollection(ScriptableCard card)
    {
        CardsInCollection.Add(card);
    }
}
