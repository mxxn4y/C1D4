using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterMove : MonoBehaviour
{
    [SerializeField] private Transform[] waypoints;
    int cur = 0;
    private bool isMove = false;

    [SerializeField] GameObject highlight;
    [SerializeField] GameObject player;

    public float speed = 0.1f;
    void Update()
    {
        if(isMove)
            Move();
    }

    private void OnMouseEnter()
    {
        highlight.SetActive(true);
    }
    private void OnMouseExit()
    {
        highlight.SetActive(false);
    }

    private void OnMouseDown()
    {
        isMove = true;
    }

    public void Move()
    {
        if (player.transform.position != waypoints[cur].position)
        {
            Vector2 p = Vector2.MoveTowards(player.transform.position, waypoints[cur].position, speed);
            player.GetComponent<Rigidbody2D>().MovePosition(p);
        }
        else
        {
            cur++;
        }

        if(transform.position != waypoints[waypoints.Length].position)
        {
            isMove = false;
        }
    }
}
