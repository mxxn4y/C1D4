using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// 실제 타일 객체 클래스
/// </summary>
public class TileInfo: MonoBehaviour
{
    public Vector2 TilePos { get; private set; }
    public TILE_TYPE Type { get; private set; }
    private TILE_STATE tileState;
    public TILE_STATE TileState { 
        get { return tileState; } 
        private set {
            tileState = value;
            switch(tileState)
            {
                case TILE_STATE.DEFAULT:
                    GetComponentInChildren<SpriteRenderer>().sprite = defaultImage;
                    break;
                case TILE_STATE.SELECTED:
                    GetComponentInChildren<SpriteRenderer>().sprite = selectedImage;
                    break;
                case TILE_STATE.OCCUPIED:
                    GetComponentInChildren<SpriteRenderer>().sprite = placedImage;
                    break;
            }
        } }
    [SerializeField] private Sprite defaultImage;
    [SerializeField] private Sprite selectedImage;
    [SerializeField] private Sprite placedImage;

    /// <summary>
    /// 타일 초기 설정
    /// </summary>
    /// <param name="_tilePos"></param>
    /// <param name="_type"></param>
    public void SetTile(Vector2 _tilePos, TILE_TYPE _type)
    {
        this.Type = _type;
        this.TilePos = _tilePos;
        gameObject.transform.position = this.TilePos;
        TileState = TILE_STATE.DEFAULT;
        CardPlaceManager.Instance.OnCardMove += ShowTileSelected;
        CardPlaceManager.Instance.OnCardPlace += SelectTile;
    }

    public void UnSelectTile()
    {
        TileState = TILE_STATE.DEFAULT;
    }

    private void ShowTileSelected()
    {
        if (TileState != TILE_STATE.OCCUPIED)
        {         
            if (CardPlaceManager.Instance.selectedTile == this)
            {
                TileState = TILE_STATE.SELECTED;
            }
            else
            {
                TileState = TILE_STATE.DEFAULT;
            }
        }
    }

    private void SelectTile()
    {
        if(TileState == TILE_STATE.SELECTED)
        {
            TileState = TILE_STATE.OCCUPIED;
        }
    }

}


public enum TILE_STATE
{
    DEFAULT,
    SELECTED,
    OCCUPIED
}
public enum TILE_TYPE
{
    PASSION,
    CALM,
    WISDOM
}