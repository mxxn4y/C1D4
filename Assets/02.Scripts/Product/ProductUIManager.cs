using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class ProductUIManager : MonoBehaviour
{
    [SerializeField] TMP_Text whiteProductText;
    [SerializeField] TMP_Text yellowProductText;
    [SerializeField] TMP_Text cyanProductText;
    [SerializeField] TMP_Text magentaProductText;

    public void SetProductNumberText(int _yellowNum, int _cyanNum, int _magentaNum, int _whiteNum)
    {
        whiteProductText.text = "W_PRODUCTS: " + _whiteNum;
        yellowProductText.text = "Y_PRODUCTS: " + _yellowNum;
        cyanProductText.text = "C_PRODUCTS: " + _cyanNum;
        magentaProductText.text = "M_PRODUCTS: " + _magentaNum;
    }
}
