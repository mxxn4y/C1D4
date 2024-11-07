using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// 카드 데이터 csv파일 읽어주고 id값으로 카드 data 조회 가능
/// </summary>
public class MinionTable
{
    #region Singleton

    private static MinionTable instance;
    public static MinionTable Instance
    {
        get
        {
            if (null == instance)
            {
                instance = new MinionTable();
            }
            return instance;
        }
    }

    #endregion

    private readonly List<Dictionary<string, object>> minionTable = CSVReader.Read("MinionTable");

    /// <summary>
    /// 카드 id를 매개변수로 받아 해당 id를 가진 CardData return
    /// </summary>
    public MinionStructs.CardData GetData(string _minionId)
    {
        var cardData = new MinionStructs.CardData();

        foreach (var data in minionTable)
        {
            if(_minionId == data["cid"].ToString())
            {
                cardData.cid = data["cid"].ToString();
                cardData.name = data["name"].ToString();
                if (data["type"].ToString() == "passion") { cardData.type = MinionEnums.MINION_TYPE.PASSION; }
                else if (data["type"].ToString() == "calm") { cardData.type = MinionEnums.MINION_TYPE.CALM; }
                else if (data["type"].ToString() == "wisdom") { cardData.type = MinionEnums.MINION_TYPE.WISDOM; }
                if (data["grade"].ToString() == "A") { cardData.grade = MinionEnums.MINION_GRADE.A; }
                else if (data["grade"].ToString() == "B") { cardData.grade = MinionEnums.MINION_GRADE.B; }
                else if (data["grade"].ToString() == "C") { cardData.grade = MinionEnums.MINION_GRADE.C; }
                cardData.loyaltyRate = (int)data["loyaltyRate"];
                cardData.stamina = (int)data["stamina"];
                cardData.attack = (int)data["attack"];
                cardData.defence = (int)data["defence"];
                cardData.produceSpeed = (int)data["productSpeed"];
                cardData.productYield = (int)data["productYield"];
                cardData.goodsProbability = (int)data["goodsProbability"];

                return cardData;
            }
        }
        Debug.LogError($"ID가 존재하지 않습니다 cid: {_minionId}");
        return cardData;
    }

    /// <summary>
    /// 매개변수로 받은 타입과 등급의 카드 아이디 리스트 반환
    /// </summary>
    public List<string> GetAllCards(MinionEnums.MINION_TYPE _type, MinionEnums.MINION_GRADE _grade)
    {
        var cards = new List<string>();
        var type = "";
        var grade = "";

        if (_type == MinionEnums.MINION_TYPE.PASSION) type = "passion";
        else if (_type == MinionEnums.MINION_TYPE.CALM) type = "calm";
        else if (_type == MinionEnums.MINION_TYPE.WISDOM) type = "wisdom";

        if (_grade == MinionEnums.MINION_GRADE.A) grade = "A";
        else if (_grade == MinionEnums.MINION_GRADE.B) grade = "B";
        else if (_grade == MinionEnums.MINION_GRADE.C) grade = "C";

        foreach (var data in minionTable)
        {
            if (data["type"].ToString() == type && data["grade"].ToString() == grade)
            {
                cards.Add(data["cid"].ToString());
            }
        }
        
        return cards;
    }
}





