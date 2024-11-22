using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ExpandSlot : MonoBehaviour
{
    public static ExpandSlot Instance { get; private set; }

    // 초기 덱을 선택하면  -> 카드 저장(& 나중에 사용하면 제거)하는 클래스에서 받아옴
    // 내가 카드 슬롯에 맞춰서 카드를 Add하고 Remove하는 로직은 안해도 되는 거..? ( ex.이미 슬롯 개수만큼 개수가 꽉 찼을 때, 카드 더 이상 받을 수 없게 하는 로직같은 거)

    private int minSlot = 8; // 초기 카드 슬롯
    private int maxSlot = 20; // 확장할 수 있는 최대 카드 슬롯
    private int currentMaxSlot; // 현재 가질 수 있는 최대 카드 슬롯

    private void Awake()
    {
        if (Instance != null && Instance != this)
        {
            Destroy(gameObject);
        }
        else
        {
            Instance = this;
            currentMaxSlot = minSlot;
        }
    }

    public void UpdateSlot(int _additionalSlot)
    {
        if ( currentMaxSlot < maxSlot)
        {
            currentMaxSlot = Mathf.Min(currentMaxSlot + _additionalSlot, maxSlot);
            Debug.Log("인벤토리가 확장 되었음.최대 슬롯 : " + currentMaxSlot);
            //Debug.Log("현재 슬롯 : " + itemList.Count + "/" + currentMaxSlot);
        }
        else
        {
            Debug.Log("더 이상 확장 불가");
        }
    }

    //public void AddItem(Card _item)
    //{
    //    if (itemList.Count < currentSlot)
    //    {
    //        itemList.Add(_item);
    //        Debug.Log("넣음");
    //    }
    //    else
    //    {
    //        Debug.Log(" 인벤토리 슬롯 부족");
    //    }
    //}

    //public void RemoveItem(Card _item)
    //{
    //    if (itemList.Contains(_item))
    //    {
    //        itemList.Remove(_item);
    //        Debug.Log("제거");
    //    }
    //}

    //public List<Card> GetInventory()
    //{
    //    return new List<Card>(itemList);
    //}
}
