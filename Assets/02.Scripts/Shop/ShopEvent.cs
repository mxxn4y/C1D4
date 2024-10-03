using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class ShopEvent : MonoBehaviour, IPointerClickHandler
{
    public void OnPointerClick(PointerEventData _event)
    {
        Debug.Log("Click");
    }

    void Start()
    {
        
    }

    void Update()
    {
        
    }
}
