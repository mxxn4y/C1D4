using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SelectRoom : MonoBehaviour
{
    //���̶���Ʈ ������Ʈ(�ڽ� ������Ʈ�� ��)
    [SerializeField] GameObject highlight;
    //���� ��ġ ���̵�
    [SerializeField] string roomName;

    [SerializeField] CharacterMove characterMove;


    //������ �� �����ߴ��� �Ǻ�
    public void SetStart(string _prevRoom)
    {
        if (_prevRoom == "Morning")
        {

        }
    }
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

    private void OnMouseDown()
    {
        //Ŭ������ �� ������ ��ġ ����
        characterMove?.SetDestination(roomName);
    }
}
