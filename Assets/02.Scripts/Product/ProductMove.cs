using System.Collections;
using System.Collections.Generic;
using System.Runtime.ConstrainedExecution;
using UnityEngine;

public class ProductMove : MonoBehaviour
{
    //움직임 속도
    [SerializeField] private float speed = 0.1f;

    //웨이포인트
    public Transform movePoint;
    public List<Transform> movePoints = new List<Transform>();

    //물품 멈추는 시간
    private float stopTime = 2f;

    //멈추는 위치(웨이 포인트 인덱스 번호)
    private const int STOP_POINT_RED = 20;
    private const int STOP_POINT_YELLOW = 24;
    private const int STOP_POINT_BLUE = 28;

    //움직임 관리
    public bool isMove { get; private set; } = true;
    private int cur = 0;

    private void Start()
    {
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
            //기계 앞일 때 멈춤
            if (cur == STOP_POINT_RED || cur == STOP_POINT_YELLOW || cur == STOP_POINT_BLUE)
                StartCoroutine(StopProduct());

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

    public void SetMove()
    {
        isMove = true;
        cur++;
    }
}
