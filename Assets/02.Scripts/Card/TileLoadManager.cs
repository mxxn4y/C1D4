using System.Collections;
using System.Collections.Generic;
using UnityEngine;
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
    private Vector2 tileSize = new Vector2(1f,0.6f);
    public GameObject tilePrefab;

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
            var tilePos = new Vector2((int)data["x"] * tileSize.x, (int)data["y"] * tileSize.y);
            switch (data["type"].ToString())
            {
                case "passion":
                    tile.GetComponent<TileInfo>().SetTile(tilePos, TILE_TYPE.P);
                    break;
                case "calm":
                    tile.GetComponent<TileInfo>().SetTile(tilePos, TILE_TYPE.C);
                    break;
                case "wisdom":
                    tile.GetComponent<TileInfo>().SetTile(tilePos, TILE_TYPE.W);
                    break;
            }
            
        }

    }

}


