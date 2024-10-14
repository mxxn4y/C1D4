using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class MapMoveManager
{
    public static int prevWork;
    public enum Room
    {
        HOUSE,
        MORNING,
        AFTERNOON,
        SHOP
    }

    public static void SetMoveOption(int _prevWork)
    {
        switch (_prevWork)
        {
            case (int)Room.HOUSE:
                prevWork = (int)Room.HOUSE;
                break;

            case (int)Room.MORNING:
                prevWork = (int)Room.MORNING;
                break;

            case (int)Room.AFTERNOON:
                prevWork = (int)Room.AFTERNOON;
                break;
        }
    }
}
