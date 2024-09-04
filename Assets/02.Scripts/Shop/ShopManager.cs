using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;


public class ShopManager : MonoBehaviour
{

    public static ShopManager Instance { get; private set; }

    private int gem;
    public int specialGem;
    public TMP_Text gemUI;
    public TMP_Text specialGemUI;
    public GameObject[] shopPanelsGO;
    public ShopItemSO[] shopItemSO;
    public ShopTemplate[] shopPanels;
    public Button[] purchaseBtns;
    public int itemCount=0;

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
        for (int i = 0; i < shopItemSO.Length; i++)
        {
            shopPanelsGO[i].SetActive(true);
        }
        gemUI.text = "Gem : " + gem.ToString();
        specialGemUI.text = "Special Gem : " + specialGem.ToString();
        LoadPanel();
        UpdatePurchaseButtons();
    }


    public void UpdatePurchaseButtons()
    {
        for (int i = 0; i < shopItemSO.Length; i++)
        {
            if ((gem >= shopItemSO[i].price && shopItemSO[i].gemType == ShopItemSO.GemType.NORMAL) ||
                (specialGem >= shopItemSO[i].price && shopItemSO[i].gemType == ShopItemSO.GemType.SPECIAL))
            {
                purchaseBtns[i].interactable = true;
            }
            else
            {
                purchaseBtns[i].interactable = false;
            }
        }
    }

    public void PurchaseItem(int _btnNum)
    {

        if (shopItemSO[_btnNum].itemType == ShopItemSO.ItemType.SLOT)
        {
            InventoryManager.Instance.ExpendSlot(2);
            gem -= shopItemSO[_btnNum].price;
            gemUI.text = "Gem : " + gem.ToString();
        }
        else
        {
            if ((shopItemSO[_btnNum].gemType == ShopItemSO.GemType.NORMAL) && gem >= shopItemSO[_btnNum].price)
            {
                gem -= shopItemSO[_btnNum].price;
                gemUI.text = "Gem : " + gem.ToString();
            }

            if ((shopItemSO[_btnNum].gemType == ShopItemSO.GemType.SPECIAL) && specialGem >= shopItemSO[_btnNum].price)
            {
                specialGem -= shopItemSO[_btnNum].price;
                specialGemUI.text = "Special Gem : " + specialGem.ToString();
            }

            ++itemCount;
            InventoryManager.Instance.AddItem(shopItemSO[_btnNum]);
            Debug.Log(_btnNum + "����");
        }

        UpdatePurchaseButtons();

    }

    //�ӽ� gem���
    public void AddCoins()
    {
        gem++;
        gemUI.text = "Gem : " + gem.ToString();
        UpdatePurchaseButtons();
    }

    public void AddSpecialGem()
    {
        specialGem++;
        specialGemUI.text = "Special Gem : " + specialGem.ToString();
        UpdatePurchaseButtons();
    }
    //

    public void LoadPanel()
    {
        for(int i = 0; i < shopItemSO.Length; i++)
        {
            shopPanels[i].titleTxt.text = shopItemSO[i].itemName;
            shopPanels[i].costTxt.text = "Cost : " + shopItemSO[i].price.ToString();
            shopPanels[i].tempImg.sprite = shopItemSO[i].itemImg;
        }
    }

}
