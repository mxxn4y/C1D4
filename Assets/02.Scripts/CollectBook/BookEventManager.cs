using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using static BookEventManager;

public class BookEventManager : MonoBehaviour,IPointerClickHandler,IPointerEnterHandler,IPointerExitHandler
{

    public enum IndexType { PASSION,CALM,WISDOM }
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
                // ������ �ϴ����� ��ġ ����
                rectTransform.position = new Vector2(mousePosition.x + 80, mousePosition.y - 80);
            }
        }
    }

        public void OnPointerClick(PointerEventData _eventData) //ī�� Ŭ��, �ε��� Ŭ��
        {
            GameObject clickedObj = _eventData.pointerCurrentRaycast.gameObject;
            CollectCard collectCard = clickedObj.GetComponentInParent<CollectCard>(); // �ݷ�Ʈ���� ��� ī�� �����տ� �����Ǿ��ִ� ��ũ��Ʈ

            Debug.Log("Ŭ���� ������Ʈ: " + clickedObj.name);

            if (clickedObj == null)
            {
                Debug.Log("����� ������Ʈ �ƹ��͵� ����");
            }
            else if (clickedObj.TryGetComponent<IndexEvent>(out IndexEvent index))
            {
                index.IndexEventAction();
                Debug.Log("IndexEventAction");
            }
            else if (collectCard != null && collectCard.IsUnlockCard&& (!collectCard.isExhausted))
            {

                //card.CardClick();
                Debug.Log($"CollectCard�� Minion ������: {collectCard.minionData.Data.name}");

                if (PlayerData.Instance.SelectedMinions.Contains(collectCard.minionData))
                {
                    PlayerData.Instance.SelectedMinions.Remove(collectCard.minionData);
                    collectCard.SetUnClickImg();
                    Debug.Log("���õ� ����Ʈ�� Ŭ�� �̴Ͼ��� ���� ��");

                    foreach (var item in PlayerData.Instance.SelectedMinions)
                    {
                        Debug.Log("���õ� ����Ʈ���� ���� �̴Ͼ�: " + item.Data.name);
                    }
                }
                else if (PlayerData.Instance.SelectedMinions.Count < chooseableCount)
            {
                    PlayerData.Instance.SelectedMinions.Add(collectCard.minionData);
                    collectCard.SetClickImg();
                    Debug.Log("���õ� ����Ʈ�� Ŭ�� �̴Ͼ��� ���� ��");

                    foreach (var item in PlayerData.Instance.SelectedMinions)
                    {
                        Debug.Log("���õ� ����Ʈ���� �߰�:" + item.Data.name);
                    }
                }
            else
            {
                Debug.Log("3�������� ���� ����");
            }
            }
            else
            {
                Debug.Log("");
            }

        }

        public void OnPointerEnter(PointerEventData _eventData)
        {
            GameObject hoveredObj = _eventData.pointerCurrentRaycast.gameObject;
            CollectCard collectCard = hoveredObj.GetComponentInParent<CollectCard>();

            Debug.Log("ȣ������ ������Ʈ: " + hoveredObj.name);

            if (hoveredObj != null && collectCard != null && collectCard.IsUnlockCard)
            {
            if(collectCard == null)
            {
                Debug.Log("콜렉트카드 없음");
            }
                if (cardPropertyPrefab != null)
                {
                    GameObject cardPropertyObject = Instantiate(cardPropertyPrefab, transform);
                    cardProperty = cardPropertyObject.GetComponent<CardProperty>();
                    currentCardProperty = cardPropertyObject;
                    cardProperty.DisplayCardProperty(collectCard.minionData);
                    Debug.Log($"ȣ������ ������Ʈ�� Minion ������: {collectCard.minionData.Data.name}");
                }
                else
                {
                    Debug.LogError("CardPropertyPrefab�� �Ҵ���� ����");
                }
            }
            else
                {
                    Debug.Log("ȣ���� ������Ʈ�� �̴Ͼ� ������ ����");
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

 

