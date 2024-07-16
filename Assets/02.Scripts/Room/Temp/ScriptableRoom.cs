using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// 개별 방의 일반적인 속성들 모두 가짐
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
