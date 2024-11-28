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
        else if (clickedObj.TryGetComponent<CardEvent>(out CardEvent card)) //이 부분 구현 안 돼 -> 카드 프리팹에 CardEvent 스크립트 부착 안 할 거라..
                                                                            //그래서 그냥 카드 프리팹 내 이벤트 트리거로 구현
        {
            card.CardClick(); 
            Debug.Log("카드 클릭 이벤트 호출");
        }
        else
        {
            Debug.Log("IndexEvent 컴포넌트가 없습니다.");
        }
    }

    public void OnPointerEnter(PointerEventData _eventData) //속성 보여죽기
    {
        // 너도 그냥 이벤트 트리거로 구현
      /*
         GameObject hoveredObj = _eventData.pointerCurrentRaycast.gameObject;
        Debug.Log("클릭된 오브젝트: " + hoveredObj.name);

        if (hoveredObj != null && hoveredObj.TryGetComponent<CardEvent>(out CardEvent card))
        {
            Debug.Log($"카드 프리팹 진입: {hoveredObj.name}");
            card.CardEnter(); // CardEvent 스크립트의 CardEnter 메서드 호출
        }
        else
        {
            Debug.Log("카드 프리팹 영역이 아닙니다.");
        }
       */

    }

    public void OnPointerExit(PointerEventData _eventData)
    {
        // 너도 이벤트 트리거로
    }

}
