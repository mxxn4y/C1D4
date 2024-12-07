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
    
    /// <summary>
    /// 미니언의 특수재화 생산확률에 따라 특수 재화 생산 성공여부 반환
    /// </summary>
    public bool TryEarnSpecialGem()
    {
        System.Random random = new System.Random(Guid.NewGuid().GetHashCode());
        return random.Next(1, 11) <= Data.sGemProb;
    }

    /// <summary>
    /// 충성도 / 100만큼의 신뢰도 수치 상승
    /// </summary>
    public void GainTrust()
    {
        ++Trust;
        //Trust +=  (float)Data.loyalty / 100;
    }
}
