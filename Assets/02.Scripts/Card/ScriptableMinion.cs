using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Scriptable Objects/Minion Data")]
public class ScriptableMinion : ScriptableObject
{
    [field: SerializeField] public ScriptableCard Card { get; private set; }
    [field: SerializeField] public Sprite Image { get; private set; }
}
