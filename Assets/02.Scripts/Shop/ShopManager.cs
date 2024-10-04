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
    public SellingCardUI[] shopPanels;
    //public Button[] purchaseBtns; /// 수정중
    public int itemCount=0;

    private Dictionary<ShopItemSO, int> dailyPurchaseCount = new Dictionary<ShopItemSO, int>();
    private Dictionary<ShopItemSO, int> totalPurchaseCount = new Dictionary<ShopItemSO, int>();
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
            dailyPurchaseCount[shopItemSO[i]] = 0;
            totalPurchaseCount[shopItemSO[i]] = 0;
            shopPanelsGO[i].SetActive(true);
        }
        gemUI.text = gem.ToString();
        specialGemUI.text = specialGem.ToString();
        LoadPanel();
        UpdatePurchaseButtons();
    }


    public void UpdatePurchaseButtons()
    {
        for (int i = 0; i < shopItemSO.Length; i++)
        {
            var item = shopItemSO[i];
            bool canPurchase = false;
            if (item.isUnlimited)
            {
                canPurchase= ((gem >= item.price && item.gemType == ShopItemSO.GemType.NORMAL) ||
                (specialGem >= item.price && item.gemType == ShopItemSO.GemType.SPECIAL));
            }
            //if ((gem >= shopItemSO[i].price && shopItemSO[i].gemType == ShopItemSO.GemType.NORMAL) ||
                //(specialGem >= shopItemSO[i].price && shopItemSO[i].gemType == ShopItemSO.GemType.SPECIAL))
            //{
                //purchaseBtns[i].interactable = true;
            //}
            else
            {
                bool canBuyDaily = item.maxDailyPurchase == 0 || dailyPurchaseCount[item] < item.maxDailyPurchase;
                bool canBuyTotal = item.maxTotalPurchase == 0 || totalPurchaseCount[item] < item.maxTotalPurchase;
                if (canBuyDaily && canBuyTotal)
                {
                    canPurchase = ((gem >= item.price && item.gemType == ShopItemSO.GemType.NORMAL) ||
                    (specialGem >= item.price && item.gemType == ShopItemSO.GemType.SPECIAL));
                }
                //purchaseBtns[i].interactable = false;
            }
            // purchaseBtns[i].interactable = canPurchase; ////
        }
    }

    public bool CanPurchaseItem(ShopItemSO _item)
    {
        if (_item.isUnlimited)
        {
            return ((gem >= _item.price && _item.gemType == ShopItemSO.GemType.NORMAL) ||
                (specialGem >= _item.price && _item.gemType == ShopItemSO.GemType.SPECIAL));
        }
        else
        {
            // 최대 구매 제한이 있는 경우에는 "<"를 사용해야 함.
            bool canBuyDaily = _item.maxDailyPurchase == 0 || dailyPurchaseCount[_item] < _item.maxDailyPurchase;
            bool canBuyTotal = _item.maxTotalPurchase == 0 || totalPurchaseCount[_item] < _item.maxTotalPurchase;

            return (canBuyDaily && canBuyTotal) &&
                   ((gem >= _item.price && _item.gemType == ShopItemSO.GemType.NORMAL) ||
                   (specialGem >= _item.price && _item.gemType == ShopItemSO.GemType.SPECIAL));
        }
    }

    public void PurchaseItem(int _btnNum)
    {

        //if (shopItemSO[_btnNum].itemType == ShopItemSO.ItemType.SLOT)
        //{
        //    InventoryManager.Instance.ExpendSlot(2);
        //    gem -= shopItemSO[_btnNum].price;
        //    gemUI.text = "Gem : " + gem.ToString();
        //}
        //else
        //{
        var item = shopItemSO[_btnNum];

        if (CanPurchaseItem(item))
        {
            if (item.gemType == ShopItemSO.GemType.NORMAL && gem >= item.price)
            {
                if (item.itemType == ShopItemSO.ItemType.SLOT)
                {
                    ExpandSlot.Instance.UpdateSlot(2);
                }
                gem -= shopItemSO[_btnNum].price;
                gemUI.text = "일반 재화 : " + gem.ToString();
            }

            if (item.gemType == ShopItemSO.GemType.SPECIAL && specialGem >= item.price)
            {
                specialGem -= shopItemSO[_btnNum].price;
                specialGemUI.text = "특수 재화 : " + specialGem.ToString();
            }

            ++itemCount;
            InventoryUI.Instance.AddItem(item);
            Debug.Log(_btnNum + "넣음");

            dailyPurchaseCount[item]++;
            totalPurchaseCount[item]++;
            //}

            UpdatePurchaseButtons();
        }
    }

    //임시 gem얻기
    public void AddCoins()
    {
        gem++;
        gemUI.text = "일반 재화 : " + gem.ToString();
        UpdatePurchaseButtons();
    }

    public void AddSpecialGem()
    {
        specialGem++;
        specialGemUI.text = "특수 재화 : " + specialGem.ToString();
        UpdatePurchaseButtons();
    }
    //

    public void LoadPanel()
    {
        for(int i = 0; i < shopItemSO.Length; i++)
        {
            shopPanels[i].titleTxt.text = shopItemSO[i].itemName;
            shopPanels[i].costTxt.text = shopItemSO[i].price.ToString();
            //shopPanels[i].gemTxt.text = shopItemSO[i].gemTypeTxt;
            if (shopItemSO[i].gemType == ShopItemSO.GemType.NORMAL)
            {
                //shopPanels[i].gemTxt.text = "일반";
                //shopPanels[i].gemType.color = new Color(150/255f, 107/255f , 81/255f);
            }
            else if (shopItemSO[i].gemType == ShopItemSO.GemType.SPECIAL)
            {
                //shopPanels[i].gemTxt.text = "특수";
                //shopPanels[i].gemType.color = new Color(149 / 255f, 97 / 255f, 166 / 255f);
            }
            shopPanels[i].tempImg.sprite = shopItemSO[i].itemImg;
            
        }
    }

}
