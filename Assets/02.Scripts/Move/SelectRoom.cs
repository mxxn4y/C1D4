using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SelectRoom : MonoBehaviour
{
    //���̶���Ʈ ������Ʈ(�ڽ� ������Ʈ�� ��)
    [SerializeField] GameObject highlight;
    //���� ��ġ ���̵�
    [SerializeField] string roomName;

    private void OnMouseEnter()
    {
        //�ݶ��̴� ���� ���� �� TRUE
        highlight.SetActive(true);
    }
    private void OnMouseExit()
    {
        //�ݶ��̴� ������ ���� �� FALSE
        highlight.SetActive(false);
    }

    //Ŭ������ �� ��ġ ����
    private void OnMouseDown()
    {
        //String �Ű������� ����
        Debug.Log("Ŭ��");
        CharacterMove.SetMove(roomName);
    }
}
