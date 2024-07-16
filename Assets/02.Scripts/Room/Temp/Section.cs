using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;
/// <summary>
///  �� ��ġ ������ ����
/// </summary>
public class Section : ScriptableObject
{
    [field: SerializeField] public int SectionNum { get; private set; }
    [field: SerializeField] public int MaxRoom { get; private set; }
    [field: SerializeField] public List<ScriptableRoom> RoomsInSection { get; private set; }
    [field: SerializeField] public TileBase BaseTile { get; private set; }


    public void RemoveRoomFromSection(ScriptableRoom room)
    {
        if (HasRoom(room))
        {
            RoomsInSection.Remove(room);
        }
        else
        {
            //ī�尡 ���� ������ ��� �� ��
        }
    }

    //ī�� ���� �߰�
    public void AddRoomToSection(ScriptableRoom room)
    {
        RoomsInSection.Add(room);
    }


    public bool HasRoom(ScriptableRoom room)
    {
        if (RoomsInSection.Contains(room))
        {
            return true;
        }
        else return false;
    }
}
