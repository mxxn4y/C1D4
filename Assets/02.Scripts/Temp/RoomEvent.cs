using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class RoomEvent : MonoBehaviour,IPointerClickHandler
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void OnPointerClick(PointerEventData _eventData)
    {
        GameObject clickedObj = _eventData.pointerCurrentRaycast.gameObject;
        InteractableObject interactObj = clickedObj.GetComponent<InteractableObject>();

        if (interactObj != null)
        {
            interactObj.LoadTargetScene();
        }
        else
        {
            Debug.Log("interact 오브젝트 없음");
        }
    }
}
