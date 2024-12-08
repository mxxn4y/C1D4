using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class PlayerData: Singleton<PlayerData>
{ 
    // 뽑기 1회로 획득하는 미니언 개수
    public int GainNum { get; private set; } = 5;

    //소지한 재화량(일반/특수)
    public int Gem { get; private set; }
    public int SpecialGem { get; private set; }

    public PlayerData()
    {
        // 기본 미니언 추가
        MinionList.Add(new Minion("P001"));
        MinionList.Add(new Minion("C001"));
        MinionList.Add(new Minion("W001"));
    }

    //보유 아이템


    //보유 미니언 리스트
    public List<Minion> MinionList { get; private set; } = new List<Minion>();
    public List<Minion> SelectedMinions { get; private set; } = new List<Minion>();
    
    public void AddMinion(string _mid)
    {
        MinionList.Add(new Minion(_mid));
        MinionList = MinionList.OrderBy(_m => _m.Data.mid).ToList();
    }

    public Minion GetPlayerMinionById(string _mid)
    {
        foreach(var minion in MinionList)
        {
            if(minion.Data.mid == _mid) return minion;
        }
        return null;
    }

    public void AddGems(int _gem, int _specialGem)
    {
        Gem += _gem;
        SpecialGem += _specialGem;
    }
}
