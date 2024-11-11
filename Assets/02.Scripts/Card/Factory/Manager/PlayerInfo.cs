using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class PlayerInfo
{
    //슬롯 한도 -> 없어짐
    //public static int[] slotLimit { get; private set; } = { 8, 8, 8 };

    //카드 획득 개수 -> 한도 사라졌으므로 일단 통일
    //public static int[] gainNum { get; private set; } = { 5, 5, 5 };
    public static int gainNum { get; private set; } = 5;

    //소지금
    public static int gem { get; private set; }
    public static int specialGem { get; private set; }

    //보유 아이템


    //보유 미니언 리스트
    //public static Dictionary<string, int> playerMinions { get; private set; } = new ();
    public static List<Minion> MinionList { get; private set; } = new List<Minion>();
    public static List<Minion> SelectedMinions { get; private set; } = new List<Minion>();

    // public static void SortCards()
    // {
    //     playerMinions = playerMinions.OrderBy(_item => _item.Key)
    //         .ToDictionary(_pair => _pair.Key, _pair => _pair.Value);
    // }

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
    
    public static void AddMinion(string _mid)
    {
        Debug.Log("획득한 카드: " + _mid);

        if (MinionList.Exists(_minion => _minion.BaseData.mid == _mid))
        {
            MinionList.Find(_minion => _minion.BaseData.mid == _mid).IncreaseCount();
        }
        else
        {
            MinionList.Add(new Minion(_mid));
            MinionList = MinionList.OrderBy(_minion => _minion.BaseData.mid).ToList();
        }
    }
    
    // 플레이어가 획득 가능한 카드 개수 -> 한도 사라져서 삭제해야할듯
    // public static int GainableCardNum(MinionEnums.TYPE _type)
    // {
    //     int availableNum = 0;
    //     int gainAmount = 0;
    //
    //     switch (_type)
    //     {
    //         case MinionEnums.TYPE.PASSION:
    //             availableNum = slotLimit[0] - CountMinion(_type);
    //             gainAmount = gainNum[0];
    //             break;
    //         case MinionEnums.TYPE.CALM:
    //             availableNum = slotLimit[1] - CountMinion(_type);
    //             gainAmount = gainNum[1];
    //             break;
    //         case MinionEnums.TYPE.WISDOM:
    //             availableNum = slotLimit[2] - CountMinion(_type);
    //             gainAmount = gainNum[2];
    //             break;
    //     }
    //     Debug.Log("획득 가능한 카드 개수: " + Mathf.Min(availableNum, gainAmount));
    //     return Mathf.Min(availableNum, gainAmount);
    // }
}
