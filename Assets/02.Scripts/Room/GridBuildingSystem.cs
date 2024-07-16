using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.Tilemaps;


public class GridBuildingSystem : TilemapManagement
{
    public static GridBuildingSystem current;

    public GridLayout gridLayout;
    public Tilemap MainTilemap;
    public Tilemap TempTilemap;



    //건설할 건물 레퍼런스. temp라고 한 이유는 새로운 건물 가져올때마다 변경될것이므로
    private Building room;
    private Building prevRoom;
    private Vector3 prevPos; //건물 이전 위치 저장할 Vector3
    private BoundsInt prevArea;

    //건물 선택중(이동중인지) 확인하는 변수
    private bool isSelected = false;


    #region Unity Methods

    private void Awake()
    {
        current = this;
    }

    private void Start()
    {
        string tilePath = @"Tile\";
        tileBases.Add(TileType.Empty, null);
        tileBases.Add(TileType.White, Resources.Load<TileBase>(tilePath + "W"));
        tileBases.Add(TileType.Green, Resources.Load<TileBase>(tilePath + "G"));
        tileBases.Add(TileType.Red, Resources.Load<TileBase>(tilePath + "R"));
    }


    private void Update()
    {
        //건물이 선택되었다면
        if (isSelected)
        {
            MoveRoom();

            //왼쪽 마우스 버튼을 눌렀다면
            if (Input.GetMouseButtonDown(0))
            {
                PlaceRoom();

            }
            //esc 또는 우클릭하면 건설 취소
            else if (Input.GetKeyDown(KeyCode.Escape) | Input.GetMouseButtonDown(1))
            {
                CancelMoveRoom();
            }
        }
        else
        {
            // 마우스 왼쪽 버튼이 클릭되었는지 확인
            if (Input.GetMouseButtonDown(0)) 
            {

                SelectRoom();
            }
        }

    }
    #endregion


    #region Building Placement

    //버튼에 추가해주기(버튼 누르면 건물 생성)
    public void InitializeWithBuilding(GameObject building)
    {
        if (!isSelected)
        {
            room = Instantiate(building, Vector3.zero, Quaternion.identity).GetComponent<Building>();
            isSelected = true;
        }
        
    }

    //영역 타일 지우기

    private void ClearArea(BoundsInt area,Tilemap tilemap)
    {
        if(tilemap == TempTilemap)
        {
            SetTilesBlock(area, TileType.Empty, tilemap);
        }
        else
        {
            SetTilesBlock(area, TileType.White, tilemap);
        }
        
    }


    private void FollowBuilding()
    {
        ClearArea(prevArea,TempTilemap);

        room.area.position = gridLayout.WorldToCell(room.gameObject.transform.position);
        BoundsInt roomArea = room.area;

        if (CanTakeArea(roomArea))
        {
            SetTilesBlock(roomArea, TileType.Green, TempTilemap);
        }
        else
        {
            SetTilesBlock(roomArea, TileType.Red, TempTilemap);
        }
        prevArea = roomArea;
    }

    //배치 가능한 영역인지 체크
    public bool CanTakeArea(BoundsInt area)
    {
        //메인 타일맵(배치 가능 영역에 흰 타일 설치해둔)의 특정 영역에 있는 타일들 가져와 배열에 넣음
        TileBase[] baseArray = GetTilesBlock(area, MainTilemap);

        foreach (var b in baseArray)
        {
            //하나라도 설치 가능한 영역(흰 타일)이 아니라면
            if (b != tileBases[TileType.White])
            {
                Debug.Log("배치 불가능한 영역");
                return false;
            }
        }

        return true;
    }

    public void TakeArea(BoundsInt area)
    {
        SetTilesBlock(area, TileType.Empty, TempTilemap);
        SetTilesBlock(area, TileType.Empty, MainTilemap);
    }

    private void MoveRoom()
    {

        Vector2 touchPos = Camera.main.ScreenToWorldPoint(Input.mousePosition); //마우스 포인터의 스크린 좌표 -> 월드 좌표로 변환
        Vector3Int cellPos = gridLayout.LocalToCell(touchPos); //변환된 월드 좌표를 그리드의 셀 좌표로 변환

        //이전 위치와 현재 위치가 다르면(건물의 현재 위치와 동일한 위치 아니라면)
        if (prevPos != cellPos)
        {
            //건물의 로컬 위치 변경(새로운 셀의 위치로) 영상에서는 그리드 간격 조정해서 x,y값에 5f 더해준듯..? 
            room.transform.localPosition = gridLayout.CellToLocalInterpolated(cellPos
                + new Vector3(0f, 0f, 0f));
            prevPos = cellPos; //현재 셀의 위치를 prevPos에 저장
            FollowBuilding();
            room.MovingRoom();
        }
    }

    private void SelectRoom()
    {
        // 카메라로부터 마우스 포지션까지의 광선을 생성
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        RaycastHit2D hit = Physics2D.Raycast(ray.origin, ray.direction, Mathf.Infinity);
        Debug.Log(hit.collider);
        // 광선이 어떤 오브젝트와 충돌했는지 확인
        if (hit.collider != null)
        {
            // 충돌한 오브젝트의 clone 생성
            room = Instantiate(hit.collider.gameObject, hit.collider.gameObject.transform.position, Quaternion.identity).GetComponent<Building>();
            prevRoom = hit.collider.gameObject.GetComponent<Building>();
            prevRoom.gameObject.SetActive(false);

            isSelected = true;
        }
    }

    private void PlaceRoom()
    {
        if (CanTakeArea(room.area))
        {
            if (prevRoom != null)
            {
                ClearArea(prevRoom.area, MainTilemap);
                Destroy(prevRoom.gameObject);
                prevRoom = null;
            }
            room.Place();
            TakeArea(prevArea);
            isSelected = false;
        }
    }

    private void CancelMoveRoom()
    {
        ClearArea(prevArea, TempTilemap);
        Destroy(room.gameObject);
        if (prevRoom != null)
        {
            prevRoom.gameObject.SetActive(true);
            prevRoom = null;
        }
        isSelected = false;
    }


    #endregion
}

