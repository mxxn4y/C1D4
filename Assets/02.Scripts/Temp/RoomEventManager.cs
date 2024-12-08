using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using System;
using UnityEngine.UI;

public class RoomEventManager : MonoBehaviour
{
    [SerializeField] GameObject PlayerRoomScene;
    [SerializeField] GameObject CollectBookScene;
    [SerializeField] GameObject BedScene;
    [SerializeField] GameObject DoorScene;

    void Start()
    {
        AudioManager.Instance.PlayAudio("01.b_room", true, SoundType.BGM, 0);
    }
    void Update()
    {

        if (Input.GetMouseButtonDown(0))
        {
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            RaycastHit2D hit = Physics2D.Raycast(ray.origin, ray.direction);

            //Vector2 pos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            //RaycastHit2D hit = Physics2D.Raycast(pos, Vector2.zero);
         
            if (hit.collider != null)
            {
                if (hit.collider.CompareTag("Bed"))
                {
                    PlayerRoomScene.SetActive(false);
                    BedScene.SetActive(true);
                    AudioManager.Instance.StopAudio("01.b_room");
                    Debug.Log("베드씬 로드");
                }
                else if (hit.collider.CompareTag("CollectBook"))
                {
                    CollectBookScene.SetActive(true);
                    PlayerRoomScene.SetActive(false);
                   
                    Debug.Log("콜렉트북씬 로드");
                }
                else if (hit.collider.CompareTag("Door"))
                {
                    DoorScene.SetActive(true);
                    PlayerRoomScene.SetActive(false);
                    AudioManager.Instance.StopAudio("01.b_room");
                    Debug.Log("방 밖으로 나가는 씬 로드");
                }
            }
            else
            {
                Debug.Log("클릭 오브젝트 없음");
            }
        }
  
            
    }


    

}
