using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;
using TMPro;


public class ShopEvent : MonoBehaviour, IPointerClickHandler, IPointerEnterHandler, IPointerExitHandler
{

    public void OnPointerEnter(PointerEventData _event)
    {

    }

    public void OnPointerExit(PointerEventData _event)
    {

    }
 
    /// public static ShopEvent Instance;

    public List<ShopItemSO> normalItems;  // 일반 아이템 리스트
    public List<ShopItemSO> specialItems; // 특수 아이템 리스트
    public Transform itemGrid;  // 4번 이미지의 GridLayout
    public Button purchaseButton;  // 구매하기 버튼
    public TextMeshProUGUI purchaseButtonText;  // 구매하기 버튼의 텍스트
    public GameObject shopUI;  // 상점 UI 창
    public Button closeButton;  // 6번 이미지 (상점 닫기 버튼)
    public GameObject cardPrefab;

    private int totalPrice = 0;
    private List<ShopItemSO> currentItems;  // 현재 표시 중인 아이템 리스트

    private void Start()
    {
        UpdateItemList(normalItems);
        purchaseButton.onClick.AddListener(OnPurchase);
        closeButton.onClick.AddListener(CloseShop);

    }

    // IPointerClickHandler 구현
    public void OnPointerClick(PointerEventData eventData)
    {
        // 일반 아이템 선택
        if (eventData.pointerCurrentRaycast.gameObject.name == "Index1")
        {
            UpdateItemList(normalItems);
            ResetTotalPrice();
            Debug.Log("인덱스1");
        }
        // 특수 아이템 선택
        else if (eventData.pointerCurrentRaycast.gameObject.name == "Index2")
        {
            UpdateItemList(specialItems);
            ResetTotalPrice();
            Debug.Log("인덱스2");
        }
    }

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
            cardUI.SetCardUI(itemData);

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

    // 아이템 클릭 시 총액 계산
    public void OnItemClick(ShopItemSO itemData)
    {
        totalPrice += itemData.Price;
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
        purchaseButtonText.text = "구매하기 " + totalPrice ;
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
