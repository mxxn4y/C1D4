using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;
using System.IO;
using System;

public class CSVtoSO : MonoBehaviour
{
    private static string shopCSVPath = "ShopCSV2.txt"; //Assets에서 CSV폴더 안 ShopCSV파일
    [MenuItem("Create/Generate ShopItem")]

    public static void GenerateShopItem()
    {

        //string[] allLines = File.ReadAllLines(Application.dataPath + shopCSVPath);
        StreamReader reader = new StreamReader(Application.dataPath + "/"+"Resources/ShopSO/"+shopCSVPath);

        bool isFinish = false;
        string headerLine = reader.ReadLine();

        while (isFinish == false)
        {
            // ReadLine은 한줄씩 읽어서 string으로 반환하는 메서드
            // 한줄씩 읽어서 data변수에 담으면
            string data = reader.ReadLine(); // 한 줄 읽기

            // data 변수가 비었는지 확인
            if (data == null)
            {
                // 만약 비었다면? 마지막 줄 == 데이터 없음이니
                // isFinish를 true로 만들고 반복문 탈출
                isFinish = true;
                break;
            }

            //for (int i = 1; i < allLines.Length; i++) {
            //string[] splitData = allLines[i].Split(',');

            //if (splitData.Length != 7)
            //{
            //    Debug.Log(allLines[i] + " Does not have 7 values");
            //}

            var splitData = data.Split(',');

            ShopItemSO item = ScriptableObject.CreateInstance<ShopItemSO>();

            item.itemName = splitData[0];
            item.price = int.Parse(splitData[1]);
            item.itemType = item.StringToItem(splitData[2]);
            //item.gemTypeTxt = splitData[3];
            item.gemType = item.StringToGem(splitData[3]);
            item.maxDailyPurchase = int.Parse(splitData[4]);
            item.maxTotalPurchase = int.Parse(splitData[5]);
            item.isUnlimited = bool.Parse(splitData[6]);

            AssetDatabase.CreateAsset(item, $"Assets/Resources/ShopSO/{item.itemName}.asset");
            //itemList.Add(item);
        }

        AssetDatabase.SaveAssets();
    }
}
