using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TileSetter : MonoBehaviour
{
    #region Fields and Properties

    [field: SerializeField] public ScriptableTile TileData { get; private set; }
    [field: SerializeField] public GameObject minionPrefab { get; private set; }
    [field: SerializeField] public TILE_STATE tileState { get; private set; }
    [field: SerializeField] public GridLayout settingGrid { get; private set; }
    [field: SerializeField] public Sprite defaultImage { get; private set; }
    [field: SerializeField] public Sprite selectedImage { get; private set; }
    [field: SerializeField] public Sprite placedImage { get; private set; }



    #endregion

    #region Methods
    private void Start()
    {
        SetTileInit();
    }

    private void SetTileInit()
    {
        tileState = TILE_STATE.DEFAULT;
        SetTilePos();
        SetTileImage();
    }

    //Ÿ�� ��ġ Ÿ�ϵ����Ϳ��� �Է��� ����ǥ��
    private void SetTilePos()
    {
        transform.position = settingGrid.CellToWorld(TileData.TilePosition);
    }

    //���� ���ο� ���� �������� Ÿ���� �̹��� ����
    private void SetTileImage()
    {
        switch(tileState){
            case TILE_STATE.DEFAULT:
                GetComponentInChildren<SpriteRenderer>().sprite = defaultImage;
                break;
            case TILE_STATE.SELECTED:
                GetComponentInChildren<SpriteRenderer>().sprite = selectedImage;
                break;
            case TILE_STATE.PLACED:
                GetComponentInChildren<SpriteRenderer>().sprite = placedImage;
                break;

        }
    }

    public void SetTileType(TILE_STATE state)
    {
        tileState = state;
        SetTileImage();
    }

    public void ActivateTileMinion(ScriptableMinion minion)
    {
        minionPrefab.SetActive(true);
        minionPrefab.GetComponent<Minion>().SetMinion(minion);
    }


    #endregion
}

public enum TILE_STATE
{
    DEFAULT,
    SELECTED,
    PLACED
}
