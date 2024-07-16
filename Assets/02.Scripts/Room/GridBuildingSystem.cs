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



    //�Ǽ��� �ǹ� ���۷���. temp��� �� ������ ���ο� �ǹ� �����ö����� ����ɰ��̹Ƿ�
    private Building room;
    private Building prevRoom;
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
        tileBases.Add(TileType.Empty, null);
        tileBases.Add(TileType.White, Resources.Load<TileBase>(tilePath + "W"));
        tileBases.Add(TileType.Green, Resources.Load<TileBase>(tilePath + "G"));
        tileBases.Add(TileType.Red, Resources.Load<TileBase>(tilePath + "R"));
    }


    private void Update()
    {
        //�ǹ��� ���õǾ��ٸ�
        if (isSelected)
        {
            MoveRoom();

            //���� ���콺 ��ư�� �����ٸ�
            if (Input.GetMouseButtonDown(0))
            {
                PlaceRoom();

            }
            //esc �Ǵ� ��Ŭ���ϸ� �Ǽ� ���
            else if (Input.GetKeyDown(KeyCode.Escape) | Input.GetMouseButtonDown(1))
            {
                CancelMoveRoom();
            }
        }
        else
        {
            // ���콺 ���� ��ư�� Ŭ���Ǿ����� Ȯ��
            if (Input.GetMouseButtonDown(0)) 
            {

                SelectRoom();
            }
        }

    }
    #endregion


    #region Building Placement

    //��ư�� �߰����ֱ�(��ư ������ �ǹ� ����)
    public void InitializeWithBuilding(GameObject building)
    {
        if (!isSelected)
        {
            room = Instantiate(building, Vector3.zero, Quaternion.identity).GetComponent<Building>();
            isSelected = true;
        }
        
    }

    //���� Ÿ�� �����

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

    //��ġ ������ �������� üũ
    public bool CanTakeArea(BoundsInt area)
    {
        //���� Ÿ�ϸ�(��ġ ���� ������ �� Ÿ�� ��ġ�ص�)�� Ư�� ������ �ִ� Ÿ�ϵ� ������ �迭�� ����
        TileBase[] baseArray = GetTilesBlock(area, MainTilemap);

        foreach (var b in baseArray)
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
        SetTilesBlock(area, TileType.Empty, TempTilemap);
        SetTilesBlock(area, TileType.Empty, MainTilemap);
    }

    private void MoveRoom()
    {

        Vector2 touchPos = Camera.main.ScreenToWorldPoint(Input.mousePosition); //���콺 �������� ��ũ�� ��ǥ -> ���� ��ǥ�� ��ȯ
        Vector3Int cellPos = gridLayout.LocalToCell(touchPos); //��ȯ�� ���� ��ǥ�� �׸����� �� ��ǥ�� ��ȯ

        //���� ��ġ�� ���� ��ġ�� �ٸ���(�ǹ��� ���� ��ġ�� ������ ��ġ �ƴ϶��)
        if (prevPos != cellPos)
        {
            //�ǹ��� ���� ��ġ ����(���ο� ���� ��ġ��) ���󿡼��� �׸��� ���� �����ؼ� x,y���� 5f �����ص�..? 
            room.transform.localPosition = gridLayout.CellToLocalInterpolated(cellPos
                + new Vector3(0f, 0f, 0f));
            prevPos = cellPos; //���� ���� ��ġ�� prevPos�� ����
            FollowBuilding();
            room.MovingRoom();
        }
    }

    private void SelectRoom()
    {
        // ī�޶�κ��� ���콺 �����Ǳ����� ������ ����
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        RaycastHit2D hit = Physics2D.Raycast(ray.origin, ray.direction, Mathf.Infinity);
        Debug.Log(hit.collider);
        // ������ � ������Ʈ�� �浹�ߴ��� Ȯ��
        if (hit.collider != null)
        {
            // �浹�� ������Ʈ�� clone ����
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

