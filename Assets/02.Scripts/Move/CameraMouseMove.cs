using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraMouseMove : MonoBehaviour
{
    public float mouseSensitivity = 10f; //���콺����

    private float MouseY;
    private float MouseX;

    void Update()
    {
        CameraMove();
    }
    private void CameraMove()
    {
        MouseY += Input.GetAxisRaw("Mouse Y") * mouseSensitivity * Time.deltaTime;
        //MouseY = Mathf.Clamp(MouseY, -90f, 90f); //Clamp�� ���� �ּҰ� �ִ밪�� ���� �ʵ�����
        transform.position = new Vector3(0, MouseY, -1);// �� ���� �Ѳ����� ���
    }
}
