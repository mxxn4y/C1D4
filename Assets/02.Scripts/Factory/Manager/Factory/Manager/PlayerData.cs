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

    //보유 아이템


    //보유 미니언 리스트
    public List<Minion> MinionList { get; private set; } = new List<Minion>();
    public List<Minion> SelectedMinions { get; private set; } = new List<Minion>();

    //플레이어가 소유한 특정 타입의 미니언 개수 -> 필요 없어짐.. 한도가 없어져서..
    // public static int CountMinion(MinionEnums.TYPE _type)
    // {
    //     int num = 0;
    //
    //     switch (_type)
    //     {
    //         case MinionEnums.TYPE.PASSION:
    //             foreach (var minion in minions)
    //             {
    //                 if (minion.Data.ToString().StartsWith('P'))
    //                 {
    //                     num += minion.Value;
    //                 }
    //             }
    //             break;
    //         case MinionEnums.TYPE.CALM:
    //             foreach (var card in playerMinions)
    //             {
    //                 if (card.Key.StartsWith('C'))
    //                 {
    //                     num += card.Value;
    //                 }
    //             }
    //             break;
    //         case MinionEnums.TYPE.WISDOM:
    //             foreach (var card in playerMinions)
    //             {
    //                 if (card.Key.StartsWith('W'))
    //                 {
    //                     num += card.Value;
    //                 }
    //             }
    //             break;
    //     }
    //     return num;
    // }

    //매개변수로 받은 미니언 플레이어 덱에 없으면 추가, 존재하면 획득 카운트 +1
    
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

}
