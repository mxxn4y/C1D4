using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Building : MonoBehaviour
{
    #region Fields and Properties
    //�ǹ� ���� ������ -> �ǹ� ����� �°� �����Ϳ��� ����
    // z���� 0���� �ϸ� �ȵ�! 1�����ֱ�(area ũ�⿡ ���� array����µ� 0�� ���� ������ array�� Ÿ���� ������
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
