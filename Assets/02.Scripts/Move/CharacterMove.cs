using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class CharacterMove : MonoBehaviour
{
    [SerializeField] private Transform[] waypoints;
    private int desIndex;
    private int startIndex;
    int cur = 0;
    private static bool isMove = true;

    [SerializeField] private float speed = 0.1f;
    [SerializeField] private Animator animator;
    [SerializeField] private SpriteRenderer spriteRenderer;

<<<<<<< HEAD
=======
    [SerializeField] private GameObject[] factoryScenes;
    [SerializeField] private GameObject moveScene;

>>>>>>> parent of 3c249bf4 (사운드 및 씬 연결)
    enum room
    {
        MORNING = 0, 
        SHOP = 3, 
        AFTERNOON = 6
    }

    enum prevWork
    {
        HOUSE,
        MORNING,
        AFTERNOON
    }

    //출발지 설정
    public void SetStart()
    {
        for (int i = 0; i < waypoints.Length; i++) 
        {
            if (transform.position == waypoints[i].position)
            {
                startIndex = i;
                break;
            }
        }
    }

    //목적지 설정
    public void SetDestination(string _destination)
    {
        //출발지 설정
        SetStart();

        //목적지 설정
        if (_destination == "Shop")
            desIndex = (int)room.SHOP;
        else if (_destination == "Morning")
            desIndex = (int)room.MORNING;
        else if (_destination == "Afternoon")
            desIndex = (int)room.AFTERNOON;

        //움직임 상태로 설정
        isMove = true;
        animator.SetBool("IsMove", isMove);

    }

    public void Move()
    {
        if (transform.position != waypoints[cur].position)
        {
            Vector2 p = Vector2.MoveTowards(transform.position, waypoints[cur].position, speed);
            GetComponent<Rigidbody2D>().MovePosition(p);
        }
        //목적지와 현재 위치가 같으면 이동 멈춤
        else if (transform.position == waypoints[desIndex].position)
        {
            isMove = false;
            animator.SetBool("IsMove", isMove);
<<<<<<< HEAD
=======

            //목적지가 팩토리면
            if(cur == (int)room.AFTERNOON)
            {
                for(int i = 0; i < factoryScenes.Length; i++)
                {
                    factoryScenes[i].SetActive(true);
                    moveScene.SetActive(false);
                }
            }
>>>>>>> parent of 3c249bf4 (사운드 및 씬 연결)
        }
        else
        {
            if (desIndex > startIndex)
            {
                cur++;
                spriteRenderer.flipX = false;
            }
            else
            {
                cur--;
                spriteRenderer.flipX = true;
            } 
        }
    }

    void Update()
    {
        if (isMove)
            Move();
    }
}
