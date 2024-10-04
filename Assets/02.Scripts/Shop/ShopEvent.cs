using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class ShopEvent : MonoBehaviour, IPointerClickHandler, IPointerEnterHandler, IPointerExitHandler
{
    public void OnPointerClick(PointerEventData _event)
    {
        GameObject clickedObject = _event.pointerCurrentRaycast.gameObject;
        Debug.Log(clickedObject);
        //Debug.Log("Click");
    }

    public void OnPointerEnter(PointerEventData _event)
    {

    }

    public void OnPointerExit(PointerEventData _event)
    {

    }

    void Start()
    {
        
    }

    void Update()
    {
        
    }
}
