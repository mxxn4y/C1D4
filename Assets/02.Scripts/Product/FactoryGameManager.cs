using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FactoryGameManager : MonoSingleton<FactoryGameManager>
{    public enum FactoryColor
    {
        WHITE,
        RED,
        YELLOW,
        BLUE,
        ORANGE,
        GREEN,
        PURPLE,
        BLACK,
    }

    public bool[] isPlaces { get; private set; } = { false, false, false };
    public FactoryColor[] machineColors { get; private set; }


    //미니언 배치하면 확인하기
    public void SetPlace(int _number, bool _place)
    {
        isPlaces[_number] = _place;
    }

    //처음에 머신 색상 정하기
    public void SetMachineColor(int _number, FactoryColor _color)
    {
        machineColors[_number] = _color;
    }
}
