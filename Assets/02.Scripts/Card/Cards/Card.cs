using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// 카드가 무엇이고 무엇이 될 수 있는지 정의, 모든 데이터와 행동 연결
/// </summary>
[RequireComponent(typeof(CardUI))] 
[RequireComponent(typeof(CardMovement))] 
public class Card : MonoBehaviour
{
    #region Fields and Properties

    [field: SerializeField] public ScriptableCard CardData { get; private set; }

    #endregion

    #region Methods

    //런타임에 연관된 카드 데이터 설정
    public void SetUp(ScriptableCard data)
    {
        CardData = data;
        GetComponent<CardUI>().SetCardUI();
    }

    public void SetUI(bool moving)
    {
        if (moving)
        {
            GetComponent<CardUI>().SetCardMovingUI();
        }
        else
        {
            GetComponent<CardUI>().SetCardUI();
        }
    }

    #endregion

}
