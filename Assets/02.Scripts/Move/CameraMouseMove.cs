using System.Collections;
using System.Collections.Generic;
using System.Threading;
using UnityEngine;

public class CameraMouseMove : MonoBehaviour
{
    [SerializeField] private float mouseSensitivity = 6; //마우스감도

    private float mouseDirectionY;
    private float mouseDirectionX;

    private float MouseY;
    private float MouseX;

    void Update()
    {
        if(Input.mousePosition.y > 900f)
        {
            mouseDirectionY = 1;
        }
        else if (Input.mousePosition.y < 180)
        {
            mouseDirectionY = -1;
        }
        else
        {
            mouseDirectionY = 0;
        }

        if (Input.mousePosition.x > 1800)
        {
            mouseDirectionX = 1;
        }
        else if (Input.mousePosition.x < 180)
        {
            mouseDirectionX = -1;
        }
        else
        {
            mouseDirectionX = 0;
        }

        CameraMove();
    }
    private void CameraMove()
    {
        MouseX += mouseDirectionX * mouseSensitivity * Time.deltaTime;
        MouseY += mouseDirectionY * mouseSensitivity * Time.deltaTime;

        MouseX = Mathf.Clamp(MouseX, -9.5f, 9.5f); //Clamp를 통해 최소값 최대값을 넘지 않도록함
        MouseY = Mathf.Clamp(MouseY, -2.75f, 1f); //Clamp를 통해 최소값 최대값을 넘지 않도록함
        transform.position = new Vector3(MouseX, MouseY, -1);// 각 축을 한꺼번에 계산
    }
}
