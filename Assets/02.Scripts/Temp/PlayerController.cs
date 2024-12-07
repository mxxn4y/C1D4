using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class PlayerController : MonoBehaviour
{
    public float speed = 5.0f;
    private Vector2 targetPosition;
    private bool isMoving = false;
    private Rigidbody2D rigid;
    private Animator anim;

    void Start()
    {
        rigid = GetComponent<Rigidbody2D>();
        anim = GetComponent<Animator>();
    }

    void Update()
    {
        if (isMoving)
        {
            MoveToTarget();
            SetAnimationState();
        }
        else
        {
            ResetToIdleState();
        }
    }

    public void SetTargetPosition(Vector2 position)
    {
        targetPosition = position;
        isMoving = true;
    }

    private void MoveToTarget()
    {
        Vector2 direction = (targetPosition - rigid.position).normalized;
        rigid.velocity = direction * speed;

        //¾ê·Î ÇÏ¸é ´ú´ú °Å¸± ÅÙµ¥
        //float step = speed * Time.deltaTime;
        //transform.position = Vector3.MoveTowards(transform.position, targetPosition, step);

        if (Vector2.Distance(rigid.position, targetPosition) < 0.1f)
        {
            isMoving = false;
            rigid.velocity = Vector2.zero;
        }
    }

    private void SetAnimationState()
    {
        if (targetPosition.x < transform.position.x)
        {
            anim.SetBool("isLeft", true);
            anim.SetBool("isRight", false);
        }
        else
        {
            anim.SetBool("isLeft", false);
            anim.SetBool("isRight", true);
        }

        anim.SetBool("isMoving", true);
    }

    private void ResetToIdleState()
    {
        anim.SetBool("isMoving", false);
        anim.SetBool("isLeft", false);
        anim.SetBool("isRight", false);

        // »óÅÂ¸¦ IDLE·Î À¯Áö
        if (anim.GetBool("isLeft"))
        {
            anim.Play("idle_left");
        }
        else
        {
            anim.Play("idle_right");
        }
    }
}

