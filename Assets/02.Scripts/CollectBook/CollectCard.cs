using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using System;

public class CollectCard : MonoBehaviour
{
    public Minion minionData;
    public MinionBaseData minionBase;
    private string cardName;
    private string cardId;
    public Image cardImg;
    public Image cardLvImg;
    public Image cardBackImg;
    public Sprite click;
    public Sprite unclick;
    public bool IsUnlockCard { get; set; }
    public bool isExhausted;
    private int gainCount;

    public Text nameText;
    //public TMP_Text typeText;
    //public TMP_Text gradeText;
    public Text levelText;

    public void Setup(Minion _minion)
    {
        if (_minion == null )
        {
            Debug.Log("Setup 호출 시 전달된 Minion이 null");
            return;
        }
        minionData = _minion;
        minionBase = _minion.Data;
        Debug.Log(minionBase.name);

        nameText.text = _minion.Data.name;
        isExhausted = _minion.Exhaustion;
        gainCount = _minion.GainCount;
        levelText.text = SetTypeText(_minion);
    }

    public void SetCollectCardImg(MinionBaseData _baseData)
    {
        Sprite[] collectCardImgs = Resources.LoadAll<Sprite>("Character/CharacterImage");
        if (0 == collectCardImgs.Length)
        {
            Debug.LogError("보유한 콜랙트북 카드 이미지 없음 imagePath: {collectCardImgs}");
            return;
        }

        foreach (var img in collectCardImgs)
        {
            if (img.name == _baseData.mid)
            {
                cardImg.sprite = img;
                return;
            }
        }

        Debug.LogError($"스프라이트가 없음. imageName : {_baseData.mid}");
    }



    public void SetClickImg()
    {
        cardBackImg.sprite = click;
        Debug.Log("CollectCard 클릭이미지 함수");

    }

    public void SetUnClickImg()
    {
        cardBackImg.sprite = unclick;
        Debug.Log("CollectCard 언클릭이미지 함수");
    }

    public void SetColorImg(Minion _minion)
    {
        if (_minion.Data.type == MinionEnums.TYPE.PASSION)
        {
            cardLvImg.GetComponent<Image>().color = new Color(255/255f, 81 / 255f, 0 / 255f, 255 / 255f);
        }
        else if(_minion.Data.type== MinionEnums.TYPE.CALM)
        {
            cardLvImg.GetComponent<Image>().color = new Color(0 / 255f, 195 / 255f, 255 / 255f, 255 / 255f);
        }
        else
        {
            cardLvImg.GetComponent<Image>().color = new Color(255 / 255f, 201 / 255f, 0 / 255f, 255 / 255f);
        }

    }

    private string SetTypeText(Minion _minion)
    {
        if(_minion.Data.type== MinionEnums.TYPE.PASSION)
        {
            return "열정";
        }
        else if (_minion.Data.type == MinionEnums.TYPE.CALM)
        {
            return "냉철";
        }
        else
        {
            return "지혜";
        }
    }
}
