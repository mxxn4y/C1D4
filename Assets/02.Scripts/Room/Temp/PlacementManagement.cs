using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;
/// <summary>
/// �̱���
/// ��ġ ������ ī�� ��Ÿ���� �� ��ġ �� ������ �������� �ƴ��� Ȯ������
/// </summary>
public class PlacementManagement : TilemapManagement
{
    #region Fields and Properties

    public static PlacementManagement Instance;
    public Room _roomPrefab;
    public Canvas _roomCanvas;

    public GridLayout _gridLayout;
    public Tilemap _mainTilemap;
    public Tilemap _tempTilemap;

    //private Room _room;
    //private Vector3 _prevPos; 
    private BoundsInt _prevArea;

    //�̰� ���߿� ī��� �ٲܰŶ� �ӽ�
    private RectTransform _rectTransform;
    [SerializeField] private Vector3 _roomStartPos = new Vector3(140, -120, 0); //�� ���� ��ġ ��ġ
    [SerializeField] private int _roomMargin = -210; //�� ����

    #endregion

    #region Unity Methods
    private void Awake()
    {
        Instance = this; //�̱���
    }

    private void Start()
    {
        string tilePath = @"Tile\";
        tileBases.Add(TileType.Empty, null);
        tileBases.Add(TileType.White, Resources.Load<TileBase>(tilePath + "basic"));
        tileBases.Add(TileType.Green, Resources.Load<TileBase>(tilePath + "available"));
        tileBases.Add(TileType.Red, Resources.Load<TileBase>(tilePath + "unavailable"));
        InstantiateRoom();
    }

    #endregion

    #region Methods
    private void InstantiateRoom()
    {
        for (int i = 0; i < 5; i++)
        {
            Room room = Instantiate(_roomPrefab, _roomCanvas.transform);
            InitPlace(room, i);

        }

    }
    private void InitPlace(Room room, int i)
    {
        _rectTransform = room.GetComponent<RectTransform>();
        Vector3 margin = new Vector3(0, _roomMargin * i, 0);
        _rectTransform.anchoredPosition = _roomStartPos + margin;
    }

    public void ClearArea(BoundsInt area, Tilemap tilemap)
    {
        if (tilemap == _tempTilemap)
        {
            SetTilesBlock(area, TileType.Empty, tilemap);
        }
        else
        {
            SetTilesBlock(area, TileType.White, tilemap);
        }

    }


    public void FollowRoom(Room room)
    {
        ClearArea(_prevArea, _tempTilemap);

        BoundsInt cuurnetArea = room.RoomData.area;

        if (CanTakeArea(cuurnetArea))
        {
            SetTilesBlock(cuurnetArea, TileType.Green, _tempTilemap);
        }
        else
        {
            SetTilesBlock(cuurnetArea, TileType.Red, _tempTilemap);
        }
        _prevArea = cuurnetArea;
    }

    public void PlaceRoom(Room room)
    {
        room.SetPlace();
        TakeArea(_prevArea);
    }

    public bool CanTakeArea(BoundsInt area)
    {
        TileBase[] baseArray = GetTilesBlock(area, _mainTilemap);

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
        SetTilesBlock(area, TileType.Empty, _tempTilemap);
        SetTilesBlock(area, TileType.Empty, _mainTilemap);
    }

    #endregion
}
