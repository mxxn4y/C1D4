using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;
using System.IO;
using System;

public class CSVtoSO : MonoBehaviour
{
    private static string shopCSVPath = "/CSV/ShopCSV.csv"; //Assets에서 CSV폴더 안 ShopCSV파일
    [MenuItem("Create/Generate ShopItem")]

    public static void GenerateShopItem()
    {
        
        string[] allLines = File.ReadAllLines(Application.dataPath + shopCSVPath);
        for (int i = 1; i < allLines.Length; i++) { 
            string[] splitData = allLines[i].Split(',');

            if (splitData.Length != 7)
            {
                Debug.Log(allLines[i] + " Does not have 7 values");
            }

            ShopItemSO item = ScriptableObject.CreateInstance<ShopItemSO>();

            item.itemName = splitData[0];
            item.price = int.Parse(splitData[1]);
            item.itemType = item.StringToItem(splitData[2]);
            item.gemType = item.StringToGem(splitData[3]);
            item.maxDailyPurchase = int.Parse(splitData[4]);
            item.maxTotalPurchase = int.Parse(splitData[5]);
            item.isUnlimited = bool.Parse(splitData[6]);

            AssetDatabase.CreateAsset(item, $"Assets/CSV/{item.itemName}.asset");
            //itemList.Add(item);
        }

        AssetDatabase.SaveAssets();
    }
}
