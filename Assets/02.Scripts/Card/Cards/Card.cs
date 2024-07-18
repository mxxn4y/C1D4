using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// ī�尡 �����̰� ������ �� �� �ִ��� ����, ��� �����Ϳ� �ൿ ����
/// </summary>
[RequireComponent(typeof(CardUI))] 
[RequireComponent(typeof(CardMovement))] 
public class Card : MonoBehaviour
{
    #region Fields and Properties

    [field: SerializeField] public ScriptableCard CardData { get; private set; }

    #endregion

    #region Methods

    //��Ÿ�ӿ� ������ ī�� ������ ����
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
