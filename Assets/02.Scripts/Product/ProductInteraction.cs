using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ProductInteraction : MonoBehaviour
{
    private ProductMove productMove;
    private SpriteRenderer sprite;

    private int colorNumber = 0;

    private void Start()
    {
        productMove = GetComponent<ProductMove>();
        sprite = GetComponent<SpriteRenderer>();
    }

    //클릭했을 경우
    private void OnMouseDown()
    {
        //움직이는 중이면 클릭X
        if (productMove.isMove)
            return;

        SetColor();
    }

    private void SetColor()
    {
        switch (colorNumber)
        {
            //블랙 -> Red, Green, Blue
            case 0:
                //색깔 입힘
                sprite.color = Color.red;

                //색 입힘(1번)
                colorNumber++;

                //다시 움직이게 설정
                productMove.SetMove();

                break;

            //RGB -> Yellow, Cyan, Magenta
            case 1:
                //색깔 입힘
                sprite.color = Color.yellow;

                //색 입힘(1번)
                colorNumber++;

                //다시 움직이게 설정
                productMove.SetMove();

                break;

            //YCM -> White
            case 2:
                //색깔 입힘
                sprite.color = Color.white;

                //색 입힘(1번)
                colorNumber++;

                //다시 움직이게 설정
                productMove.SetMove();

                break;

            default:
                Debug.Log("잘못됨");
                break;
        }
    }
}
