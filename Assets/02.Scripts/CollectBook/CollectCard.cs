using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using System;

public class CollectCard : MonoBehaviour
{
    private string cardName;
    private string cardId;
    public Image cardImg;
    public Image cardLvImg;

    public Text nameText;
    //public TMP_Text typeText;
    //public TMP_Text gradeText;
    public Text levelText;

    public void Setup(Minion _minion)
    {
        nameText.text = _minion.Data.name;
        
        //typeText.text = minion.Data.type.ToString();
        //gradeText.text = minion.Data.grade.ToString();
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
}
