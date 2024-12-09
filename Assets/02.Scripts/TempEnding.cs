using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TempEnding : MonoBehaviour
{
    public Text gemtext;
    public Text s_Gemtext;
    public Text scoretext;
    void OnEnable()
    {
        int gem = PlayerData.Instance.Gem;
        int sGem = PlayerData.Instance.SpecialGem;
        gemtext.text = $"Total Gem: {gem}";
        s_Gemtext.text = $"Total Special Gem: {sGem}";
        scoretext.text = $"Final Score: {gem + sGem * 5}";
    }

}
