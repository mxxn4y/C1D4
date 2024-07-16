using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// ���� ���� �Ϲ����� �Ӽ��� ��� ����
/// </summary>
[CreateAssetMenu(menuName = "RoomData")]
public class ScriptableRoom : ScriptableObject
{
    [field:SerializeField]public ScriptableCard Card { get; private set; }
    [field:SerializeField]public Sprite Image { get; private set; }
    //[field:SerializeField]public FurnitureType Furnituere { get; private set; }

    [field: SerializeField] public BoundsInt area;

}

public enum FurnitureType
{
    FType1,
    FType2
}
