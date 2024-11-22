using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;


public class ShopManager : MonoBehaviour
{

    public static ShopManager Instance { get; private set; }

    public int gem;
    public int specialGem;
    public TMP_Text gemUI;
    public TMP_Text specialGemUI;
    public GameObject[] shopPanelsGO;
    public ShopItemSO[] shopItemSO;
    public SellingCardUI[] shopPanels;

    public int itemCount=0;
    public delegate void PurchaseStateChanged(string itemName);
    public event PurchaseStateChanged OnPurchaseStateChanged;

    public Dictionary<string, int> dailyPurchaseCount = new Dictionary<string, int>();
    public Dictionary<string, int> totalPurchaseCount = new Dictionary<string, int>();

    private void Awake()
    {
        if (Instance != null && Instance != this)
        {
            Destroy(this.gameObject);
        }
        else
        {
            Instance = this;
        }
    }

    void Start()
    {


        gemUI.text = gem.ToString();
        specialGemUI.text = specialGem.ToString();
    }

    public bool CanClickItem(ShopItemData _itemData)
    {
        // ������ �������� ������ Ŭ�� ����
        if (_itemData.isUnlimited)
            return true;

            // ���� �Ϸ� ���� Ƚ�� ���
            int remainingDaily = _itemData.maxDailyPurchase > 0 && ShopManager.Instance.dailyPurchaseCount.ContainsKey(_itemData.itemName)
                ? _itemData.maxDailyPurchase - ShopManager.Instance.dailyPurchaseCount[_itemData.itemName]
                : _itemData.maxDailyPurchase;

            // ���� ��ü ���� Ƚ�� ���
            int remainingTotal = _itemData.maxTotalPurchase > 0 && ShopManager.Instance.totalPurchaseCount.ContainsKey(_itemData.itemName)
                ? _itemData.maxTotalPurchase - ShopManager.Instance.totalPurchaseCount[_itemData.itemName]
                : _itemData.maxTotalPurchase;

        if (_itemData.maxDailyPurchase > 0)
        {
            return remainingDaily > 0;
            Debug.Log(_itemData.itemName + remainingDaily + "�� ����");
        }
        if (_itemData.maxTotalPurchase > 0)
        {
            return remainingTotal > 0;
            Debug.Log(_itemData.itemName + remainingTotal + "�� ����");
        }
        return true;
    }


    public bool CanPurchaseItem(ShopItemData item)
    {
        if (item.isUnlimited) return true;

        // �Ϸ� �ִ� ���� Ƚ�� Ȯ��
        if (item.maxDailyPurchase > 0)
        {
            // dailyPurchaseCount.TryGetValue���� item.itemName�� Ű�� ���
            if (dailyPurchaseCount.TryGetValue(item.itemName, out int dailyCount) && dailyCount >= item.maxDailyPurchase)
                return false;
        }

        // ���� ��ü �ִ� ���� Ƚ�� Ȯ��
        if (item.maxTotalPurchase > 0)
        {
            // totalPurchaseCount.TryGetValue���� item.itemName�� Ű�� ���
            if (totalPurchaseCount.TryGetValue(item.itemName, out int totalCount) && totalCount >= item.maxTotalPurchase)
                return false;
        }

        return true;
    }
    /*
    public bool CanPurchaseItem(ShopItemData item, int currentlySelected = 0)
    {
        if (item.isUnlimited) return true;

        // �Ϸ� �ִ� ���� Ƚ�� Ȯ��
        if (item.maxDailyPurchase > 0)
        {
            dailyPurchaseCount.TryGetValue(item.itemName, out int dailyCount);
            int totalDailyCount = dailyCount + currentlySelected;

            if (totalDailyCount >= item.maxDailyPurchase)
            {
                return false;
            }
        }

        // ��ü �ִ� ���� Ƚ�� Ȯ��
        if (item.maxTotalPurchase > 0)
        {
            totalPurchaseCount.TryGetValue(item.itemName, out int totalCount);
            int realtotalCount = totalCount + currentlySelected;

            if (realtotalCount >= item.maxTotalPurchase)
            {
                return false;
            }
        }

        return true;
    }

    */
    public void TrackPurchase(ShopItemData item) // �� ������ �� �Ϸ� �Ǵ� ��ü �ִ� ���� Ƚ�� ����
    {
        if (!item.isUnlimited) // CSV���� isUnlimitedȮ�� ����
        {
            // �Ϸ� �ִ� ���� Ƚ�� ����
            if (item.maxDailyPurchase > 0) // CSV���� Ȯ�� ����
            {
                // item.itemName�� Ű�� ����Ͽ� dailyPurchaseCount ��ųʸ��� ����
                if (!dailyPurchaseCount.ContainsKey(item.itemName))
                    dailyPurchaseCount[item.itemName] = 0;
                dailyPurchaseCount[item.itemName]++;
            }

            // ��ü �ִ� ���� Ƚ�� ����
            if (item.maxTotalPurchase > 0)
            {
                // item.itemName�� Ű�� ����Ͽ� totalPurchaseCount ��ųʸ��� ����
                if (!totalPurchaseCount.ContainsKey(item.itemName))
                    totalPurchaseCount[item.itemName] = 0;
                totalPurchaseCount[item.itemName]++;
            }
        }
        OnPurchaseStateChanged?.Invoke(item.itemName);
        Debug.Log(item.itemName+CanPurchaseItem(item));
    }

    public void RemovePurchase(ShopItemData item)
    {
        if (!item.isUnlimited)
        {
            // �Ϸ� ���� Ƚ�� ����
            if (item.maxDailyPurchase > 0 &&
                dailyPurchaseCount.ContainsKey(item.itemName) &&
                dailyPurchaseCount[item.itemName] > 0)
            {
                dailyPurchaseCount[item.itemName]--;
                Debug.Log(item.itemName+ "�Ϸ� Ƚ�� ���� {dailyPurchaseCount[item.itemName]}");
            }

            // ��ü ���� Ƚ�� ����
            if (item.maxTotalPurchase > 0 &&
                totalPurchaseCount.ContainsKey(item.itemName) &&
                totalPurchaseCount[item.itemName] > 0)
            {
                totalPurchaseCount[item.itemName]--;
                Debug.Log(item.itemName + "��ü Ƚ�� ���� {totalPurchaseCount[item.itemName]}");
            }
        }
        OnPurchaseStateChanged?.Invoke(item.itemName);
        Debug.Log(item.itemName + "�̹��� ���� �̺�Ʈ");
        Debug.Log(item.itemName + CanPurchaseItem(item));

    }


    public void ResetDailyPurchases()
    {
        dailyPurchaseCount.Clear(); // ���߿� �Ϸ� ������ �� ȣ��
    }

    public void UpdateNormalGem(int _amount)
    {
        gem -= _amount;
        gemUI.text = gem.ToString();
        Debug.Log("�Ϲ���ȭ ���"+gem);
    }

    public void UpdateSpecialGem(int _amount)
    {
        specialGem -= _amount;
        specialGemUI.text = specialGem.ToString();
    }

    //�ӽ� gem���
    public void AddCoins()
    {
        gem+=1000;
        gemUI.text = gem.ToString();
        //UpdatePurchaseButtons();
    }

    public void AddSpecialGem()
    {
        specialGem+=100;
        specialGemUI.text = specialGem.ToString();
       // UpdatePurchaseButtons();
    }

}
