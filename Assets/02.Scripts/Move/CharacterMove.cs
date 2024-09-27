using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterMove : MonoBehaviour
{
    [SerializeField] private Transform[] waypoints;
    private Transform destination;
    int cur = 0;
    private bool isMove = false;

    [SerializeField] private float speed = 0.1f;

    enum room
    {
        HOUSE = 0, 
        MORNING = 1, 
        AFTERNOON = 6
    }

    public void SetDestination(string _destination)
    {
        //움직임 상태로 설정
        isMove = true;

        //목적지 설정
        if (_destination == "House")
            destination = waypoints[(int)room.HOUSE];
        else if (_destination == "Morning")
            destination = waypoints[(int)room.MORNING];
        else if (_destination == "Afternoon")
            destination = waypoints[(int)room.AFTERNOON];
    }

    public void Move()
    {

        if (transform.position != waypoints[cur].position)
        {
            Vector2 p = Vector2.MoveTowards(transform.position, waypoints[cur].position, speed);
            GetComponent<Rigidbody2D>().MovePosition(p);
        }
        //목적지와 현재 위치가 같으면 이동 멈춤
        else if (transform.position == destination.position)
        {
            isMove = false;
        }
        else
        {
            cur++;
        }
    }

    void Update()
    {
        if (isMove)
            Move();
    }
}
