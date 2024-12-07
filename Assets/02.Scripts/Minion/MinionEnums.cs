using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MinionEnums
{
    /// <summary>
    /// 카드 속성 PASSION,CALM,WISDOM
    /// </summary>
    public enum TYPE
    {
        PASSION,
        CALM,
        WISDOM
    }

    /// <summary>
    /// 카드 등급 A,B,C
    /// </summary>
    public enum GRADE
    {
        A,
        B,
        C
    }
    
    public enum EVENT
    {
        EXTRA_GEM,
        TRUST,
        FEVER_TIME
    }
}