using System.Collections;
using System.Collections.Generic;
using UnityEngine;
/// <summary>
/// Ÿ�� ������ csv���� �о Ÿ�� ��ü ����
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

    /// <summary> Ÿ���� ũ�� </summary>
    private Vector2 tileSize = new Vector2(1f,0.6f);
    public GameObject tilePrefab;

    private void Start()
    {
        LoadAllTiles();
    }

    /// <summary> csv ���Ͽ��� �о�� ��� Ÿ�� ������ ��ǥ�� ��ġ </summary>
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


