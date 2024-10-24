using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraMouseMove : MonoBehaviour
{
    [SerializeField] private float mouseSensitivity; //���콺����
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
        MouseY = Mathf.Clamp(MouseY, 0f, 15f); //Clamp�� ���� �ּҰ� �ִ밪�� ���� �ʵ�����
        transform.position = new Vector3(0, MouseY, -1);// �� ���� �Ѳ����� ���
    }
}
