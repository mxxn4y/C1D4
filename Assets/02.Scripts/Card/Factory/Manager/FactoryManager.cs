using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System;

public class FactoryManager : MonoBehaviour
{
    #region Singleton

    private static FactoryManager instance = null;

    void Awake()
    {
        if (null == instance)
        {
            instance = this;
        }
        else
        {
            Destroy(this.gameObject);
        }
    }

    public static FactoryManager Instance
    {
        get
        {
            if (null == instance)
            {
                return null;
            }
            return instance;
        }
    }

    #endregion

    #region used for test
    [SerializeField] private GameObject[] buttons; //카드 뽑기 버튼

    #endregion

    #region Fields and Properties

    [SerializeField] private GameObject cardPrefab; //각자 다른 CardData 가지고 복사될 프리팹
    [SerializeField] private GameObject cardCanvas; //플레이어가 가진 카드 생성될 캔버스
    [SerializeField] private Image fileImage;
    [SerializeField] private Image[] indexButtons;
    [SerializeField] private Text[] gemTexts;
    
    private readonly Color passionColor = new (153f /255f, 84f / 255f, 255f / 255f);
    private readonly Color calmColor = new (31f / 255f, 185f / 255f, 248f / 255f);
    private readonly Color wisdomColor = new (149f / 255f, 255f / 255f, 254f / 255f); 
    
    private List<GameObject> displayedCards = new List<GameObject>(); //현재 캔버스에 존재하는 카드 객체 리스트
    public List<MinionController> minions { get; set; } = new List<MinionController>();
    private bool isWorkStart;
    private float workTime = 180; //제한시간 3분(180초)
    
    [SerializeField] private Text timeText;

    private static int todayGem;
    private static int todaySpecialGem;
    private int workMin;
    private int workSec;
    
    #endregion

    #region Methods

    private void Start()
    {
        LoadCards();
        isWorkStart = false;
        CardPlaceManager.Instance.OnCardPlace += StartWork;
    }
    private void Update()
    {
        if (isWorkStart)
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
            }
        }
    }

    /*
    public void LoadPassionCards()
    {
        SetFileUI(MinionEnums.TYPE.PASSION);
        DestroyAllCards();
        foreach (var card in PlayerInfo.playerMinions)
        {
            if (card.Key.StartsWith('P'))
            {
                var newCard = Instantiate(cardPrefab, cardCanvas.transform); // 카드 생성
                newCard.SetCard(MinionTable.Instance.GetData(card.Key), card.Value); //생성한 카드에 데이터 넣기
                displayedCards.Add(newCard.gameObject);
            }
        }
    }

    public void LoadCalmCards()
    {
        SetFileUI(MinionEnums.TYPE.CALM);
        DestroyAllCards();
        foreach (var card in PlayerInfo.playerMinions)
        {
            if (card.Key.StartsWith('C'))
            {
                var newCard = Instantiate(cardPrefab, cardCanvas.transform); // 카드 생성
                newCard.SetCard(MinionTable.Instance.GetData(card.Key), card.Value); //생성한 카드에 데이터 넣기
                displayedCards.Add(newCard.gameObject);
            }
        }
    }

    public void LoadWisdomCards()
    {
        SetFileUI(MinionEnums.TYPE.WISDOM);
        DestroyAllCards();
        foreach (var card in PlayerInfo.playerMinions)
        {
            if (card.Key.StartsWith('W'))
            {
                var newCard = Instantiate(cardPrefab, cardCanvas.transform); // 카드 생성
                newCard.SetCard(MinionTable.Instance.GetData(card.Key), card.Value); //생성한 카드에 데이터 넣기
                displayedCards.Add(newCard.gameObject);
            }
        }
    }
    */

    
    public void LoadCards()
    {
        foreach (var minion in PlayerInfo.MinionList)
        {
            var newCard = Instantiate(cardPrefab, cardCanvas.transform); // 카드 생성
            newCard.GetComponent<CardController>().Set(minion);
            displayedCards.Add(newCard.gameObject);
        }
    }
    public void StartWork()
    {
        isWorkStart = true;
        CardPlaceManager.Instance.OnCardPlace -= StartWork;
    }

    private void SetFileUI(MinionEnums.TYPE _type)
    {
        switch (_type)
        {
            case MinionEnums.TYPE.PASSION:;
                indexButtons[0].color = passionColor;
                fileImage.color = indexButtons[0].color;
                indexButtons[1].color = Color.gray;
                indexButtons[2].color = Color.gray;
                break;
            case MinionEnums.TYPE.CALM:
                indexButtons[1].color = wisdomColor;
                fileImage.color = indexButtons[1].color;
                indexButtons[0].color = Color.gray;
                indexButtons[2].color = Color.gray;
                break;
            case MinionEnums.TYPE.WISDOM:
                indexButtons[2].color = calmColor;
                fileImage.color = indexButtons[2].color;
                indexButtons[0].color = Color.gray;
                indexButtons[1].color = Color.gray;
                break;
        }
    }

    /// <summary>
    /// 존재하는 모든 카드 객체 제거
    /// </summary>
    private void DestroyAllCards()
    {
        if (displayedCards == null) return;
        foreach (var cardGO in displayedCards)
        {
            Destroy(cardGO);
        }
        displayedCards.Clear();
    }

    //플레이어가 고른 속성의 미니언들 뽑기 
    private void AddMinions(MinionEnums.TYPE _type)
    {
        var amount = PlayerInfo.gainNum;

        var aCards = MinionTable.Instance.FindMinions(_type, MinionEnums.GRADE.A);
        var bCards = MinionTable.Instance.FindMinions(_type, MinionEnums.GRADE.B);
        var cCards = MinionTable.Instance.FindMinions(_type, MinionEnums.GRADE.C);
        
        for (int i = 0; i < amount; i++)
        {
            System.Random random = new(Guid.NewGuid().GetHashCode());
            var grade = ChooseGrade();
            switch (grade)
            {
                case MinionEnums.GRADE.A:
                    PlayerInfo.AddMinion(aCards[random.Next(0, aCards.Count)]);
                    break;
                case MinionEnums.GRADE.B:
                    PlayerInfo.AddMinion(bCards[random.Next(0, bCards.Count)]);
                    break;
                case MinionEnums.GRADE.C:
                    PlayerInfo.AddMinion(cCards[random.Next(0, cCards.Count)]);
                    break;
            }
        }
    }

    public void SelectPassionButton()
    {
        AddMinions(MinionEnums.TYPE.PASSION);
    }
    public void SelectWisdomButton()
    {
        AddMinions(MinionEnums.TYPE.WISDOM);
    }
    public void SelectCalmButton()
    {
        AddMinions(MinionEnums.TYPE.CALM);
    }
    //등급별 획득 확률을 적용해서 등급 A,B,C 중 랜덤 선택 -> 나중에 인벤토리로 이동하는게 맞을지도
    private MinionEnums.GRADE ChooseGrade()
    {
        System.Random random = new System.Random(Guid.NewGuid().GetHashCode());
        var chance = random.Next(1, 101);
        if ( chance <= 10)
        {
            return MinionEnums.GRADE.A;
        }
        else if( chance <= 30)
        {
            return MinionEnums.GRADE.B;
        }
        else
        {
            return MinionEnums.GRADE.C;
        }
    }

    public static void ResetTodayGem()
    {
        todayGem = 0;
        todaySpecialGem = 0;
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

    #endregion
}
