using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Building : MonoBehaviour
{
    #region Fields and Properties
    //건물 영역 사이즈 -> 건물 사이즈에 맞게 에디터에서 수정
    // z값은 0으로 하면 안됨! 1적어주기(area 크기에 맞춰 array만드는데 0인 값이 있으면 array에 타일이 없을것
    [field: SerializeField] public BoundsInt area;

    private bool _moving = false;

    public Image RoomImage;
    #endregion

    #region Methods

    private void Update()
    {
        if (_moving)
        {
            Color color = RoomImage.GetComponent<Image>().color;
            color.a = 0.5f;
            RoomImage.GetComponent<Image>().color = color;
        }
        else
        {
            Color color = RoomImage.GetComponent<Image>().color;
            color.a = 1f;
            RoomImage.GetComponent<Image>().color = color;
        }

    }

    //public bool CanBePlaced()
    //{
    //    Vector3Int positionInt = GridBuildingSystem.current.gridLayout.LocalToCell(transform.position);
    //    BoundsInt areaTemp = area;
    //    areaTemp.position = positionInt;

    //    if (GridBuildingSystem.current.CanTakeArea(areaTemp))
    //    {
    //        return true;
    //    }

    //    return false;
    //}

    public void Place()
    {
        _moving = false;
    }
    public void MovingRoom()
    {
        _moving = true;
    }


    #endregion



}
