using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class CardHoverEvent : MonoBehaviour,IPointerEnterHandler,IPointerExitHandler
{
    public enum IndexType { PASSION, CALM, WISDOM }
    private CardProperty cardProperty;
    public GameObject cardPropertyPrefab;
    private GameObject currentCardProperty;
    public int chooseableCount = 3;

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
                //         ϴ        ġ     
                rectTransform.position = new Vector2(mousePosition.x + 80, mousePosition.y - 80);
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
                GameObject cardPropertyObject = Instantiate(cardPropertyPrefab, collectCard.transform.parent);
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
