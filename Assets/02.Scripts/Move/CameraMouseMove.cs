using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraMouseMove : MonoBehaviour
{
    [SerializeField] private float mouseSensitivity; //마우스감도
    private float mouseDirection;

    private float MouseY;
    private float MouseX;

    void Update()
    {
        if(Input.mousePosition.y > 900f)
        {
            mouseDirection = 1;
            CameraMove();
        }
        else if (Input.mousePosition.y < 180)
        {
            mouseDirection = -1;
            CameraMove();
        }
    }
    private void CameraMove()
    {
        MouseY += mouseDirection * mouseSensitivity * Time.deltaTime;
        MouseY = Mathf.Clamp(MouseY, 0f, 15f); //Clamp를 통해 최소값 최대값을 넘지 않도록함
        transform.position = new Vector3(0, MouseY, -1);// 각 축을 한꺼번에 계산
    }
}
