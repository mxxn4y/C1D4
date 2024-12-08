using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.EventSystems;
using System;

public class RoomEventManager : MonoBehaviour
{


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
                    //SceneManager.LoadScene("");
                    Debug.Log("베드씬 로드");
                }
                else if (hit.collider.CompareTag("CollectBook"))
                {
                    //SceneManager.LoadScene("CollectBookScene");
                    Debug.Log("콜렉트북씬 로드");
                }
                else if (hit.collider.CompareTag("Door"))
                {
                    //SceneManager.LoadScene("");
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
