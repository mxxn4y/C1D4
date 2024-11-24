using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CardEvent : MonoBehaviour
{
    public static CardEvent Instance { get; private set; }

    private bool cardPick;
    public GameObject cardProperyImg;

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
        }
        else
        {
            Destroy(gameObject);
        }
    }

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void CardClick()
    {
        Debug.Log("Ä«µå Å¬¸¯µÊ");
        cardPick = true;
    }

    public void CardUnClick()
    {
        cardPick = false;
    }

    public void CardEnter()
    {
        //sInstantiate(cardProperyImg,)
        Vector2 mousePos = Input.mousePosition;
        //Instantiate(cardProperyImg, mousePos.position + (new Vector2(50, 0)));
    }

    public void CardExit()
    {
        Destroy(cardProperyImg);
    }

    public bool IsCard(GameObject _clickedObj)
    {
        return false;
    }
}
