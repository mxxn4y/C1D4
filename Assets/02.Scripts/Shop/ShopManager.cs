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
        // 무제한 아이템은 언제나 클릭 가능
        if (_itemData.isUnlimited)
            return true;

            // 남은 하루 구매 횟수 계산
            int remainingDaily = _itemData.maxDailyPurchase > 0 && ShopManager.Instance.dailyPurchaseCount.ContainsKey(_itemData.itemName)
                ? _itemData.maxDailyPurchase - ShopManager.Instance.dailyPurchaseCount[_itemData.itemName]
                : _itemData.maxDailyPurchase;

            // 남은 전체 구매 횟수 계산
            int remainingTotal = _itemData.maxTotalPurchase > 0 && ShopManager.Instance.totalPurchaseCount.ContainsKey(_itemData.itemName)
                ? _itemData.maxTotalPurchase - ShopManager.Instance.totalPurchaseCount[_itemData.itemName]
                : _itemData.maxTotalPurchase;

        if (_itemData.maxDailyPurchase > 0)
        {
            return remainingDaily > 0;
            Debug.Log(_itemData.itemName + remainingDaily + "개 남음");
        }
        if (_itemData.maxTotalPurchase > 0)
        {
            return remainingTotal > 0;
            Debug.Log(_itemData.itemName + remainingTotal + "개 남음");
        }
        return true;
    }


    public bool CanPurchaseItem(ShopItemData item)
    {
        if (item.isUnlimited) return true;

        // 하루 최대 구매 횟수 확인
        if (item.maxDailyPurchase > 0)
        {
            // dailyPurchaseCount.TryGetValue에서 item.itemName을 키로 사용
            if (dailyPurchaseCount.TryGetValue(item.itemName, out int dailyCount) && dailyCount >= item.maxDailyPurchase)
                return false;
        }

        // 게임 전체 최대 구매 횟수 확인
        if (item.maxTotalPurchase > 0)
        {
            // totalPurchaseCount.TryGetValue에서 item.itemName을 키로 사용
            if (totalPurchaseCount.TryGetValue(item.itemName, out int totalCount) && totalCount >= item.maxTotalPurchase)
                return false;
        }

        return true;
    }
    /*
    public bool CanPurchaseItem(ShopItemData item, int currentlySelected = 0)
    {
        if (item.isUnlimited) return true;

        // 하루 최대 구매 횟수 확인
        if (item.maxDailyPurchase > 0)
        {
            dailyPurchaseCount.TryGetValue(item.itemName, out int dailyCount);
            int totalDailyCount = dailyCount + currentlySelected;

            if (totalDailyCount >= item.maxDailyPurchase)
            {
                return false;
            }
        }

        // 전체 최대 구매 횟수 확인
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
    public void TrackPurchase(ShopItemData item) // 각 아이템 별 하루 또는 전체 최대 구매 횟수 추적
    {
        if (!item.isUnlimited) // CSV에서 isUnlimited확인 가능
        {
            // 하루 최대 구매 횟수 증가
            if (item.maxDailyPurchase > 0) // CSV에서 확인 가능
            {
                // item.itemName을 키로 사용하여 dailyPurchaseCount 딕셔너리에 접근
                if (!dailyPurchaseCount.ContainsKey(item.itemName))
                    dailyPurchaseCount[item.itemName] = 0;
                dailyPurchaseCount[item.itemName]++;
            }

            // 전체 최대 구매 횟수 증가
            if (item.maxTotalPurchase > 0)
            {
                // item.itemName을 키로 사용하여 totalPurchaseCount 딕셔너리에 접근
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
            // 하루 구매 횟수 감소
            if (item.maxDailyPurchase > 0 &&
                dailyPurchaseCount.ContainsKey(item.itemName) &&
                dailyPurchaseCount[item.itemName] > 0)
            {
                dailyPurchaseCount[item.itemName]--;
                Debug.Log(item.itemName+ "하루 횟수 감소 {dailyPurchaseCount[item.itemName]}");
            }

            // 전체 구매 횟수 감소
            if (item.maxTotalPurchase > 0 &&
                totalPurchaseCount.ContainsKey(item.itemName) &&
                totalPurchaseCount[item.itemName] > 0)
            {
                totalPurchaseCount[item.itemName]--;
                Debug.Log(item.itemName + "전체 횟수 감소 {totalPurchaseCount[item.itemName]}");
            }
        }
        OnPurchaseStateChanged?.Invoke(item.itemName);
        Debug.Log(item.itemName + "이미지 변경 이벤트");
        Debug.Log(item.itemName + CanPurchaseItem(item));

    }


    public void ResetDailyPurchases()
    {
        dailyPurchaseCount.Clear(); // 나중에 하루 지나갈 때 호출
    }

    public void UpdateNormalGem(int _amount)
    {
        gem -= _amount;
        gemUI.text = gem.ToString();
        Debug.Log("일반재화 계산"+gem);
    }

    public void UpdateSpecialGem(int _amount)
    {
        specialGem -= _amount;
        specialGemUI.text = specialGem.ToString();
    }

    //임시 gem얻기
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
