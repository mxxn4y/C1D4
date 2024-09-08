using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// 실제 타일 객체 클래스
/// </summary>
public class TileInfo: MonoBehaviour
{
    [field: SerializeField] public Vector2 _tilePos { get; private set; }
    [field: SerializeField] public TILE_TYPE _type { get; private set; }
    [field: SerializeField] public TILE_STATE _tileState { get; private set; }
    [field: SerializeField] public Sprite _defaultImage { get; private set; }
    [field: SerializeField] public Sprite _selectedImage { get; private set; }
    [field: SerializeField] public Sprite _placedImage { get; private set; }

    /// <summary>
    /// 타일 초기 설정
    /// </summary>
    /// <param name="tilePos"></param>
    /// <param name="type"></param>
    public void InitTile(Vector2 tilePos, TILE_TYPE type)
    {
        _type = type;
        _tilePos = tilePos;
        gameObject.transform.position = _tilePos;
        _tileState = TILE_STATE.DEFAULT;
        SetImage();
    }
    //타일의 상태 변경(이미지도 함께 변경)
    public void SetState(TILE_STATE state)
    {
        _tileState = state;
        SetImage();
    }

    ///<summary> 선택 여부에 따라 보여지는 타일의 이미지 변경 </summary>
    private void SetImage()
    {
        switch (_tileState)
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