using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Minion
{
    public MinionBaseData BaseData { get; private set; } //csv로 가져온 기본 정보
    public int Level { get; private set; } // 레벨
    public int GainCount {get; private set;} // 획득 횟수
    public bool Exhaustion {get; private set;} // 사용 가능 여부
    // 다시 사용 가능한 상태가 되는 날짜 변수도 필요할듯

    public Minion(string _mid)
    {
        this.BaseData = MinionTable.Instance.GetData(_mid);;
        this.Level = 0;
        this.GainCount++;
        this.Exhaustion = false;
    }

    public void IncreaseCount()
    {
        GainCount++;
        // 레벨 증가해야하는지 검사하는 로직
    }

    public void SetExhaustion()
    {
        Exhaustion = true;
    }
    
    public bool TryEarnSpecialGem()
    {
        System.Random random = new System.Random(Guid.NewGuid().GetHashCode());
        if (random.Next(1, 11) <= BaseData.sGemProb)
        {
            return true;
        }
        return false;
    }
}
