using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public float speed = 5.0f;
    private Vector2 targetPosition;
    private bool isMoving = false;
    private Rigidbody2D rigid;
    void Start()
    {
        rigid = GetComponent<Rigidbody2D>();
    }

    void Update()
    {
        if (isMoving)
        {
            MoveToTarget();
        }
    }

    public void SetTargetPosition(Vector2 position)
    {
        targetPosition = position;
        isMoving = true;
        if (position.x < transform.position.x)
        {
            transform.localScale = new Vector3(-1, 1, 1);
        }
        else
        {
            transform.localScale = new Vector3(1, 1, 1);
        }
    }

    private void MoveToTarget()
    {
        //娟肺 窍搁 代代 芭副 刨单
        //float step = speed * Time.deltaTime;
        //transform.position = Vector3.MoveTowards(transform.position, targetPosition, step);
        Vector2 direction = (targetPosition - rigid.position).normalized;
        rigid.velocity = direction * speed;

        if (Vector2.Distance(rigid.position, targetPosition) < 0.1f)
        {
            isMoving = false;
            rigid.velocity = Vector2.zero;
        }
    }
}
