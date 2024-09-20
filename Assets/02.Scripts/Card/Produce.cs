using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Produce
{
    private static int _todayMoney;
    private static int _todayGem;

    public static void Reset()
    {
        _todayMoney = 0;
        _todayGem = 0;
    }
    public static void AddMoney(int money)
    {
        _todayMoney += money;
        Debug.Log("money:" + _todayMoney);
    }
    public static void AddGem(int gem)
    {
        _todayGem += gem;
        Debug.Log("gem: " + _todayGem);
    }
}
