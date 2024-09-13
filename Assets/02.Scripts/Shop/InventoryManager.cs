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
            Debug.Log(" �κ��丮 ���� ����");
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
        Debug.Log("�κ��丮�� Ȯ�� �Ǿ���.�ִ� ���� : " + minSlot );
        Debug.Log("���� ���� : " + inventory.Count);
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
