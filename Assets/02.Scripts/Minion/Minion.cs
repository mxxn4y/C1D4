using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Minion
{
    private readonly MinionBaseData originalData; //csv로 가져온 기본 정보

    public MinionBaseData Data
    {
        get
        {
            var data = originalData;
            data.stamina = (int)Math.Round(data.stamina * (1 + 0.05f * GainCount));
            data.efficiency = (int)Math.Round(data.efficiency * (1 + 0.1f * GainCount));
            return data;
        }
    } 

    //public int Level { get; private set; } // 레벨 -> 보류

    public int GainCount {get; private set;} // 획득 횟수
    public bool Exhaustion {get; private set;} // 사용 가능 여부
    // 다시 사용 가능한 상태가 되는 날짜 변수도 필요할듯
    public float Trust { get; private set; }

    /// <summary>
    /// mid를 인수로 받아 미니언 객체 생성하는 생성자
    /// </summary>
    public Minion(string _mid)
    {
        this.originalData = MinionTable.Instance.GetData(_mid);
        this.Trust = 0;
        //this.Level = 0;
        this.GainCount = 0;
        this.Exhaustion = false;
    }

    /// <summary>
    /// 획득 개수를 1 증가시키는 메소드
    /// </summary>
    public void IncreaseCount()
    {
        GainCount++;
        // 레벨 증가해야하는지 검사하는 로직
    }

    /// <summary>
    /// 미니언의 탈진 설정 메소드
    /// </summary>
    public void SetExhaustion(bool _isExhausted)
    {
        Exhaustion = _isExhausted;
    }
    
    public bool TryEarnSpecialGem()
    {
        var random = new System.Random(Guid.NewGuid().GetHashCode());
        if (random.Next(1, 11) <= Data.sGemProb)
        {
            return true;
        }
        return false;
    }

    public void GainTrust()
    {
        Trust +=  Data.loyalty / 100;
    }
}
