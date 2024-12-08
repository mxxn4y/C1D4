using System.Collections;
using System.Collections.Generic;
using System.Globalization;
using UnityEngine;
using UnityEngine.UI;

public class CardProperty : MonoBehaviour
{
    public Text nameText;
    public Text loyaltyText;
    public Text staminaText;
    public Text speedText;
    public Text EfficiencyText;
    public Text sGemProb;
    public Text count;
    public Text level; //레벨
    public Slider slider;

    public GameObject cardPropertyPrefab;
    private GameObject currentCardProperty;

    private void SetCardProperty(Minion _minion)
    {
        nameText.text = _minion.Data.name;
        loyaltyText.text = _minion.Data.loyalty.ToString();
        staminaText.text = _minion.Data.stamina.ToString();
        speedText.text = _minion.Data.speed.ToString();
        EfficiencyText.text = _minion.Data.efficiency.ToString();
        sGemProb.text = _minion.Data.sGemProb.ToString();
        count.text = _minion.GainCount.ToString()+" / "+ GetSliderMax(_minion).ToString();

    }

    public void UpdateSlider(Minion _minionData)
    {
        slider.value = (float)_minionData.GainCount / GetSliderMax(_minionData);
        Debug.Log("UpdateSlider함수 실행" + slider.value);
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
    public void DisplayCardProperty(Minion minionData)
    {
        //if (currentCardProperty != null)
        //{
            //Destroy(currentCardProperty);
        //}

        //currentCardProperty = Instantiate(cardPropertyPrefab, transform);
        //CardProperty cardProperty = currentCardProperty.GetComponent<CardProperty>();

        //if (cardProperty != null)
        //{
            SetCardProperty(minionData);
           UpdateSlider(minionData);
        //}
    }
}

