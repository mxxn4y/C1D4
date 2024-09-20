using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SelectRoom : MonoBehaviour
{
    //하이라이트 오브젝트(자식 오브젝트에 둠)
    [SerializeField] GameObject highlight;
    //현재 위치 아이디
    [SerializeField] string roomName;

    private void OnMouseEnter()
    {
        //콜라이더 내로 들어올 때 TRUE
        highlight.SetActive(true);
    }
    private void OnMouseExit()
    {
        //콜라이더 밖으로 나갈 때 FALSE
        highlight.SetActive(false);
    }

    //클릭했을 때 위치 보냄
    private void OnMouseDown()
    {
        //String 매개변수로 쓰기
        Debug.Log("클릭");
        CharacterMove.SetMove(roomName);
    }
}
