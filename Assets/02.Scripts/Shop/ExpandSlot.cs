using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ExpandSlot : MonoBehaviour
{
    public static ExpandSlot Instance { get; private set; }

    // �ʱ� ���� �����ϸ�  -> ī�� ����(& ���߿� ����ϸ� ����)�ϴ� Ŭ�������� �޾ƿ�
    // ���� ī�� ���Կ� ���缭 ī�带 Add�ϰ� Remove�ϴ� ������ ���ص� �Ǵ� ��..? ( ex.�̹� ���� ������ŭ ������ �� á�� ��, ī�� �� �̻� ���� �� ���� �ϴ� �������� ��)

    private int minSlot = 8; // �ʱ� ī�� ����
    private int maxSlot = 20; // Ȯ���� �� �ִ� �ִ� ī�� ����
    private int currentMaxSlot; // ���� ���� �� �ִ� �ִ� ī�� ����

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
            Debug.Log("�κ��丮�� Ȯ�� �Ǿ���.�ִ� ���� : " + currentMaxSlot);
            //Debug.Log("���� ���� : " + itemList.Count + "/" + currentMaxSlot);
        }
        else
        {
            Debug.Log("�� �̻� Ȯ�� �Ұ�");
        }
    }

    //public void AddItem(Card _item)
    //{
    //    if (itemList.Count < currentSlot)
    //    {
    //        itemList.Add(_item);
    //        Debug.Log("����");
    //    }
    //    else
    //    {
    //        Debug.Log(" �κ��丮 ���� ����");
    //    }
    //}

    //public void RemoveItem(Card _item)
    //{
    //    if (itemList.Contains(_item))
    //    {
    //        itemList.Remove(_item);
    //        Debug.Log("����");
    //    }
    //}

    //public List<Card> GetInventory()
    //{
    //    return new List<Card>(itemList);
    //}
}
