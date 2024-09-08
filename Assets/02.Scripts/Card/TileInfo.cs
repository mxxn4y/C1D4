using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// ���� Ÿ�� ��ü Ŭ����
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
    /// Ÿ�� �ʱ� ����
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
    //Ÿ���� ���� ����(�̹����� �Բ� ����)
    public void SetState(TILE_STATE state)
    {
        _tileState = state;
        SetImage();
    }

    ///<summary> ���� ���ο� ���� �������� Ÿ���� �̹��� ���� </summary>
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