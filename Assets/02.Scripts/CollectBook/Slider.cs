using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CardSlider : MonoBehaviour
{
    public Text Count;
    public Slider slider;
    private int sliderValue;
    private int sliderMax;

    public void SetSliderText(Minion _minionData)
    {
        Count.text = _minionData.GainCount.ToString();
    }

    public void UpdateSlider(Minion _minion)
    {
        slider.value = _minion.GainCount / GetSliderMax(_minion);
        Debug.Log("UpdateSlider함수 실행");
    }

    public int GetSliderMax(Minion _minion)
    {
        if (_minion.Data.grade == MinionEnums.GRADE.A)
            return 20;
        else if (_minion.Data.grade == MinionEnums.GRADE.B)
            return 10;
        else
            return 5;
    }

    // Start is called before the first frame update

}
