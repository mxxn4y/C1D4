using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using static BookEventManager;

public class BookEventManager : MonoBehaviour,IPointerClickHandler//,IPointerEnterHandler,IPointerExitHandler
{

    public enum IndexType { PASSION,CALM,WISDOM }
    private CardProperty cardProperty;
    public GameObject cardPropertyPrefab;
    private GameObject currentCardProperty;
    public int chooseableCount=3;

    void Start()
    {

    }

    void Update()
    {
        if (currentCardProperty != null)
        { 
            Vector2 mousePosition = Input.mousePosition;
            RectTransform rectTransform = currentCardProperty.GetComponent<RectTransform>();
            if (rectTransform != null)
            {
                // 오른쪽 하단으로 위치 조정
                rectTransform.position = new Vector2(mousePosition.x + 80, mousePosition.y - 80);
            }
        }
    }

        public void OnPointerClick(PointerEventData _eventData) //카드 클릭, 인덱스 클릭
        {
            GameObject clickedObj = _eventData.pointerCurrentRaycast.gameObject;
            CollectCard collectCard = clickedObj.GetComponentInParent<CollectCard>(); // 콜렉트북의 모든 카드 프리팹에 부착되어있는 스크립트

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
            else if (collectCard != null && collectCard.IsUnlockCard&& (!collectCard.isExhausted))
            {

                //card.CardClick();
                Debug.Log($"CollectCard의 Minion 데이터: {collectCard.minionData.Data.name}");

                if (PlayerData.Instance.SelectedMinions.Contains(collectCard.minionData))
                {
                    PlayerData.Instance.SelectedMinions.Remove(collectCard.minionData);
                    collectCard.SetUnClickImg();
                    Debug.Log("선택된 리스트에 클릭 미니언이 있을 때");

                    foreach (var item in PlayerData.Instance.SelectedMinions)
                    {
                        Debug.Log("선택된 리스트에서 남은 미니언: " + item.Data.name);
                    }
                }
                else if (PlayerData.Instance.SelectedMinions.Count < chooseableCount)
            {
                    PlayerData.Instance.SelectedMinions.Add(collectCard.minionData);
                    collectCard.SetClickImg();
                    Debug.Log("선택된 리스트에 클릭 미니언이 없을 때");

                    foreach (var item in PlayerData.Instance.SelectedMinions)
                    {
                        Debug.Log("선택된 리스트에서 추가:" + item.Data.name);
                    }
                }
            else
            {
                Debug.Log("3개까지만 선택 가능");
            }
            }
            else
            {
                Debug.Log("");
            }

        }

    /*
        public void OnPointerEnter(PointerEventData _eventData)
        {
            GameObject hoveredObj = _eventData.pointerCurrentRaycast.gameObject;
            CollectCard collectCard = hoveredObj.GetComponentInParent<CollectCard>();

            Debug.Log("호버링된 오브젝트: " + hoveredObj.name);

            if (hoveredObj != null && collectCard != null && collectCard.IsUnlockCard)
            {
                if (cardPropertyPrefab != null)
                {
                    GameObject cardPropertyObject = Instantiate(cardPropertyPrefab, transform);
                    cardProperty = cardPropertyObject.GetComponent<CardProperty>();
                    currentCardProperty = cardPropertyObject;
                    cardProperty.DisplayCardProperty(collectCard.minionData);
                    Debug.Log($"호버링된 오브젝트의 Minion 데이터: {collectCard.minionData.Data.name}");
                }
                else
                {
                    Debug.LogError("CardPropertyPrefab이 할당되지 않음");
                }
            }
            else
                {
                    Debug.Log("호버링 오브젝트의 미니언 데이터 없음");
                }
        }

        public void OnPointerExit(PointerEventData _eventData)
        {

        if (currentCardProperty != null) 
        { 
            Destroy(currentCardProperty); 
            currentCardProperty = null; 
        }
    } 
    */
}

 

