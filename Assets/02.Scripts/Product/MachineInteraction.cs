using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class MachineInteraction : MonoBehaviour
{
    [SerializeField] private FactoryGameManager.FactoryColor machineColor;

    public Product currentProduct { get; set; }
    private bool isClick = false;

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Product"))
        {
            isClick = true;
        }
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        {
            isClick = false;
        }
    }

    //클릭했을 경우
    private void OnMouseUp()
    {
        //현재 생성된 게 없으면 X
        if (currentProduct == null || isClick == false)
            return;

        //움직이는 중이면 클릭X
        if (currentProduct.isMove)
            return;

        currentProduct.SetColor(machineColor);
    }
}
