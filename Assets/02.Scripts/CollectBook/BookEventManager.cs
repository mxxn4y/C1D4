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
        CollectCard collectCard = clickedObj.GetComponentInParent<CollectCard>();
        Debug.Log("클릭된 오브젝트: " + clickedObj.name);

        if (clickedObj == null)
        {
            Debug.Log("적용될 오브젝트 아무것도 없음");
        }
        else if (clickedObj.TryGetComponent<IndexEvent>(out IndexEvent index))
        {
            index.IndexEventAction();
            Debug.Log("IndexEventAction");
        }
        else if (collectCard!=null) //이 부분 구현 안 돼 -> 카드 프리팹에 CardEvent 스크립트 부착 안 할 거라..
                                                                            //그래서 그냥 카드 프리팹 내 이벤트 트리거로 구현
        {
            //card.CardClick(); 
            Debug.Log("카드 클릭 이벤트 호출");
            Debug.Log($"CollectCard의 Minion 데이터: {collectCard.minionData.Data.name}");
            if (PlayerData.Instance.SelectedMinions.Contains(collectCard.minionData))
            {
                PlayerData.Instance.SelectedMinions.Remove(collectCard.minionData);
                collectCard.SetUnClickImg();
                Debug.Log("선택된 리스트에 클릭 미니언이 있을 때");

                foreach (var item in PlayerData.Instance.SelectedMinions)
                {
                    Debug.Log("선택된 리스트에서 남은 미니언"+item.Data.name);
                }
            }
            else
            {
                PlayerData.Instance.SelectedMinions.Add(collectCard.minionData);
                collectCard.SetClickImg();
                Debug.Log("선택된 리스트에 클릭 미니언이 없을 때");

                foreach (var item in PlayerData.Instance.SelectedMinions)
                {
                    Debug.Log("선택된 리스트에서 추가:" + item.Data.name);
                }
            }
        }
        else
        {
            Debug.Log("");
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
