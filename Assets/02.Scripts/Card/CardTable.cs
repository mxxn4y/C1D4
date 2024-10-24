using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// ī�� ������ csv���� �о��ְ� id������ ī�� data ��ȸ ����
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

    private List<Dictionary<string, object>> csv = CSVReader.Read("CardTable");

    /// <summary>
    /// ī�� id�� �Ű������� �޾� �ش� id�� ���� CardData ��ȯ
    /// </summary>
    /// <param name="_cid"></param>
    /// <returns></returns>
    public CardData GetData(string _cid)
    {
        var cData = new CardData();

        foreach (var data in csv)
        {
            if(_cid == data["cid"].ToString())
            {
                cData.cid = data["cid"].ToString();
                cData.name = data["name"].ToString();
                if (data["type"].ToString() == "passion") { cData.type = CARD_TYPE.PASSION; }
                else if (data["type"].ToString() == "calm") { cData.type = CARD_TYPE.CALM; }
                else if (data["type"].ToString() == "wisdom") { cData.type = CARD_TYPE.WISDOM; }
                if (data["grade"].ToString() == "A") { cData.grade = CARD_GRADE.A; }
                else if (data["grade"].ToString() == "B") { cData.grade = CARD_GRADE.B; }
                else if (data["grade"].ToString() == "C") { cData.grade = CARD_GRADE.C; }
                cData.loyaltyRate = (int)data["loyaltyRate"];
                cData.stamina = (int)data["stamina"];
                cData.attack = (int)data["attack"];
                cData.defence = (int)data["defence"];
                cData.produceSpeed = (int)data["productSpeed"];
                cData.productYield = (int)data["productYield"];
                cData.goodsProbability = (int)data["goodsProbability"];

                return cData;
            }
        }
        Debug.LogError($"ID�� �������� �ʽ��ϴ� cid: {_cid}");
        return cData;
    }

    /// <summary>
    /// �Ű������� ���� Ÿ�԰� ����� ī�� ���̵� ����Ʈ ��ȯ
    /// </summary>
    /// <param name="_type"></param>
    /// <param name="_grade"></param>
    /// <returns></returns>
    public List<string> GetAllCards(CARD_TYPE _type, CARD_GRADE _grade)
    {
        var cards = new List<string>();
        var type = "";
        var grade = "";

        if (_type == CARD_TYPE.PASSION) type = "passion";
        else if (_type == CARD_TYPE.CALM) type = "calm";
        else if (_type == CARD_TYPE.WISDOM) type = "wisdom";

        if (_grade == CARD_GRADE.A) grade = "A";
        else if (_grade == CARD_GRADE.B) grade = "B";
        else if (_grade == CARD_GRADE.C) grade = "C";

        foreach (var data in csv)
        {
            if (data["type"].ToString() == type && data["grade"].ToString() == grade)
            {
                cards.Add(data["cid"].ToString());
            }
        }
        
        return cards;
    }
}

/// <summary>
/// csv���� �о�� ī�� ������ ����
/// </summary>
public struct CardData
{
    public string cid;
    public string name;
    public CARD_TYPE type;
    public CARD_GRADE grade;
    public int loyaltyRate;
    public int stamina;
    public int attack;
    public int defence;
    public int produceSpeed;
    public int productYield;
    public int goodsProbability;
}


/// <summary>
/// ī�� �Ӽ� PASSION,CALM,WISDOM
/// </summary>
public enum CARD_TYPE
{
    PASSION,
    CALM,
    WISDOM
}

/// <summary>
/// ī�� ��� A,B,C
/// </summary>
public enum CARD_GRADE
{
    NONE,
    A,
    B,
    C
}
