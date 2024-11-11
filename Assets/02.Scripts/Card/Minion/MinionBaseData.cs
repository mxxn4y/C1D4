using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// csv에서 읽어온 미니언 데이터 저장
/// </summary>
public struct MinionBaseData
{
    public string mid;
    public string name;
    public MinionEnums.TYPE type;
    public MinionEnums.GRADE grade;
    public int loyalty;
    public int stamina;
    public int speed;
    public int efficiency;
    public int sGemProb;
}