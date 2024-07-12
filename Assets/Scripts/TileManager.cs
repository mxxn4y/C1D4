using System.Collections;
using System.Collections.Generic;
using UnityEditor.Rendering;
using UnityEngine;

public class TileManager : MonoBehaviour
{
    [SerializeField] GameObject[] tile;
    [SerializeField] int gridHeight = 10;
    [SerializeField] int gridWidth = 10;
    [SerializeField] float tileSize = 1f;

    private Dictionary<Vector2, GameObject> tiles;
    void Start()
    {
        //Ÿ�ϸ� ����
        GeneratedGrid();
    }
    private void GeneratedGrid()
    {
        //��ųʸ� �ʱ�ȭ
        tiles = new Dictionary<Vector2, GameObject>();

        for (int x = 0; x< gridWidth; x++)
        {
            for (int y=0; y< gridHeight; y++)
            {
                var randomTile = tile[Random.Range(0, tile.Length)];
                GameObject newTile = Instantiate(randomTile, transform);

                //�ǹ��� 0.5, 0.5 && X : Y ���� 2:1
                float posX = (x * tileSize - y * tileSize) / 2f;
                float posY = (x * tileSize + y * tileSize) / 4f;

                newTile.transform.localPosition = new Vector2(posX, posY);
                newTile.name = x + ", " + y;

                //���� �ν��Ͻ�ȭ�� Ÿ���� �̸��� Ű������ �ϴ� newTile ����
                tiles[new Vector2(x, y)] = newTile;
            }
        }
    }

    public GameObject GetTileAtPosition(Vector2 pos)
    {
        Vector2 dictionaryKey = new Vector2(2 * pos.y + pos.x, 2 * pos.y - pos.x);
        return tiles[dictionaryKey];
    }
}
