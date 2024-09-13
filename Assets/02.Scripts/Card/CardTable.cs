using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// 카드 데이터 csv파일 읽어주는 클래스
/// </summary>
public class CardTable
{
    #region Singleton

    private static CardTable instance;
    public static CardTable Instance
    {
        get
        {
            if (null == instance)
            {
                instance = new CardTable();
            }
            return instance;
        }
    }

    #endregion

    private List<Dictionary<string, object>> _csv;

    /// <summary>
    /// 카드 id를 매개변수로 받아 해당 id를 가진 CardData 반환
    /// </summary>
    /// <param name="cid"></param>
    /// <returns></returns>
    public CardData GetData(string cid)
    {
        _csv = CSVReader.Read("CardTable");
        var cData = new CardData();

        foreach (var data in _csv)
        {
            if(cid == data["cid"].ToString())
            {
                cData._cid = data["cid"].ToString();
                cData._name = data["name"].ToString();
                if (data["type"].ToString() == "passion") { cData._type = CARD_TYPE.PASSION; }
                else if (data["type"].ToString() == "calm") { cData._type = CARD_TYPE.CALM; }
                else if (data["type"].ToString() == "wisdom") { cData._type = CARD_TYPE.WISDOM; }
                if (data["grade"].ToString() == "A") { cData._grade = CARD_GRADE.A; }
                else if (data["grade"].ToString() == "B") { cData._grade = CARD_GRADE.B; }
                else if (data["grade"].ToString() == "C") { cData._grade = CARD_GRADE.C; }
                cData._loyaltyRate = (int)data["loyaltyRate"];
                cData._stamina = (int)data["stamina"];
                cData._attack = (int)data["attack"];
                cData._defence = (int)data["defence"];
                cData._productSpeed = (int)data["productSpeed"];
                cData._productYield = (int)data["productYield"];
                cData._goodsProbability = (int)data["goodsProbability"];

                return cData;
            }
        }
        Debug.LogError($"ID가 존재하지 않습니다 cid: {cid}");
        return cData;
    }
}

/// <summary>
/// csv에서 읽어온 카드 데이터 저장할 struct
/// </summary>
public struct CardData
{
    public string _cid;
    public string _name;
    public CARD_TYPE _type;
    public CARD_GRADE _grade;
    public int _loyaltyRate;
    public int _stamina;
    public int _attack;
    public int _defence;
    public int _productSpeed;
    public int _productYield;
    public int _goodsProbability;
}


/// <summary>
/// 카드 속성 PASSION,CALM,WISDOM
/// </summary>
public enum CARD_TYPE
{
    PASSION,
    CALM,
    WISDOM
}

/// <summary>
/// 카드 등급 A,B,C
/// </summary>
public enum CARD_GRADE
{
    A,
    B,
    C
}
