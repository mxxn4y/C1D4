using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

/// <summary>
/// CSVReader 사용한 미니언 데이터 테이블
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
    /// 매개변수로 받은 id에 해당하는 미니언 정보를 담은 MinionData 반환
    /// </summary>
    public MinionBaseData GetData(string _mid)
    {
        var baseData = new MinionBaseData();

        foreach (var data in minionTable
                     .Where(_data => _mid == _data["mid"].ToString()))
        {
            baseData.mid = data["mid"].ToString();
            baseData.name = data["name"].ToString();
            try
            {
                baseData.type = data["type"].ToString() switch
                {
                    "passion" => MinionEnums.TYPE.PASSION,
                    "calm" => MinionEnums.TYPE.CALM,
                    "wisdom" => MinionEnums.TYPE.WISDOM,
                    _ => throw new ArgumentOutOfRangeException
                    (nameof(MinionEnums.TYPE), "Not expected minionType value: " + data["type"])
                };
                
                baseData.grade = data["grade"].ToString() switch
                {
                    "A" => MinionEnums.GRADE.A,
                    "B" => MinionEnums.GRADE.B,
                    "C" => MinionEnums.GRADE.C,
                    _ => throw new ArgumentOutOfRangeException
                        (nameof(MinionEnums.GRADE), "Not expected minionGrade value: " + data["grade"])
                };
            }
            catch(ArgumentOutOfRangeException ex)
            {
                Debug.LogError(ex.Message);
            }
            
            baseData.loyalty = (int)data["loyalty"];
            baseData.stamina = (int)data["stamina"];
            baseData.speed = (int)data["speed"];
            baseData.efficiency = (int)data["efficiency"];
            baseData.sGemProb = (int)data["specialGemProb"];

            return baseData;
        }
        
        Debug.LogError($"Not expected minionID value: {_mid}");
        return baseData;
    }

    /// <summary>
    /// 매개변수로 받은 타입과 등급의 미니언 아이디 리스트 반환
    /// </summary>
    public List<string> FindMinions(MinionEnums.TYPE _type, MinionEnums.GRADE _grade)
    {
        var idList = new List<string>();

        string type = _type switch
        {
            MinionEnums.TYPE.PASSION => "passion",
            MinionEnums.TYPE.CALM => "calm",
            MinionEnums.TYPE.WISDOM => "wisdom",
            _ => "unknown"
        };

        string grade = _grade switch
        {
            MinionEnums.GRADE.A => "A",
            MinionEnums.GRADE.B => "B",
            MinionEnums.GRADE.C => "C",
            _ => "unknown"
        };
        
        foreach (var data in minionTable
                     .Where(_data => _data["type"].ToString() == type && 
                                                         _data["grade"].ToString() == grade))
        {
            idList.Add(data["mid"].ToString());
        }
        
        return idList;
    }
}





