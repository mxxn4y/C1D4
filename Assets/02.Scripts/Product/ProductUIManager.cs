using System.Collections;
using System.Collections.Generic;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

public class ProductUIManager : MonoBehaviour
{
    [SerializeField] Color defaultColor;
    [SerializeField] Color red;
    [SerializeField] Color yellow;
    [SerializeField] Color blue;
    [SerializeField] Color ornage;
    [SerializeField] Color green;
    [SerializeField] Color purple;

    [SerializeField] Image[] productColors;
    [SerializeField] TMP_Text productText;

    public void SetProductColor(FactoryGameManager.FactoryColor _color, int _index)
    {
        switch (_color)
        {
            case FactoryGameManager.FactoryColor.BLACK:
                productColors[_index - 1].color = Color.black;
                break;
            case FactoryGameManager.FactoryColor.RED:
                productColors[_index - 1].color = red;
                break;
            case FactoryGameManager.FactoryColor.YELLOW:
                productColors[_index - 1].color = yellow;
                break;
            case FactoryGameManager.FactoryColor.BLUE:
                productColors[_index - 1].color = blue;
                break;
            case FactoryGameManager.FactoryColor.ORANGE:
                productColors[_index - 1].color = ornage;
                break;
            case FactoryGameManager.FactoryColor.GREEN:
                productColors[_index - 1].color = green;
                break;
            case FactoryGameManager.FactoryColor.PURPLE:
                productColors[_index - 1].color = purple;
                break;
            default:
                productColors[_index - 1].color = Color.white;
                break;
        }

        productText.text = "PRODUCT : " + _index;
    }
}
