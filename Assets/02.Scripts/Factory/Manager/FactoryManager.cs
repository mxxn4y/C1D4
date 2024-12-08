using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System;
using System.Linq;
using UnityEngine.Serialization;

public class FactoryManager : MonoSingleton<FactoryManager>
{
    #region Fields and Properties

    [SerializeField] private GameObject cardPrefab; //각자 다른 CardData 가지고 복사될 프리팹
    [SerializeField] private GameObject cardCanvas; //플레이어가 가진 카드 생성될 캔버스
    [SerializeField] private Text[] gemTexts;
    [SerializeField] private GameObject factoryCanvas;
    [SerializeField] private GameObject randomDrawUI;
    [SerializeField] private GameObject settlementUI;
    [SerializeField] private GameObject factorySceneUI;
    [SerializeField] private Text[] settlementTexts;
    [SerializeField] private GameObject minionUIs;
    [SerializeField] private Image[] feverObjs;
    
    //테스트용 moveScene
    [SerializeField] private GameObject moveScene;
    
    private List<GameObject> displayedCards = new List<GameObject>(); //현재 캔버스에 존재하는 카드 객체 리스트
    private List<GameObject> displayedTiles = new List<GameObject>();
    public List<MinionController> ActiveMinionList { get; set; } = new List<MinionController>();
    public bool IsStart { get; private set; }
    private float workTime; //제한시간 3분(180초)
    
    [SerializeField] private Text timeText;

    private int todayGem;
    private int todaySpecialGem;
    private int workMin;
    private int workSec;
    
    private bool[] feverArray;
    private readonly Color f1Color = new Color(255f/255f,83f/255f,93f/255f);
    private readonly Color f2Color = new Color(255f/255f,183f/255f,31f/255f);
    private readonly Color f3Color = new Color(255f/255f,215f/255f,123f/255f);
    private readonly Color f4Color = new Color(72f/255f,235f/255f,105f/255f);
    private readonly Color f5Color = new Color(0f,100f/255f,255f/255f);
    #endregion

    #region Methods

    private void Start()
    {
        Init();
    }

    protected override void Init()
    {
        IsStart = false;
        workTime = 180;
        todayGem = 0;
        todaySpecialGem = 0;
        gemTexts[0].text = $"gem: {todayGem.ToString()}";
        gemTexts[1].text = $"s_gem: {todaySpecialGem.ToString()}";
        SetTimeText();
        factorySceneUI.SetActive(true);
        randomDrawUI.SetActive(true);
        factoryCanvas.SetActive(false);
        settlementUI.SetActive(false);
        CardPlaceManager.Instance.OnCardPlace += StartWork;
        feverArray = new bool[5];
        ResetFeverList(); // feverList 모두 false로 초기화
    }
    private void Update()
    {
        if (!IsStart)
        {
            return;
        }

        workTime -= Time.deltaTime;
        if(workTime > 0)
        {
            SetTimeText();
        }
        else
        {
            IsStart = false;
            AudioManager.Instance.StopAudio("04.b_AMwork");
            timeText.text = "time over";
            DestroyAllCards();
            foreach (MinionController minion in ActiveMinionList)
            {
                minion.TimeEnd();
                ShowTodaySettlement();
            }
        }
    }

    private void SetTimeText()
    {
        workMin = (int)workTime / 60;
        workSec = (int)workTime % 60;
        timeText.text = $"{workMin:D2}m {workSec:D2}s";
    }
    
    /// <summary>
    /// 공장씬에 필요한 UI 로드, 플레이어가 가진 미니언을 불러와 카드로 만들어 화면에 표시
    /// </summary>
    public void ShowFactoryUI()
    {
        factoryCanvas.SetActive(true);
        displayedTiles = TileLoadManager.Instance.LoadAllTiles();
        foreach (Minion minion in PlayerData.Instance.SelectedMinions)
        {
            GameObject newCard = Instantiate(cardPrefab, cardCanvas.transform); // 카드 생성
            newCard.GetComponent<CardController>().Set(minion);
            displayedCards.Add(newCard.gameObject);
        }

        bool scrollBtnActive = PlayerData.Instance.SelectedMinions.Count > 3;
        CardPlaceManager.Instance.SetScrollUpDownBtn(scrollBtnActive);
    }
    
    private void StartWork()
    {
        IsStart = true;
        CardPlaceManager.Instance.OnCardPlace -= StartWork;
        AudioManager.Instance.PlayAudio("04.b_AMwork", true, SoundType.BGM);
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
    public void AddSpecialGem(int _amount = 1)
    {
        todaySpecialGem += _amount;
        gemTexts[1].text = $"s_gem: {todaySpecialGem.ToString()}";
    }

    private void ShowTodaySettlement()
    {
        settlementUI.SetActive(true);
        settlementTexts[0].text = todayGem.ToString();
        settlementTexts[1].text = todaySpecialGem.ToString();
    }
    
    public void FeverEventClicked(int _feverIndex)
    {
        feverArray[_feverIndex] = true;
        SetFeverUIColor(_feverIndex);
        Debug.Log($"Fever: {_feverIndex} feverList:{string.Concat(feverArray)}");
        if (feverArray.All(_b => _b.Equals(true)))
        {
            AddSpecialGem(5);
            ResetFeverList();
        }
    }

    private void SetFeverUIColor(int _feverIndex)
    {
        feverObjs[_feverIndex].color = _feverIndex switch
        {
            0 => f1Color,
            1 => f2Color,
            2 => f3Color,
            3 => f4Color,
            4 => f5Color
        };
    }
    private void ResetFeverList()
    {
        for (int i = 0; i < feverArray.Length; i++)
        {
            feverArray[i] = false;
            feverObjs[i].color = Color.gray;
        }
    }

    public void EndFactoryScene()
    {
        PlayerData.Instance.AddGems(todayGem, todaySpecialGem);
        foreach (GameObject tile in displayedTiles)
        {
            Destroy(tile);
        }
        foreach (Transform child in minionUIs.transform) 
        {
            Destroy(child.gameObject);
        }
        factorySceneUI.SetActive(false);
        moveScene.SetActive(true);
        AudioManager.Instance.PlayAudio("02.b_lobby", true, SoundType.BGM);
        ActiveMinionList.Clear();
    }

    public void TestStartFactory()
    {
        Init();
        moveScene.SetActive(false);
        randomDrawUI.GetComponent<MinionRandomDraw>().Init();
    }

    #endregion
}
