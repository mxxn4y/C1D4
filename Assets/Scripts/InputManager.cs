using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InputManager : MonoBehaviour
{
    [SerializeField]
    private Camera sceneCamera;

    private Vector3 lastPosition;

    [SerializeField]
    private LayerMask placementLayermask;

    public Vector3 GetSelectedMapPosition()
    {
        Vector3 mousePos = Input.mousePosition; //���콺 ������ ������
        // Camera�� ScreenToWorldPoint �޼ҵ带 ����Ͽ� ��ũ�� �������� ���� ���������� ��ȯ
        // �� ��, z���� ī�޶���� �Ÿ��� �ǹ�.
        // 2D������ �Ϲ������� ī�޶�� ������Ʈ ������ �Ÿ��� ��Ȯ�� �˱� ��Ʊ� ������,
        // 10.0f�� ���� ������ �Ÿ� ���� ������ �ִ� ���� ����.
        //mousePos.z = sceneCamera.nearClipPlane;
        //-> 3D�϶��� �̷������� ī�޶� ��ó���� �����ϴ� ray ���� ����
        //2D�� �Ϲ������� z�� ������ �����Ƿ� ���������� �����ϴ°��� �Ϲ���
        mousePos.z = 10.0f;
        //Ray ray = sceneCamera.ScreenPointToRay(mousePos);
        Vector2 mouseWorldPos = sceneCamera.ScreenToWorldPoint(mousePos);
        //RaycastHit hit;
        RaycastHit2D hit = Physics2D.Raycast(mouseWorldPos, Vector2.zero, Mathf.Infinity, placementLayermask);
        if (hit.collider != null)
        {
            // �浹 ��ġ�� lastPosition�� ����
            lastPosition = hit.point;
            Debug.Log(hit.point);
        }
        return lastPosition;
    }
}
