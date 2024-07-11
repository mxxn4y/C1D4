using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Building : MonoBehaviour
{
    public bool Placed { get; private set; }
    //public bool check;

    //건물 영역 사이즈 -> 건물 사이즈에 맞게 에디터에서 수정
    // z값은 0으로 하면 안됨! 1적어주기(area 크기에 맞춰 array만드는데 0인 값이 있으면 array에 타일이 없을것
    public BoundsInt area; 

    #region Build Methods

    public bool CanBePlaced()
    {
        Vector3Int positionInt = GridBuildingSystem.current.gridLayout.LocalToCell(transform.position);
        BoundsInt areaTemp = area;
        areaTemp.position = positionInt;

        if (GridBuildingSystem.current.CanTakeArea(areaTemp))
        {
            return true;
        }

        return false;
    }

    public void Place()
    {
        Vector3Int positionInt = GridBuildingSystem.current.gridLayout.LocalToCell(transform.position);
        BoundsInt areaTemp = area;
        areaTemp.position = positionInt;
        Placed = true;
        GridBuildingSystem.current.TakeArea(areaTemp);
    }


    #endregion

    private void Update()
    {
        if(!Placed)
        {
            Color color = this.GetComponentInChildren<SpriteRenderer>().color;
            color.a = 0.5f;
            this.GetComponentInChildren<SpriteRenderer>().color = color;
            //check = false;
        }
        else
        {

            Color color = this.GetComponentInChildren<SpriteRenderer>().color;
            color.a = 1f;
            this.GetComponentInChildren<SpriteRenderer>().color = color;
            //check = true;
        }
    }

}
