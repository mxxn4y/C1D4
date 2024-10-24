using System.Collections;
using System.Collections.Generic;

using UnityEngine;

public class ShoppingManager : MonoBehaviour //이름 바꾸고 슬롯 빼고 ShoppingManager
{
    public static ShoppingManager Instance { get; private set; }

    private List<ShopItemSO> itemList = new List<ShopItemSO>(); //변수명 itemList
    private int maxSlot =20;
    private int minSlot =8;
    private int currentSlot;

    private void Awake()
    {
        if (Instance != null && Instance != this)
        {
            Destroy(gameObject);
        }
        else
        {
            Instance = this;
            currentSlot = minSlot;
            //DontDestroyOnLoad(gameObject);
        }
    }

    public void AddItem(ShopItemSO _item)
    {
        if (itemList.Count < currentSlot)
        {
            itemList.Add(_item);
            Debug.Log("인벤토리에 넣음");

            Debug.Log(itemList.Count);
        }
        else
        {
            Debug.Log(" 인벤토리 슬롯 부족");
        }
    }

    public void RemoveItem(ShopItemSO _item)
    {
        if (itemList.Contains(_item))
        {
            //inventory[item]--;
            //if (inventory[item] <= 0)
            //{
            itemList.Remove(_item);
            //}
        }
    }

    public List<ShopItemSO> GetInventory()
    {
        return new List<ShopItemSO>(itemList);
    }


    public void ExpendSlot(int _additionalSlot) {
        if (currentSlot < maxSlot)
        {
            currentSlot = Mathf.Min(currentSlot + _additionalSlot, maxSlot);
            Debug.Log("인벤토리가 확장 되었음.최대 슬롯 : " + currentSlot);
            Debug.Log("현재 슬롯 : " + itemList.Count + "/" + currentSlot);
        }
    }

    //public List<ItemInstance> items = new List<ItemInstance>();
    //public int maxSlots = 10;

    //public void AddItem(CollectableItem collectable)
    //{
    //    if (items.Count < maxSlots)
    //    {
    //        items.Add(collectable.CollectItem());
    //        return;
    //    }
    //}
}
