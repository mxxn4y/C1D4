using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ChangeCardImg : MonoBehaviour
{
    public Image cardBackImg;
    public Sprite click;
    public Sprite unclick;
  
    // Start is called before the first frame update
    void Start()
    {
        SetUnClickImg();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void SetClickImg()
    {

        cardBackImg.sprite = click;
        Debug.Log("클릭 이미지로 변경");
    }

    public void SetUnClickImg()
    {
   
        cardBackImg.sprite = unclick;
        Debug.Log("언클릭 이미지로 변경");
    }
}
