using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;
using TMPro;
//ShopTable에서 itemData 받아서 GemType.NORMAL인 아이템은 normalItem에 넣고, GemTyoe.SPECIAL인 아이템은 specialItem에 넣고,ItemType이 SLOT인 아이템은 slotItem에 넣어야 돼. 그런 다음에 Index1을 클릭하면 normalItem안에 있는 카드 아이템 UI가 나오게, Index2를 클릭하면 specialItem 카드 아이템 UI가 나오게, Index3를 클릭하면 slotItem 카드 아이템 UI나오게 OnPointerClick함수랑 UpdateItemList함수 변경해줘

public class ShopEvent : MonoBehaviour, IPointerClickHandler, IPointerEnterHandler, IPointerExitHandler
{

    public void OnPointerEnter(PointerEventData _event)
    {

    }

    public void OnPointerExit(PointerEventData _event)
    {

    }

    /// public static ShopEvent Instance;

    //public List<ShopItemData> normalItems;  // 일반 아이템 리스트
    //public List<ShopItemData> specialItems; // 특수 아이템 리스트

    public List<ShopItemData> normalItem=new List<ShopItemData>(); // 수정
    public List<ShopItemData> specialItem=new List<ShopItemData>();
    public List<ShopItemData> slotItem=new List<ShopItemData>(); //

    public Transform itemGrid;
    public Button purchaseButton;  // 구매하기 버튼
    public TextMeshProUGUI purchaseButtonText;  // 구매하기 버튼의 텍스트
    public GameObject shopUI;  // 상점 UI 창
    public Button closeButton;
    public GameObject cardPrefab;

    private int totalPrice = 0;
    private List<ShopItemData> currentItems;  // 현재 표시 중인 아이템 리스트
    // private List <ShopItemData> currentItems;

    private void Start()
    {
        InitializeItems();

        if (normalItem == null || specialItem == null || slotItem == null)
        {
            Debug.LogError("아이템 리스트가 null 상태입니다.");
            return;
        }

        if (normalItem.Count > 0)
        {
            UpdateItemList(normalItem);
        }
        else
        {
            Debug.LogError("Normal Item 리스트가 비어 있습니다.");

            UpdateItemList(normalItem);
            purchaseButton.onClick.AddListener(OnPurchase);
            closeButton.onClick.AddListener(CloseShop);
        }
    }

    private void InitializeItems()
    {

        if (ShopTable.Instance.shopCSV == null || ShopTable.Instance.shopCSV.Count == 0)
        {
            Debug.LogError("shopCSV 데이터가 없습니다.");
            return;
        }


        foreach (var data in ShopTable.Instance.shopCSV)
        {
            ShopItemData itemData = ShopTable.Instance.GetData(data["itemName"].ToString());
            if (itemData.itemType != ItemType.SLOT)
            {
                switch (itemData.gemType)
                {
                    case GemType.NORMAL:
                        normalItem.Add(itemData);
                        break;
                    case GemType.SPECIAL:
                        specialItem.Add(itemData);
                        break;
                }
            }
            else
            {
                slotItem.Add(itemData);
            }
        }
        Debug.Log($"Normal Item Count: {normalItem.Count}");
        Debug.Log($"Special Item Count: {specialItem.Count}");
        Debug.Log($"Slot Item Count: {slotItem.Count}");
    }

    private void UpdateItemList(List<ShopItemData> items)
    {
        if (items == null)
        {
            Debug.LogError("items 리스트가 null 상태입니다.");
            return;
        }

        // itemGrid가 null인지 확인
        if (itemGrid == null)
        {
            Debug.LogError("itemGrid가 null 상태입니다.");
            return;
        }

        // cardPrefab이 null인지 확인
        if (cardPrefab == null)
        {
            Debug.LogError("cardPrefab이 null 상태입니다.");
            return;
        }
        currentItems = items;
        foreach (Transform item in itemGrid)
        {
            Destroy(item.gameObject);  // 기존 아이템 삭제
        }

        foreach (ShopItemData itemData in items)
        {
            GameObject newItem = Instantiate(cardPrefab, itemGrid);
            newItem.name = itemData.itemName;
            SellingCardUI cardUI = newItem.GetComponent<SellingCardUI>();
            cardUI.SetCardUI(itemData);
            newItem.AddComponent<Button>().onClick.AddListener(() => OnItemClick(itemData));  // 클릭 시 아이템 추가
        }
    }


    // IPointerClickHandler 구현
    public void OnPointerClick(PointerEventData eventData)
    {
        string indexName = eventData.pointerCurrentRaycast.gameObject.name;
        switch (indexName)
        {
            case "Index1":
                UpdateItemList(normalItem);
                ResetTotalPrice();
                Debug.Log("인덱스1");
                break;

            case "Index2":
                UpdateItemList(specialItem);
                ResetTotalPrice();
                Debug.Log("인덱스2");
                break;

            case "Index3":
                UpdateItemList(slotItem);
                ResetTotalPrice();
                Debug.Log("인덱스3");
                break;
        }
    }
    /*
        private void UpdateItemList(List<ShopItemSO> items)
        {
            currentItems = items;
            foreach (Transform item in itemGrid)
            {
                Destroy(item.gameObject);  // 기존 아이템 삭제
            }

            foreach (ShopItemSO itemData in items)
            {
                GameObject newItem = Instantiate(cardPrefab, itemGrid);
                //GameObject newItem = new GameObject();  // 새로운 아이템 오브젝트 생성
                newItem.name = itemData.itemName;
                SellingCardUI cardUI = newItem.GetComponent<SellingCardUI>();

                // 아이템 정보를 카드에 설정
                //// cardUI.SetCardUI(itemData);
                /// SellingCardUI.SetCardText();

                //SellingCardUI cardUI = newItem.GetComponent<SellingCardUI>();
                //if (cardUI != null)
                //{
                //    cardUI.SetCardUI(itemData);  // 아이템 데이터 반영
                //}

                newItem.AddComponent<Button>().onClick.AddListener(() => OnItemClick(itemData));  // 클릭 시 아이템 추가
                // 여기서 아이템 UI 요소를 추가하세요 (텍스트, 이미지 등)
                //newItem.transform.SetParent(itemGrid);
            }
        }
    */

    // 아이템 클릭 시 총액 계산
    public void OnItemClick(ShopItemData _itemData)
    {
        totalPrice += _itemData.price;
        UpdatePurchaseButtonText();

        //SellingCardUI.Instance.SetCardUI(itemData);
    }

    // 구매하기 버튼 클릭 시
    private void OnPurchase()
    {
        // 구매 처리 로직
        Debug.Log("총 구매 금액: " + totalPrice);
        ResetTotalPrice();
    }

    // 구매 버튼 텍스트 업데이트
    private void UpdatePurchaseButtonText()
    {
        purchaseButtonText.text = "구매하기 " + totalPrice;
    }

    // 총액 초기화
    private void ResetTotalPrice()
    {
        totalPrice = 0;
        UpdatePurchaseButtonText();
    }

    // 상점 닫기
    private void CloseShop()
    {
        shopUI.SetActive(false);  // 상점 UI를 비활성화
    }
}
