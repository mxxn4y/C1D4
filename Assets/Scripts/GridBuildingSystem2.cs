using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.Tilemaps;

public class GridBuildingSystem2 : MonoBehaviour
{
   public static GridBuildingSystem2 current;

    public GridLayout gridLayout;
    public Tilemap MainTilemap;
    public Tilemap TempTilemap;

    private static Dictionary<TileType,TileBase> tileBases = new Dictionary<TileType,TileBase>();

    //건설할 건물 레퍼런스. temp라고 한 이유는 새로운 건물 가져올때마다 변경될것이므로
    private Building temp;
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
        tileBases.Add(TileType.Empty,null);
        tileBases.Add(TileType.White, Resources.Load<TileBase>(tilePath + "W"));
        tileBases.Add(TileType.Green, Resources.Load<TileBase>(tilePath + "G"));
        tileBases.Add(TileType.Red, Resources.Load<TileBase>(tilePath + "R"));
    }

    private void Update()
    {
        //건물이 없다면 종료
        if (!temp)
        {
            return;
        }

        if(isSelected)
        {
            //마우스 포인터의 스크린 좌표 -> 월드 좌표로 변환
            Vector2 touchPos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            //변환된 월드 좌표를 그리드의 셀 좌표로 변환
            Vector3Int cellPos = gridLayout.LocalToCell(touchPos);

            //이전 위치와 현재 위치가 다르면(건물의 현재 위치와 동일한 위치 아니라면)
            if (prevPos != cellPos)
            {
                //건물의 로컬 위치 변경(새로운 셀의 위치로) 영상에서는 그리드 간격 조정해서 x,y값에 5f 더해준듯..? 
                temp.transform.localPosition = gridLayout.CellToLocalInterpolated(cellPos
                    + new Vector3(0f, 0f, 0f));
                prevPos = cellPos; //현재 셀의 위치를 prevPos에 저장
                FollowBuilding();
            }

            //왼쪽 마우스 버튼을 눌렀다면
            if (Input.GetMouseButtonDown(0))
            {
                if (temp.CanBePlaced())
                {
                    temp.Place();
                    isSelected = false;
                }

            }
            else if (Input.GetKeyDown(KeyCode.Escape))
            {
                ClearArea();
                Destroy(temp.gameObject);
                isSelected = false;
            }
        }
        else
        {

        }

    }
    #endregion

    #region Tilemap Management
    //유니티의 GetTilesBlock이 에디터 크래시 발생시키므로 직접 작성한 메소드 3개
    //타일 하나씩 가져와 타일 array 반환
    private static TileBase[] GetTilesBlock(BoundsInt area,Tilemap tilemap)
    {
        TileBase[] array = new TileBase[area.size.x * area.size.y * area.size.z];
        int counter = 0;

        foreach(var v in area.allPositionsWithin)
        {
            Vector3Int pos = new Vector3Int(v.x, v.y, 0);
            array[counter] = tilemap.GetTile(pos);
            counter++;
        }

        return array;
    }

    private static void SetTilesBlock(BoundsInt area,TileType type,Tilemap tilemap)
    {
        int size = area.size.x * area.size.y * area.size.z;
        TileBase[] tileArray = new TileBase[size];
        FillTiles(tileArray, type);
        tilemap.SetTilesBlock(area, tileArray);
    }

    private static void FillTiles(TileBase[] arr,TileType type)
    {
        for (int i = 0; i < arr.Length; i++)
        {
            arr[i] = tileBases[type];
        }
    }

    #endregion

    #region Building Placement
    
    //버튼에 추가해주기(버튼 누르면 건물 생성)
    public void InitializeWithBuilding(GameObject building)
    {
        temp = Instantiate(building,Vector3.zero,Quaternion.identity).GetComponent<Building>();
        FollowBuilding();
    }

    //영역 타일 지우기
    private void ClearArea()
    {
        TileBase[] toClear = new TileBase[prevArea.size.x * prevArea.size.y * prevArea.size.z];
        FillTiles(toClear,TileType.Empty);
        TempTilemap.SetTilesBlock(prevArea, toClear);
    }

    private void FollowBuilding()
    {
        ClearArea();

        isSelected = true;

        temp.area.position = gridLayout.WorldToCell(temp.gameObject.transform.position);
        BoundsInt buildingArea = temp.area;

        TileBase[] baseArray = GetTilesBlock(buildingArea, MainTilemap);

        int size = baseArray.Length;
        TileBase[] tileArray = new TileBase[size];

        for (int i = 0; i < baseArray.Length; i++)
        {
            if (baseArray[i] == tileBases[TileType.White])
            {
                tileArray[i] = tileBases[TileType.Green];
            }
            else
            {
                FillTiles(tileArray,TileType.Red);
                break;
            }
        }

        TempTilemap.SetTilesBlock(buildingArea,tileArray);
        prevArea = buildingArea;
    }
    
    //배치 가능한 영역인지 체크
    public bool CanTakeArea(BoundsInt area)
    {
        //메인 타일맵(배치 가능 영역에 흰 타일 설치해둔)의 특정 영역에 있는 타일들 가져와 배열에 넣음
        TileBase[] baseArray = GetTilesBlock(area, MainTilemap);

        foreach(var b in baseArray)
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
        SetTilesBlock(area,TileType.Empty,TempTilemap);
        SetTilesBlock(area,TileType.Green,MainTilemap);
    }
    
    #endregion
}

public enum TileType2
{
    Empty,
    White,
    Green,
    Red
}