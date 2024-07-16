using System.Collections;
using System.Collections.Generic;
using UnityEngine;
/// <summary>
/// �� ����, ��� �����Ϳ� �ൿ ����
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
        Vector3 roomWorldPos = Camera.main.ScreenToWorldPoint(gameObject.transform.position); //�� ��ǥ ���� ��ǥ�� ��ȯ
        roomWorldPos = new Vector3(roomWorldPos.x, roomWorldPos.y, 0); //z��ǥ�� 0����

        RoomData.area.position = PlacementManagement.Instance._gridLayout.WorldToCell(roomWorldPos); //���� ��ǥ �� ��ǥ�� �ٲ㼭 area�� �Ҵ�
    }
    private void Update()
    {
        GetComponent<RoomUI>().SetMovingUI();
        if (_moving)
        {
            UpdateArea();
        }
    }

    //��Ÿ�ӿ� ������ ī�� ������ ����
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
        Vector3 roomWorldPos = Camera.main.ScreenToWorldPoint(gameObject.transform.position); //�� ��ǥ ���� ��ǥ�� ��ȯ
        roomWorldPos = new Vector3(roomWorldPos.x, roomWorldPos.y, 0); //z��ǥ�� 0����

        RoomData.area.position = PlacementManagement.Instance._gridLayout.WorldToCell(roomWorldPos); //���� ��ǥ �� ��ǥ�� �ٲ㼭 area�� �Ҵ�
    }


    #endregion
}
