using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Scriptable Objects/Tile Data")]
public class ScriptableTile : ScriptableObject
{
    [field: SerializeField] public int TileNum { get; private set; }
    [field: SerializeField] public  Vector3Int TilePosition { get; private set; }
    [field: SerializeField] public List<Minion> MinionList { get; private set; }
}
