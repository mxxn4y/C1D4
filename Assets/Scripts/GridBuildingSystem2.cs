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

    //�Ǽ��� �ǹ� ���۷���. temp��� �� ������ ���ο� �ǹ� �����ö����� ����ɰ��̹Ƿ�
    private Building temp;
    private Vector3 prevPos; //�ǹ� ���� ��ġ ������ Vector3
    private BoundsInt prevArea;

    //�ǹ� ������(�̵�������) Ȯ���ϴ� ����
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
        //�ǹ��� ���ٸ� ����
        if (!temp)
        {
            return;
        }

        if(isSelected)
        {
            //���콺 �������� ��ũ�� ��ǥ -> ���� ��ǥ�� ��ȯ
            Vector2 touchPos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            //��ȯ�� ���� ��ǥ�� �׸����� �� ��ǥ�� ��ȯ
            Vector3Int cellPos = gridLayout.LocalToCell(touchPos);

            //���� ��ġ�� ���� ��ġ�� �ٸ���(�ǹ��� ���� ��ġ�� ������ ��ġ �ƴ϶��)
            if (prevPos != cellPos)
            {
                //�ǹ��� ���� ��ġ ����(���ο� ���� ��ġ��) ���󿡼��� �׸��� ���� �����ؼ� x,y���� 5f �����ص�..? 
                temp.transform.localPosition = gridLayout.CellToLocalInterpolated(cellPos
                    + new Vector3(0f, 0f, 0f));
                prevPos = cellPos; //���� ���� ��ġ�� prevPos�� ����
                FollowBuilding();
            }

            //���� ���콺 ��ư�� �����ٸ�
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
    //����Ƽ�� GetTilesBlock�� ������ ũ���� �߻���Ű�Ƿ� ���� �ۼ��� �޼ҵ� 3��
    //Ÿ�� �ϳ��� ������ Ÿ�� array ��ȯ
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
    
    //��ư�� �߰����ֱ�(��ư ������ �ǹ� ����)
    public void InitializeWithBuilding(GameObject building)
    {
        temp = Instantiate(building,Vector3.zero,Quaternion.identity).GetComponent<Building>();
        FollowBuilding();
    }

    //���� Ÿ�� �����
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
    
    //��ġ ������ �������� üũ
    public bool CanTakeArea(BoundsInt area)
    {
        //���� Ÿ�ϸ�(��ġ ���� ������ �� Ÿ�� ��ġ�ص�)�� Ư�� ������ �ִ� Ÿ�ϵ� ������ �迭�� ����
        TileBase[] baseArray = GetTilesBlock(area, MainTilemap);

        foreach(var b in baseArray)
        {
            //�ϳ��� ��ġ ������ ����(�� Ÿ��)�� �ƴ϶��
            if (b != tileBases[TileType.White])
            {
                Debug.Log("��ġ �Ұ����� ����");
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