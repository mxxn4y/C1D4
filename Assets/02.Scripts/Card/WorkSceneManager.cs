using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WorkSceneManager : MonoBehaviour
{
    #region Singleton

    private static WorkSceneManager instance = null;

    void Awake()
    {
        if (null == instance)
        {
            instance = this;
        }
        else
        {
            Destroy(this.gameObject);
        }
    }

    public static WorkSceneManager Instance
    {
        get
        {
            if (null == instance)
            {
                return null;
            }
            return instance;
        }
    }

    #endregion

    private int _todayMoney;
    private int _todayGem;

    private void Start()
    {
        _todayMoney = 0;
        _todayGem = 0;
    }
    public void AddMoney(int money)
    {
        _todayMoney += money;
        Debug.Log("money:" + _todayMoney);
    }
    public void AddGem(int gem)
    {
        _todayGem += gem;
        Debug.Log("gem: " + _todayGem);
    }
}
