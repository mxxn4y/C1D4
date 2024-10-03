using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CollectableItem : MonoBehaviour
    // 안 쓸 클래스
{
    public ItemInstance item;

    public ItemInstance CollectItem()
    {
        Destroy(gameObject);
        return item;
    }
}

    [System.Serializable]
    public struct ItemInstance
    {
        public int itemCondition;
        public ShopItemSO itemType;

        ItemInstance(int _itemCondition, ShopItemSO _itemType)
        {
            itemCondition = _itemCondition;
            itemType = _itemType;
        }
    }

