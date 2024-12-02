using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System;

public class FactoryManager : MonoSingleton<FactoryManager>
{
    #region Fields and Properties

    [SerializeField] private GameObject cardPrefab; //각자 다른 CardData 가지고 복사될 프리팹
    [SerializeField] private GameObject cardCanvas; //플레이어가 가진 카드 생성될 캔버스
    [SerializeField] private Text[] gemTexts;
    [SerializeField] private GameObject randomDrawUI;
    [SerializeField] private GameObject factoryUI;
    [SerializeField] private GameObject settlementUI;
    [SerializeField] private Text[] settlementTexts;
    
    private List<GameObject> displayedCards = new List<GameObject>(); //현재 캔버스에 존재하는 카드 객체 리스트
    public List<MinionController> ActiveMinionList { get; set; } = new List<MinionController>();
    private bool isStart;
    private float workTime = 10; //제한시간 3분(180초)
    
    [SerializeField] private Text timeText;

    private static int todayGem;
    private static int todaySpecialGem;
    private int workMin;
    private int workSec;
    
    #endregion

    #region Methods

    private void Start()
    {
        isStart = false;
        randomDrawUI.SetActive(true);
        factoryUI.SetActive(false);
        CardPlaceManager.Instance.OnCardPlace += StartWork;
    }
    private void Update()
    {
        if (isStart)
        {
            workTime -= Time.deltaTime;
            if(workTime > 0)
            {
                workMin = (int)workTime / 60;
                workSec = (int)workTime % 60;
                timeText.text = $"{workMin} min {workSec} sec left";
            }
            else
            {
                timeText.text = "time over";
                DestroyAllCards();
                foreach (MinionController minion in ActiveMinionList)
                {
                    minion.TimeEnd();
                    ShowTodaySettlement();
                }
            }
        }
    }
    
    /// <summary>
    /// 공장씬에 필요한 UI 로드, 플레이어가 가진 미니언을 불러와 카드로 만들어 화면에 표시
    /// </summary>
    public void ShowFactoryUI()
    {
        factoryUI.SetActive(true);
        TileLoadManager.Instance.LoadAllTiles();
        foreach (var minion in PlayerData.Instance.MinionList)
        {
            var newCard = Instantiate(cardPrefab, cardCanvas.transform); // 카드 생성
            newCard.GetComponent<CardController>().Set(minion);
            displayedCards.Add(newCard.gameObject);
        }
    }
    
    private void StartWork()
    {
        isStart = true;
        CardPlaceManager.Instance.OnCardPlace -= StartWork;
    }

    /// <summary>
    /// 존재하는 모든 카드 객체 제거
    /// </summary>
    private void DestroyAllCards()
    {
        if (displayedCards == null) return;
        foreach (GameObject cardGo in displayedCards)
        {
            Destroy(cardGo);
        }
        displayedCards.Clear();
    }
    
    public void AddGem(int _gem)
    {
        todayGem += _gem;
        gemTexts[0].text = $"gem: {todayGem.ToString()}";
    }
    public void AddSpecialGem()
    {
        todaySpecialGem++;
        gemTexts[1].text = $"s_gem: {todaySpecialGem.ToString()}";
    }
    
    public void AddSpecialGem(int _amount)
    {
        todaySpecialGem += _amount;
        gemTexts[1].text = $"s_gem: {todaySpecialGem.ToString()}";
    }

    public void ShowTodaySettlement()
    {
        settlementUI.SetActive(true);
        settlementTexts[0].text = todayGem.ToString();
        settlementTexts[1].text = todaySpecialGem.ToString();
    }

    #endregion
}
