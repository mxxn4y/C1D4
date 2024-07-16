using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class ShopManager : MonoBehaviour
{
    public int gem;
    public TMP_Text gemUI;
    public GameObject[] shopPanelsGO;
    public ShopItemSO[] shopItemSO;
    public ShopTemplate[] shopPanels;
    public Button[] PurchaseBtns;


    // Start is called before the first frame update
    void Start()
    {
        //for (int i = 0; i < shopItemSO.Length; i++)
        //{
        //    shopPanelsGO[i].SetActive(true);
        //}
        gemUI.text = "Gem : " + gem.ToString();
        LoadPanel();
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    public void AddCoins()
    {
        gem++;
        gemUI.text = "Gem : " + gem.ToString();

    }

    public void LoadPanel()
    {
        for(int i = 0; i < shopItemSO.Length; i++)
        {
            shopPanels[i].titleTxt.text = shopItemSO[i].title;
            shopPanels[i].costTxt.text = "Cost : " + shopItemSO[i].cost.ToString();
        }
    }
}
