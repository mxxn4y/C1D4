using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InventoryManager : MonoBehaviour
{
    public static InventoryManager Instance { get; private set; }

    private List<ShopItemSO> inventory = new List<ShopItemSO>();
    private int maxSlot =20;
    private int minSlot =8;

    private void Awake()
    {
        if (Instance != null && Instance != this)
        {
            Destroy(gameObject);
        }
        else
        {
            Instance = this;
            //DontDestroyOnLoad(gameObject);
        }
    }

    public void AddItem(ShopItemSO _item)
    {
        if (inventory.Count < maxSlot)
        {
            inventory.Add(_item);
        }
        else
        {
            Debug.Log(" 인벤토리 슬롯 부족");
        }
    }

    public void RemoveItem(ShopItemSO _item)
    {
        if (inventory.Contains(_item))
        {
            //inventory[item]--;
            //if (inventory[item] <= 0)
            //{
                inventory.Remove(_item);
            //}
        }
    }

    public List<ShopItemSO> GetInventory()
    {
        return new List<ShopItemSO>(inventory);
    }


    public void ExpendSlot(int additionalSlot) {
        //maxSlot += additionalSlot;
        minSlot = Mathf.Min(minSlot + additionalSlot, maxSlot);
        Debug.Log("인벤토리가 확장 되었음.최대 슬롯 : " + minSlot );
        Debug.Log("현재 슬롯 : " + inventory.Count);
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
