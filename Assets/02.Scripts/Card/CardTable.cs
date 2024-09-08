using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// ī�� ������ csv���� �о��ִ� Ŭ����
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
    /// ī�� id�� �Ű������� �޾� �ش� id�� ���� CardData ��ȯ
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
        Debug.LogError($"ID�� �������� �ʽ��ϴ� cid: {cid}");
        return cData;
    }
}

/// <summary>
/// csv���� �о�� ī�� ������ ������ struct
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
    A,
    B,
    C
}
