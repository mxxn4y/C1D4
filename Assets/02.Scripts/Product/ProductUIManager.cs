using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class ProductUIManager : MonoBehaviour
{
    [SerializeField] TMP_Text productText;

    public void SetProductNumberText(int _productNumber)
    {
        productText.text = "PRODUCTS: " + _productNumber;
    }
}
