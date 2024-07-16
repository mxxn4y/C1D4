using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
/// <summary>
/// 방 드래그 앤 드롭
/// </summary>
public class RoomMovement : MonoBehaviour, IDragHandler, IEndDragHandler, IBeginDragHandler
{
    #region Fields and Properties

    private Canvas _cardCanvas;
    private RectTransform _rectTransform;
    private Room _room;

    private Vector3 _prevPos;
    private BoundsInt _prevArea;

    private PlacementManagement _placeM = PlacementManagement.Instance;


    private readonly string CANVAS_TAG = "RoomCanvas";

    #endregion

    #region Methods

    private void Start()
    {
        _cardCanvas = GameObject.FindGameObjectWithTag(CANVAS_TAG).GetComponent<Canvas>();
        _rectTransform = GetComponent<RectTransform>();
        _room = GetComponent<Room>();
    }

    #endregion
    public void OnBeginDrag(PointerEventData eventData)
    {
        _prevPos = _rectTransform.position;
        _room.UpdateArea();
        _prevArea = _room.RoomData.area;
        _room.MovingRoom(true);
    }

    public void OnDrag(PointerEventData eventData)
    {
        _placeM.FollowRoom(_room);
        MoveRoom();
    }

    public void OnEndDrag(PointerEventData eventData)
    {      
        if (_placeM.CanTakeArea(_room.RoomData.area))
        {
            if (_room._placed)
            {
                _placeM.ClearArea(_prevArea, _placeM._mainTilemap);
            }
            _placeM.PlaceRoom(_room);

        }
        else
        {
            _rectTransform.position = _prevPos;
           
        }
        _room.MovingRoom(false);
        _placeM._tempTilemap.ClearAllTiles();
    }

    private void MoveRoom()
    {
        Vector2 touchPos = Camera.main.ScreenToWorldPoint(Input.mousePosition); //마우스 포인터의 스크린 좌표 -> 월드 좌표로 변환
        Vector3Int cellPos = _placeM._gridLayout.LocalToCell(touchPos); //변환된 월드 좌표를 그리드의 셀 좌표로 변환
        _rectTransform.position = Camera.main.WorldToScreenPoint(_placeM._gridLayout.CellToWorld(cellPos));
    }


}
