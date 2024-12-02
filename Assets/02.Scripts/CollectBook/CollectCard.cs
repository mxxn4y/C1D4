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

    public Text nameText;
    //public TMP_Text typeText;
    //public TMP_Text gradeText;
    public Text levelText;

    public void Setup(Minion _minion)
    {
        if (_minion == null )
        {
            Debug.Log("Setup ȣ�� �� ���޵� Minion�� null");
            return;
        }
        minionData = _minion;
        minionBase = _minion.Data;
        Debug.Log(minionBase.name);

        nameText.text = _minion.Data.name;
        isExhausted = _minion.Exhaustion;
        //bool isSelected = PlayerData.Instance.SelectedMinions.Contains(_minion);

        /*
        if (isSelected)
        {
            SetClickImg(); // Ŭ�� ���·� �ʱ�ȭ
        }
        else
        {
            SetUnClickImg(); // ��Ŭ�� ���·� �ʱ�ȭ
        }
        */

        //typeText.text = minion.Data.type.ToString();
        //gradeText.text = minion.Data.grade.ToString();
    }

    /*
    public Minion GetCardMinion(Minion _minion)
    {
        return minionData;
    }
    */

    public void SetCollectCardImg(MinionBaseData _baseData)
    {
        Sprite[] collectCardImgs = Resources.LoadAll<Sprite>("Character/CharacterImage");
        if (0 == collectCardImgs.Length)
        {
            Debug.LogError("������ �ݷ�Ʈ�� ī�� �̹��� ���� imagePath: {collectCardImgs}");
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

        Debug.LogError($"��������Ʈ�� ����. imageName : {_baseData.mid}");
    }



    public void SetClickImg()
    {
        cardBackImg.sprite = click;
        Debug.Log("CollectCard Ŭ���̹��� �Լ�");

    }

    public void SetUnClickImg()
    {
        cardBackImg.sprite = unclick;
        Debug.Log("CollectCard ��Ŭ���̹��� �Լ�");
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
