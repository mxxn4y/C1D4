using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Produce
{
    private static int todayGem;
    private static int todaySpecialGem;

    public static void ResetTodayGem()
    {
        todayGem = 0;
        todaySpecialGem = 0;
    }
    public static void AddGem(int _gem)
    {
        todayGem += _gem;
        WorkSceneManager.Instance.SetGemText(todayGem.ToString());
    }
    public static void AddSpecialGem()
    {
        todaySpecialGem++;
        WorkSceneManager.Instance.SetSpecialGemText(todaySpecialGem.ToString());
    }
}
