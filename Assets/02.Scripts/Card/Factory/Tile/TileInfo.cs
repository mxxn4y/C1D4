using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// 실제 타일 객체 클래스
/// </summary>
public class TileInfo: MonoBehaviour
{
    [SerializeField] private Sprite defaultImage;
    [SerializeField] private Sprite selectedImage;
    [SerializeField] private Sprite placedImage;
    [SerializeField] private SpriteRenderer spriteRenderer;
    public Vector2 TilePos { get; private set; }
    public TILE_TYPE Type { get; private set; }
    
    private TILE_STATE tileState;
    public TILE_STATE TileState { 
        get { return tileState; } 
        set
        {
            tileState = value;
            spriteRenderer.sprite = tileState switch
            {
                TILE_STATE.DEFAULT => defaultImage,
                TILE_STATE.SELECTED => selectedImage,
                TILE_STATE.OCCUPIED => placedImage,
                _ => spriteRenderer.sprite
            };
        } }
    
    /// <summary>
    /// 타일 초기 설정
    /// </summary>
    public void SetTile(Vector2 _tilePos, TILE_TYPE _type)
    {
        this.Type = _type;
        this.TilePos = _tilePos;
        gameObject.transform.position = this.TilePos;
        TileState = TILE_STATE.DEFAULT;
        CardPlaceManager.Instance.OnCardMove += ShowTileSelected;
        CardPlaceManager.Instance.OnCardPlace += SelectTile;
    }

    private void ShowTileSelected()
    {
        if (TileState == TILE_STATE.OCCUPIED) return;
        
        if (CardPlaceManager.Instance.SelectedTile == this)
        {
            TileState = TILE_STATE.SELECTED;
        }
        else
        {
            TileState = TILE_STATE.DEFAULT;
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