using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShopTable : MonoBehaviour
{

    public static ShopTable Instance;

    private void Awake()
    {
        if (Instance != null && Instance != this)
        {
            Destroy(this.gameObject);
        }
        else
        {
            Instance = this;
        }
    }

    //private Dictionary<string, object> shopCSV;


    public ShopItem GetData(string _itemName)
    {
        //shopCSV = CSVReader.Read("ShopCSV2");
        var itemData = new ShopItem();

        return itemData;
    }

    public struct ShopItem
    {
        public string itemName;
        public int price;
        public ItemType itemType;
        public GemType gemType;
        public Sprite itemImg;
        public int maxDailyPurchase;
        public int maxTotalPurchase;
        public bool isUnlimited;
    }

    public enum ItemType
    {
        WALL,
        FLOOR,
        WALLFLOOR,
        CEILING,
        SLOT,
        NONE
    }

    public enum GemType
    {
        NORMAL,
        SPECIAL
    }
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
