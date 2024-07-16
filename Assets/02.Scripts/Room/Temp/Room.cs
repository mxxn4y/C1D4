using System.Collections;
using System.Collections.Generic;
using UnityEngine;
/// <summary>
/// 방 정의, 모든 데이터와 행동 연결
/// </summary>
[RequireComponent(typeof(RoomUI))]
[RequireComponent(typeof(RoomMovement))]
public class Room : MonoBehaviour
{
    #region Fields and Properties

    [field: SerializeField] public ScriptableRoom RoomData { get; private set; }


    [field: SerializeField] public bool _placed { get; private set; } = false;
    public bool _moving { get; private set; } = false;


    #endregion

    #region Methods
    private void Start()
    {
        Vector3 roomWorldPos = Camera.main.ScreenToWorldPoint(gameObject.transform.position); //방 좌표 월드 좌표로 변환
        roomWorldPos = new Vector3(roomWorldPos.x, roomWorldPos.y, 0); //z좌표는 0으로

        RoomData.area.position = PlacementManagement.Instance._gridLayout.WorldToCell(roomWorldPos); //월드 좌표 셀 좌표로 바꿔서 area에 할당
    }
    private void Update()
    {
        GetComponent<RoomUI>().SetMovingUI();
        if (_moving)
        {
            UpdateArea();
        }
    }

    //런타임에 연관된 카드 데이터 설정
    public void SetUp(ScriptableRoom data)
    {
        RoomData = data;
        GetComponent<RoomUI>().SetRoomUI();
    }


    public void SetPlace()
    {
        _placed = true;
        _moving = false;
    }
    public void MovingRoom(bool b)
    {
        _moving = b;
    }

    public void UpdateArea()
    {
        Vector3 roomWorldPos = Camera.main.ScreenToWorldPoint(gameObject.transform.position); //방 좌표 월드 좌표로 변환
        roomWorldPos = new Vector3(roomWorldPos.x, roomWorldPos.y, 0); //z좌표는 0으로

        RoomData.area.position = PlacementManagement.Instance._gridLayout.WorldToCell(roomWorldPos); //월드 좌표 셀 좌표로 바꿔서 area에 할당
    }


    #endregion
}
