using System.Collections;
using System.Collections.Generic;
using System.Runtime.ConstrainedExecution;
using UnityEngine;

public class Product : MonoBehaviour
{
    //움직임 속도
    [SerializeField] private float speed = 0.1f;

    //웨이포인트
    public Transform movePoint;
    public List<Transform> movePoints = new List<Transform>();

    //물품 멈추는 시간
    private float stopTime = 2f;

    //멈추는 위치(웨이 포인트 인덱스 번호)
    private int[] stopPoints = { 20, 24, 28 };

    //움직임 관리
    public bool isMove { get; private set; } = true;
    private int cur = 0;

    [SerializeField] private Color orange;

    //스프라이트 색상
    private SpriteRenderer sprite;

    //물품 색상
    public FactoryGameManager.FactoryColor currentProductColor { get; private set; }

    private void Start()
    {
        //처음 색 블랙
        currentProductColor = FactoryGameManager.FactoryColor.WHITE;

        //스프라이트
        sprite = GetComponent<SpriteRenderer>();

        movePoint = GameObject.FindWithTag("WayPoints").transform;
        for (int i = 0; i < movePoint.childCount; i++) 
        {
            movePoints.Add(movePoint.GetChild(i));
        }
    }

    void Update()
    {
        //움직임 상태면 이동
        if (isMove)
            Move();
    }

    private void Move()
    {
        //현재 위치가 다음 웨이포인트가 아닐 때 이동
        if (transform.position != movePoints[cur].position)
        {
            for(int i = 0; i < stopPoints.Length; i++)
            {
                //지금 멈춘게 n번째고, n번째 앞에 캐릭터가 있으면
                if(cur-1 == stopPoints[i] && FactoryGameManager.Instance.isPlaces[i] == true)
                {
                    //멈춤
                    StartCoroutine(StopProduct());

                    //그리고 한 번 멈췄으면 반복문 빠져나감
                    break;
                }    
            }

            Vector2 p = Vector2.MoveTowards(transform.position, movePoints[cur].position, speed);
            GetComponent<Rigidbody2D>().MovePosition(p);
        }
        //마지막에 도착했을 경우 멈춤
        else if (transform.position == movePoints[movePoints.Count - 1].position)
        {
            //움직임 멈춤
            isMove = false;
        }
        else
        {
            cur++;
        }
    }

    private IEnumerator StopProduct()
    {
        //멈춤 상태
        isMove = false;

        //2초 대기
        yield return new WaitForSeconds(stopTime);

        //다시 움직임
        isMove = true;

        //위치 이동
        cur++;
    }
    public void SetColor(FactoryGameManager.FactoryColor _machineColor)
    {
        //누른 기계가 빨간색일 경우
        if (_machineColor == FactoryGameManager.FactoryColor.RED)
        {
            //처음 누를 경우
            if(currentProductColor == FactoryGameManager.FactoryColor.WHITE)
            {
                currentProductColor = FactoryGameManager.FactoryColor.RED;
                sprite.color = Color.red;
            }
            //두번째 누를 경우
            else if(currentProductColor == FactoryGameManager.FactoryColor.YELLOW)
            {
                currentProductColor = FactoryGameManager.FactoryColor.GREEN;
                sprite.color = Color.green;
            }
            else if (currentProductColor == FactoryGameManager.FactoryColor.BLUE)
            {
                currentProductColor = FactoryGameManager.FactoryColor.PURPLE;
                sprite.color = Color.magenta;
            }
            //세번째 누를 경우
            else if (currentProductColor == FactoryGameManager.FactoryColor.GREEN)
            {
                currentProductColor = FactoryGameManager.FactoryColor.BLACK;
                sprite.color = Color.black;
            }
        }
        //누른 기계가 노란색 경우
        else if (_machineColor == FactoryGameManager.FactoryColor.YELLOW)
        {
            //처음 누를 경우
            if (currentProductColor == FactoryGameManager.FactoryColor.WHITE)
            {
                currentProductColor = FactoryGameManager.FactoryColor.YELLOW;
                sprite.color = Color.yellow;
            }
            //두번째 누를 경우
            else if (currentProductColor == FactoryGameManager.FactoryColor.RED)
            {
                currentProductColor = FactoryGameManager.FactoryColor.ORANGE;
                sprite.color = orange;
            }
            else if (currentProductColor == FactoryGameManager.FactoryColor.BLUE)
            {
                currentProductColor = FactoryGameManager.FactoryColor.GREEN;
                sprite.color = Color.green;
            }
            //세번째 누를 경우
            else if (currentProductColor == FactoryGameManager.FactoryColor.PURPLE)
            {
                currentProductColor = FactoryGameManager.FactoryColor.BLACK;
                sprite.color = Color.black;
            }
        }
        //누른 기계가 파란색 경우
        else if (_machineColor == FactoryGameManager.FactoryColor.BLUE)
        {
            //처음 누를 경우
            if (currentProductColor == FactoryGameManager.FactoryColor.WHITE)
            {
                currentProductColor = FactoryGameManager.FactoryColor.BLUE;
                sprite.color = Color.blue;
            }
            //두번째 누를 경우
            else if (currentProductColor == FactoryGameManager.FactoryColor.RED)
            {
                currentProductColor = FactoryGameManager.FactoryColor.PURPLE;
                sprite.color = Color.magenta;
            }
            else if (currentProductColor == FactoryGameManager.FactoryColor.YELLOW)
            {
                currentProductColor = FactoryGameManager.FactoryColor.GREEN;
                sprite.color = Color.green;
            }
            //세번째 누를 경우
            else if (currentProductColor == FactoryGameManager.FactoryColor.ORANGE)
            {
                currentProductColor = FactoryGameManager.FactoryColor.BLACK;
                sprite.color = Color.black;
            }
        }

        //코루틴 멈춤
        StopCoroutine(StopProduct());
        isMove = true;
        cur++;
    }
}
