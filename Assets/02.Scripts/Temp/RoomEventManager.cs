using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.EventSystems;
using System;

public class RoomEventManager : MonoBehaviour
{
    public float speed = 5.0f;
    private Vector3 targetPosition;
    private bool isMoving;

    void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            Vector2 mousePosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            RaycastHit2D hit = Physics2D.Raycast(mousePosition, Vector2.zero);
            Debug.Log("클릭됨");
            if (hit.collider != null)
            {
                Debug.Log("히트된 객체: " + hit.collider.name);
                if (hit.collider.CompareTag("Floor"))
                {
                    targetPosition = new Vector3(hit.point.x, hit.point.y, transform.position.z);
                    isMoving = true;
                    Debug.Log("플레이어 움직임: " + isMoving);
                }
                else
                {
                    isMoving = false;
                }
            }
            else
            {
                Debug.Log("히트되지 않음");
            }
        }

        if (isMoving)
        {
            MoveToTarget();
        }
    }

    void MoveToTarget()
    {
        float step = speed * Time.deltaTime;
        transform.position = Vector3.MoveTowards(transform.position, targetPosition, step);

        if (Vector3.Distance(transform.position, targetPosition) < 0.1f)
        {
            isMoving = false;
        }
    }
}
