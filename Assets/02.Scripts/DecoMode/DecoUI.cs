using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DecoUI : MonoBehaviour
{

    public static DecoUI Instance { get; private set;}

    public GameObject decoPanel;
    public GameObject decoPrefab;

    private Dictionary<ShopItemSO, GameObject> decoItems = new Dictionary<ShopItemSO, GameObject>();
    //private Dictionary<ShopItemSO, int> decoItemsCount = InventoryUI.Instance.GetInventoryDic();

    private void Awake()
    {
        if(Instance!=null)
        {
            Destroy(gameObject);
        }
        else
        {
            Instance = this;
        }
    }

    void Start()
    {
        UpdateDecoUI();   
    }


    void Update()
    {
        UpdateDecoUI();
    }

 public void UpdateDecoUI()
{
    // UI 갱신 전에 기존 UI 요소 초기화
    //ClearDecoUI();

    // InventoryUI에서 아이템 목록 가져옴
    Dictionary<ShopItemSO, int> decoItemsCount = InventoryUI.Instance.GetInventoryDic();

    // ItemType이 NONE 또는 SLOT이 아닌 아이템만 필터링하여 표시
    foreach (var decoItem in decoItemsCount)
    {
        if (decoItem.Key.itemType != ShopItemSO.ItemType.NONE && decoItem.Key.itemType != ShopItemSO.ItemType.SLOT)
        {
            GameObject decoItemGO;
            if (!decoItems.ContainsKey(decoItem.Key))
            {
                // 아이템이 없으면 새로 인스턴스화
                decoItemGO = Instantiate(decoPrefab, decoPanel.transform);
                decoItems[decoItem.Key] = decoItemGO;
            }
            else
            {
                // 이미 있는 경우 가져옴
                decoItemGO = decoItems[decoItem.Key];
            }

            // 아이템 정보를 UI에 설정
            var decoItemUI = decoItemGO.GetComponent<PurchasedCardUI>();
            //decoItemUI.SetItem(decoItem.Key, decoItem.Value); // 개수와 함께 아이템 정보 설정
        }
    }
}
}

