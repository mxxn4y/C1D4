using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;
/// <summary>
/// 타일 데이터 csv파일 읽어서 타일 객체 생성
/// </summary>
public class TileLoadManager : MonoBehaviour
{
    #region Singleton

    private static TileLoadManager instance = null;

    void Awake()
    {
        if (null == instance)
        {
            instance = this;
        }
        else
        {
            Destroy(this.gameObject);
        }
    }

    public static TileLoadManager Instance
    {
        get
        {
            if (null == instance)
            {
                return null;
            }
            return instance;
        }
    }

    #endregion

    /// <summary> 타일의 크기 </summary>
    public GameObject tilePrefab;
    [SerializeField] private Grid tilemap;

    #region Methods
    private void Start()
    {
        LoadAllTiles();
    }

    /// <summary> csv 파일에서 읽어온 모든 타일 각각의 좌표에 배치 </summary>
    private void LoadAllTiles()
    {
        var tiles = CSVReader.Read("TileTable");

        foreach(var data in tiles)
        {
            var tile = Instantiate(tilePrefab);
            var tilePos = tilemap.CellToWorld(new Vector3Int((int)data["x"], (int)data["y"]));
            tilePos = new Vector3(tilePos.x, tilePos.y + 0.3f, tilePos.z);

            switch (data["type"].ToString())
            {
                case "passion":
                    tile.GetComponent<TileInfo>().SetTile(tilePos, TILE_TYPE.PASSION);
                    break;
                case "calm":
                    tile.GetComponent<TileInfo>().SetTile(tilePos, TILE_TYPE.CALM);
                    break;
                case "wisdom":
                    tile.GetComponent<TileInfo>().SetTile(tilePos, TILE_TYPE.WISDOM);
                    break;
            }
            
        }

    }
    #endregion

}


