using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;
using TMPro;

public class ShopUI : MonoBehaviour
{
    public static ShopUI Instance { get; private set; }

    public Transform itemGrid;
    public Button purchaseButton;
    public TextMeshProUGUI purchaseButtonText;
    public GameObject shopUI;
    public Button closeButton;
    public GameObject cardPrefab;
    public GameObject MoveScene;

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
        }
        else
        {
            Destroy(gameObject);
        }
    }

    public void UpdateItemList(List<ShopItemData> items)
    {
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
            newItem.AddComponent<Button>().onClick.AddListener(() => ShopEvent.Instance.OnItemClick(itemData));  // 클릭 시 아이템 추가
        }
    }

    public void UpdatePurchaseButtonText(int _amount)
    {
        purchaseButtonText.text = "구매하기 " + _amount.ToString();
    }

    public void ResetTotalPrice()
    {
        ShopEvent.Instance.totalPrice = 0;
        purchaseButtonText.text = "구매하기 ";
    }

    public void CloseShop()
    {
        shopUI.SetActive(false);  // 상점 UI를 비활성화
        MoveScene.SetActive(true);
        AudioManager.Instance.PlayAudio("02.b_lobby", true, SoundType.EFFECT, 0);
        AudioManager.Instance.StopAudio("03.b_shop");
    }

    public void SoldOutUI()
    {
       
        Debug.Log("솔드아웃ui");
    }

    // 구매 횟수가 초과되면 그 해당 카드 아이템 프리팹UI의 Item Image의 Image컴포넌트에서 이미지가 솔드아웃 이미지로 바뀌었으면 좋겠어
}
