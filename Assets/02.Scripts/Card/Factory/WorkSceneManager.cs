using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;
using UnityEngine.UI;
using System;

/// <summary>
/// 카드 정렬, 속성별로 로드, 랜덤 뽑기
/// </summary>
public class WorkSceneManager : MonoBehaviour
{
    #region Singleton

    private static WorkSceneManager instance = null;

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

    public static WorkSceneManager Instance
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

    [SerializeField] private Card cardPrefab; //각자 다른 CardData 가지고 복사될 프리팹
    [SerializeField] private GameObject cardCanvas; //플레이어가 가진 카드 생성될 캔버스
    [SerializeField] private Image fileImage;
    [SerializeField] private Image[] indexButtons;
    [SerializeField] private Text[] gemTexts;
    Color passionColor = new Color(153f /255f, 84f / 255f, 255f / 255f);
    Color calmColor = new Color(31f / 255f, 185f / 255f, 248f / 255f);
    Color wisdomColor = new Color(149f / 255f, 255f / 255f, 254f / 255f); 
    private List<GameObject> displayedCards = new List<GameObject>(); //현재 캔버스에 존재하는 카드 객체 리스트

    public List<Minion> minions { get; set; } = new List<Minion>();
    public bool isWorkStart { get; private set; }
    private float workTime = 180; //제한시간 3분(180초)
    private int workMin;
    private int workSec;
    [SerializeField] private Text timeText;

    #endregion

    #region Methods

    private void Start()
    {
        PlayerInfoManager.SortCards();
        LoadPassionCards();
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
                timeText.text = "남은 시간: " + workMin + " 분 " + workSec + " 초";
            }
            else
            {
                timeText.text = "남은 시간: 0 분 0 초";
            }
        }
    }

    public void LoadPassionCards()
    {
        SetFileUI(MinionEnums.MINION_TYPE.PASSION);
        DestroyAllCards();
        foreach (var card in PlayerInfoManager.playerCards)
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
        SetFileUI(MinionEnums.MINION_TYPE.CALM);
        DestroyAllCards();
        foreach (var card in PlayerInfoManager.playerCards)
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
        SetFileUI(MinionEnums.MINION_TYPE.WISDOM);
        DestroyAllCards();
        foreach (var card in PlayerInfoManager.playerCards)
        {
            if (card.Key.StartsWith('W'))
            {
                var newCard = Instantiate(cardPrefab, cardCanvas.transform); // 카드 생성
                newCard.SetCard(MinionTable.Instance.GetData(card.Key), card.Value); //생성한 카드에 데이터 넣기
                displayedCards.Add(newCard.gameObject);
            }
        }
    }
    public void StartWork()
    {
        isWorkStart = true;
        CardPlaceManager.Instance.OnCardPlace -= StartWork;
    }

    private void SetFileUI(MinionEnums.MINION_TYPE _type)
    {
        switch (_type)
        {
            case MinionEnums.MINION_TYPE.PASSION:;
                indexButtons[0].color = passionColor;
                fileImage.color = indexButtons[0].color;
                indexButtons[1].color = Color.gray;
                indexButtons[2].color = Color.gray;
                break;
            case MinionEnums.MINION_TYPE.CALM:
                indexButtons[1].color = wisdomColor;
                fileImage.color = indexButtons[1].color;
                indexButtons[0].color = Color.gray;
                indexButtons[2].color = Color.gray;
                break;
            case MinionEnums.MINION_TYPE.WISDOM:
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

    //플레이어가 고른 속성의 카드들 플레이어 덱에 추가
    private void AddCardsToDeck(MinionEnums.MINION_TYPE _type)
    {
        var aCards = MinionTable.Instance.GetAllCards(_type, MinionEnums.MINION_GRADE.A);
        var bCards = MinionTable.Instance.GetAllCards(_type, MinionEnums.MINION_GRADE.B);
        var cCards = MinionTable.Instance.GetAllCards(_type, MinionEnums.MINION_GRADE.C);

        var amount = PlayerInfoManager.GainableCardNum(_type);

        for (int i = 0; i < amount; i++)
        {
            System.Random random = new System.Random(Guid.NewGuid().GetHashCode());
            var grade = ChooseGrade();
            switch (grade)
            {
                case MinionEnums.MINION_GRADE.A:
                    PlayerInfoManager.AddCard(aCards[random.Next(0, aCards.Count)]);
                    break;
                case MinionEnums.MINION_GRADE.B:
                    PlayerInfoManager.AddCard(bCards[random.Next(0, bCards.Count)]);
                    break;
                case MinionEnums.MINION_GRADE.C:
                    PlayerInfoManager.AddCard(cCards[random.Next(0, cCards.Count)]);
                    break;
            }
        }
    }

    public void SelectPassionButton()
    {
        AddCardsToDeck(MinionEnums.MINION_TYPE.PASSION);
        PlayerInfoManager.SortCards();
    }
    public void SelectWisdomButton()
    {
        AddCardsToDeck(MinionEnums.MINION_TYPE.WISDOM);
        PlayerInfoManager.SortCards();
    }
    public void SelectCalmButton()
    {
        AddCardsToDeck(MinionEnums.MINION_TYPE.CALM);
        PlayerInfoManager.SortCards();
    }
    //등급별 획득 확률을 적용해서 등급 A,B,C 중 랜덤 선택 -> 나중에 인벤토리로 이동하는게 맞을지도
    private MinionEnums.MINION_GRADE ChooseGrade()
    {
        System.Random random = new System.Random(Guid.NewGuid().GetHashCode());
        var chance = random.Next(1, 101);
        if ( chance <= 10)
        {
            return MinionEnums.MINION_GRADE.A;
        }
        else if( chance <= 30)
        {
            return MinionEnums.MINION_GRADE.B;
        }
        else
        {
            return MinionEnums.MINION_GRADE.C;
        }
    }

    public void SetGemText(string _string)
    {
        gemTexts[0].text = "일반 재화: " + _string;
    }

    public void SetSpecialGemText(string _string)
    {
        gemTexts[1].text = "특수 재화: " + _string;
    }

    #endregion
}
