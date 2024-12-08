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

    private static bool isMove = false;

    [SerializeField] private float speed = 0.1f;
    [SerializeField] private Animator animator;
    [SerializeField] private SpriteRenderer spriteRenderer;

    [SerializeField] private GameObject[] factoryScenes;
    [SerializeField] private GameObject[] roomScenes;
    [SerializeField] private GameObject shopUI;
    [SerializeField] private GameObject moveScene;

    private int openShop = 0;

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

    private void Start()
    {
        AudioManager.Instance.PlayAudio("02.b_lobby", true, SoundType.BGM);
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
            //목적지가 팩토리면
            if (cur == (int)room.AFTERNOON && desIndex == (int)room.AFTERNOON)
            {
                for (int i = 0; i < factoryScenes.Length; i++)
                {
                    factoryScenes[i].SetActive(true);
                    moveScene.SetActive(false);
                }
                AudioManager.Instance.StopAudio("02.b_lobby");
                //AudioManager.Instance.PlayAudio("04.b_AMwork", true, SoundType.BGM);
            }
            else if (cur == (int)room.MORNING && desIndex == (int)room.MORNING)
            {
                for (int i = 0; i < roomScenes.Length; i++)
                {
                    roomScenes[i].SetActive(true);
                    moveScene.SetActive(false);
                }
                AudioManager.Instance.StopAudio("02.b_lobby");
                AudioManager.Instance.PlayAudio("01.b_room", true, SoundType.BGM);
            }
            else if (cur == (int)room.SHOP && desIndex == (int)room.SHOP && openShop == 0)
            {
                shopUI.SetActive(true);
                openShop++;
                AudioManager.Instance.StopAudio("02.b_lobby");
                AudioManager.Instance.PlayAudio("03.b_shop", true, SoundType.BGM);
            }
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
