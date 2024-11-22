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
        Debug.Log("Ŭ���� ������Ʈ: " + clickedObj.name);

        if (clickedObj == null)
        {
            Debug.Log("����� ������Ʈ �ƹ��͵� ����");
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
            Debug.Log("IndexEvent ������Ʈ�� �����ϴ�.");
        }
    }

    public void OnPointerEnter(PointerEventData _eventData) //�Ӽ� �����ױ�
    {
            //Debug.Log("ī�� ������ ����");
            //CardEvent.Instance.CardEnter();
        
    }

    public void OnPointerExit(PointerEventData _eventData)
    {
            //CardEvent.Instance.CardExit();
            //Debug.Log("ī�� �����տ� ����");
    }

}
