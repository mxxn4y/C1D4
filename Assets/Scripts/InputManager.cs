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
        Vector3 mousePos = Input.mousePosition; //마우스 포지션 가져옴
        // Camera의 ScreenToWorldPoint 메소드를 사용하여 스크린 포지션을 월드 포지션으로 변환
        // 이 때, z값은 카메라와의 거리를 의미.
        // 2D에서는 일반적으로 카메라와 오브젝트 사이의 거리를 정확히 알기 어렵기 때문에,
        // 10.0f와 같은 적절한 거리 값을 설정해 주는 것이 좋음.
        //mousePos.z = sceneCamera.nearClipPlane;
        //-> 3D일때는 이런식으로 카메라 근처에서 시작하는 ray 생성 가능
        //2D는 일반적으로 z축 사용되지 않으므로 고정값으로 설정하는것이 일반적
        mousePos.z = 10.0f;
        //Ray ray = sceneCamera.ScreenPointToRay(mousePos);
        Vector2 mouseWorldPos = sceneCamera.ScreenToWorldPoint(mousePos);
        //RaycastHit hit;
        RaycastHit2D hit = Physics2D.Raycast(mouseWorldPos, Vector2.zero, Mathf.Infinity, placementLayermask);
        if (hit.collider != null)
        {
            // 충돌 위치를 lastPosition에 저장
            lastPosition = hit.point;
            Debug.Log(hit.point);
        }
        return lastPosition;
    }
}
