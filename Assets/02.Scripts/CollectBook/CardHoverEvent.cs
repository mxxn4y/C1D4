using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class CardHoverEvent : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
{
    public enum IndexType { PASSION, CALM, WISDOM }
    private CardProperty cardProperty;
    public GameObject cardPropertyPrefab;
    private GameObject currentCardProperty;
    private GameObject collectBookScene;
    public int chooseableCount = 3;

    void Start()
    {
        collectBookScene = GameObject.FindGameObjectWithTag("CollectBookScene");
    }

    void Update()
    {
        if (currentCardProperty != null)
        {
            Vector2 mousePosition = Input.mousePosition;
            RectTransform rectTransform = currentCardProperty.GetComponent<RectTransform>();
            if (rectTransform != null)
            {
                //         ϴ        ġ     
                rectTransform.position = new Vector2(mousePosition.x + 180, mousePosition.y - 180);
            }
        }
    }


    public void OnPointerEnter(PointerEventData _eventData)
    {
        GameObject hoveredObj = _eventData.pointerCurrentRaycast.gameObject;
        CollectCard collectCard = hoveredObj.GetComponentInParent<CollectCard>();

        Debug.Log("ȣ             Ʈ: " + hoveredObj.name);

        if (hoveredObj != null && collectCard != null && collectCard.IsUnlockCard)
        {
            if (collectCard == null)
            {
                Debug.Log("콜렉트카드 없음");
            }
            if (cardPropertyPrefab != null)
            {
                GameObject cardPropertyObject = Instantiate(cardPropertyPrefab, collectBookScene.transform);
                cardProperty = cardPropertyObject.GetComponent<CardProperty>();
                currentCardProperty = cardPropertyObject;
                cardProperty.DisplayCardProperty(collectCard.minionData);
                Debug.Log($"ȣ             Ʈ   Minion       : {collectCard.minionData.Data.name}");
            }
            else
            {
                Debug.LogError("CardPropertyPrefab    Ҵ         ");
            }
        }
        else
        {
            Debug.Log("ȣ           Ʈ    ̴Ͼ             ");
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
}
