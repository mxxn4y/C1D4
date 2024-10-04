using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraMouseMove : MonoBehaviour
{
    public float mouseSensitivity = 10f; //마우스감도

    private float MouseY;
    private float MouseX;

    void Update()
    {
        CameraMove();
    }
    private void CameraMove()
    {
        MouseY += Input.GetAxisRaw("Mouse Y") * mouseSensitivity * Time.deltaTime;
        //MouseY = Mathf.Clamp(MouseY, -90f, 90f); //Clamp를 통해 최소값 최대값을 넘지 않도록함
        transform.position = new Vector3(0, MouseY, -1);// 각 축을 한꺼번에 계산
    }
}
