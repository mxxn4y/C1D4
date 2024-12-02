using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CardProperty : MonoBehaviour
{
    public Text NameText;
    public Text LoyaltyText;
    public Text StaminaText;
    public Text SpeedText;
    public Text EfficiencyText;
    public Text sGemProb;

    public GameObject cardPropertyPrefab;
    private GameObject currentCardProperty;

    private void SetCardProperty(Minion _minion)
    {
        NameText.text = _minion.Data.name;
        LoyaltyText.text = _minion.Data.loyalty.ToString();
        StaminaText.text = _minion.Data.stamina.ToString();
        SpeedText.text = _minion.Data.speed.ToString();
        EfficiencyText.text = _minion.Data.efficiency.ToString();
        sGemProb.text = _minion.Data.sGemProb.ToString();
    }

    public void DisplayCardProperty(Minion minionData)
    {
        if (currentCardProperty != null)
        {
            Destroy(currentCardProperty);
        }

        currentCardProperty = Instantiate(cardPropertyPrefab, transform);
        CardProperty cardProperty = currentCardProperty.GetComponent<CardProperty>();

        if (cardProperty != null)
        {
            cardProperty.SetCardProperty(minionData);
        }
    }
}

