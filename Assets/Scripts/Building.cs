using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Building : MonoBehaviour
{
    public bool Placed { get; private set; }
    //public bool check;

    //�ǹ� ���� ������ -> �ǹ� ����� �°� �����Ϳ��� ����
    // z���� 0���� �ϸ� �ȵ�! 1�����ֱ�(area ũ�⿡ ���� array����µ� 0�� ���� ������ array�� Ÿ���� ������
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
