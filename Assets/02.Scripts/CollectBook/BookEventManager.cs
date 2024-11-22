using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using static BookEventManager;

public class BookEventManager : MonoBehaviour,IPointerClickHandler,IPointerEnterHandler,IPointerExitHandler
{

    public enum IndexType { PASSION,CALM,WISDOM }

    void Start()
    {
        
    }

    void Update()
    {
        
    }

    public void OnPointerClick(PointerEventData _eventData) //카드 클릭, 인덱스 클릭
    {      
        GameObject clickedObj = _eventData.pointerCurrentRaycast.gameObject;
        Debug.Log("클릭된 오브젝트: " + clickedObj.name);

        if (clickedObj == null)
        {
            Debug.Log("적용될 오브젝트 아무것도 없음");
        }
        else if (clickedObj.TryGetComponent<IndexEvent>(out IndexEvent index))
        {
            index.IndexEventAction();
        }
        else if (!CardEvent.Instance==null)
        {
            CardEvent.Instance.CardClick();
        }
        else
        {
            Debug.Log("IndexEvent 컴포넌트가 없습니다.");
        }
    }

    public void OnPointerEnter(PointerEventData _eventData) //속성 보여죽기
    {
            //Debug.Log("카드 프리팹 들어옴");
            //CardEvent.Instance.CardEnter();
        
    }

    public void OnPointerExit(PointerEventData _eventData)
    {
            //CardEvent.Instance.CardExit();
            //Debug.Log("카드 프리팹에 나감");
    }

}
