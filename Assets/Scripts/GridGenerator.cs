using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GridGenerator : MonoBehaviour
{
    [SerializeField] GameObject tile1;
    [SerializeField] GameObject tile2;
    [SerializeField] GameObject tile3;
    [SerializeField] int gridHeight = 10;
    [SerializeField] int gridWidth = 10;
    [SerializeField] float tileSize = 1f;

    void Start()
    {
        //GenerateGrid();
        //GenerateGrid2();
        GenerateRoom();
    }

    private void GenerateGrid()
    {
        for(int x = 0; x < gridWidth; x++)
        {
            for (int y = 0; y < gridHeight; y++)
            {
                GameObject newTile = Instantiate(tile1, transform);

                float posX = (x* tileSize + y * tileSize) / 2f;
                float posY = (x * tileSize - y * tileSize) / 8f;

                newTile.transform.position = new Vector2(posX, posY);
                newTile.name = x + " , " + y;
            }
        }
    }

    private void GenerateGrid2()
    {
        for (int x = 0; x < gridWidth; x++)
        {
            for (int y = 0; y < gridHeight; y++)
            {
                GameObject newTile = Instantiate(tile2, transform);

                float posX = (x * tileSize) / 2f;
                float posY = (x * tileSize * 0.125f + y * tileSize * 0.5f);

                newTile.transform.position = new Vector2(posX, posY);
                newTile.name = x + " , " + y;
            }
        }
    }

    private void GenerateRoom()
    {
        for (int x = 0; x < gridWidth; x++)
        {
            for (int y = 0; y < gridHeight; y++)
            {
                GameObject floor = Instantiate(tile1, transform);
                GameObject wallL = Instantiate(tile2, transform);
                GameObject wallR = Instantiate(tile3, transform);

                float floorPosX = (x * tileSize + y * tileSize) / 2f;
                float floorPosY = (x * tileSize - y * tileSize) / 8f;

                float wallLPosX = (x * tileSize) / 2f;
                float wallLPosY = (x * tileSize * 1f + y * tileSize * 4f) / 8f;

                float wallRPosX = (x * tileSize / 2f) + (gridWidth + 1) / 2f;
                float wallRPosY = (x * tileSize * 1f + y * tileSize * 4f) / 8f;

                floor.transform.position = new Vector2(floorPosX, floorPosY);
                wallL.transform.position = new Vector2(wallLPosX, wallLPosY);
                wallR.transform.position = new Vector2(wallRPosX, wallRPosY);

                floor.name = x + " , " + y;
                wallL.name = x + " , " + y;
                wallR.name = x + " , " + y;
            }
        }
    }
}
