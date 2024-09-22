using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterMove : MonoBehaviour
{
    [SerializeField] private static Transform[] waypoints;
    private static Transform _destination;
    int cur = 0;
    private static bool isMove = true;

    [SerializeField] private float speed = 0.1f;

    enum room
    {
        House = 0, 
        Morning= 5, 
        Afternoon  = 10
    }
    void Update()
    {
        if(isMove)
            Move();
    }

    public static void SetMove(string des)
    {
        isMove = true;
        if (des == "House")
            _destination = waypoints[(int)room.House];
        else if (des == "Morning")
            _destination = waypoints[(int)room.Morning];
        else if (des == "Afternoon")
            _destination = waypoints[(int)room.Afternoon];

    }

    public void Move()
    {
        if (transform.position != waypoints[cur+1].position)
        {
            Vector2 p = Vector2.MoveTowards(transform.position, waypoints[cur].position, speed);
            GetComponent<Rigidbody2D>().MovePosition(p);
        }
        else if (cur<waypoints.Length-1)
        {
            cur++;
        }

        if(transform.position == waypoints[waypoints.Length-1].position)
        {
            isMove = false;
        }
    }
}
