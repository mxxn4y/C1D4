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

    public void OnPointerClick(PointerEventData _eventData) //ī�� Ŭ��, �ε��� Ŭ��
    {      
        GameObject clickedObj = _eventData.pointerCurrentRaycast.gameObject;
        CollectCard collectCard = clickedObj.GetComponentInParent<CollectCard>();
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
        else if (collectCard!=null) //�� �κ� ���� �� �� -> ī�� �����տ� CardEvent ��ũ��Ʈ ���� �� �� �Ŷ�..
                                                                            //�׷��� �׳� ī�� ������ �� �̺�Ʈ Ʈ���ŷ� ����
        {
            //card.CardClick(); 
            Debug.Log("ī�� Ŭ�� �̺�Ʈ ȣ��");
            Debug.Log($"CollectCard�� Minion ������: {collectCard.minionData.Data.name}");
            if (PlayerData.Instance.SelectedMinions.Contains(collectCard.minionData))
            {
                PlayerData.Instance.SelectedMinions.Remove(collectCard.minionData);
                collectCard.SetUnClickImg();
                Debug.Log("���õ� ����Ʈ�� Ŭ�� �̴Ͼ��� ���� ��");

                foreach (var item in PlayerData.Instance.SelectedMinions)
                {
                    Debug.Log("���õ� ����Ʈ���� ���� �̴Ͼ�"+item.Data.name);
                }
            }
            else
            {
                PlayerData.Instance.SelectedMinions.Add(collectCard.minionData);
                collectCard.SetClickImg();
                Debug.Log("���õ� ����Ʈ�� Ŭ�� �̴Ͼ��� ���� ��");

                foreach (var item in PlayerData.Instance.SelectedMinions)
                {
                    Debug.Log("���õ� ����Ʈ���� �߰�:" + item.Data.name);
                }
            }
        }
        else
        {
            Debug.Log("");
        }

    }

    public void OnPointerEnter(PointerEventData _eventData) //�Ӽ� �����ױ�
    {
        // �ʵ� �׳� �̺�Ʈ Ʈ���ŷ� ����
      /*
         GameObject hoveredObj = _eventData.pointerCurrentRaycast.gameObject;
        Debug.Log("Ŭ���� ������Ʈ: " + hoveredObj.name);

        if (hoveredObj != null && hoveredObj.TryGetComponent<CardEvent>(out CardEvent card))
        {
            Debug.Log($"ī�� ������ ����: {hoveredObj.name}");
            card.CardEnter(); // CardEvent ��ũ��Ʈ�� CardEnter �޼��� ȣ��
        }
        else
        {
            Debug.Log("ī�� ������ ������ �ƴմϴ�.");
        }
       */

    }

    public void OnPointerExit(PointerEventData _eventData)
    {
        // �ʵ� �̺�Ʈ Ʈ���ŷ�
    }

}
