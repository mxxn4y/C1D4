using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// 실제 타일 객체 클래스
/// </summary>
public class TileInfo: MonoBehaviour
{
    public Vector2 _tilePos { get; private set; }
    public TILE_TYPE _type { get; private set; }
    private TILE_STATE tileState;
    public TILE_STATE _tileState { 
        get { return tileState; } 
        private set {
            tileState = value;
            switch(tileState)
            {
                case TILE_STATE.DEFAULT:
                    GetComponentInChildren<SpriteRenderer>().sprite = _defaultImage;
                    break;
                case TILE_STATE.SELECTED:
                    GetComponentInChildren<SpriteRenderer>().sprite = _selectedImage;
                    break;
                case TILE_STATE.OCCUPIED:
                    GetComponentInChildren<SpriteRenderer>().sprite = _placedImage;
                    break;
            }
        } }
    [SerializeField] private Sprite _defaultImage;
    [SerializeField] private Sprite _selectedImage;
    [SerializeField] private Sprite _placedImage;

    /// <summary>
    /// 타일 초기 설정
    /// </summary>
    /// <param name="tilePos"></param>
    /// <param name="type"></param>
    public void SetTile(Vector2 tilePos, TILE_TYPE type)
    {
        _type = type;
        _tilePos = tilePos;
        gameObject.transform.position = _tilePos;
        _tileState = TILE_STATE.DEFAULT;
        CardPlaceManager.Instance.OnCardMove += ShowTileSelected;
        CardPlaceManager.Instance.OnCardPlace += SelectTile;
    }

    public void UnSelectTile()
    {
        _tileState = TILE_STATE.DEFAULT;
    }

    private void ShowTileSelected()
    {
        if (_tileState != TILE_STATE.OCCUPIED)
        {         
            if (CardPlaceManager.Instance._selectedTile == this)
            {
                _tileState = TILE_STATE.SELECTED;
            }
            else
            {
                _tileState = TILE_STATE.DEFAULT;
            }
        }
    }

    private void SelectTile()
    {
        if(_tileState == TILE_STATE.SELECTED)
        {
            _tileState = TILE_STATE.OCCUPIED;
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
    P,
    C,
    W
}