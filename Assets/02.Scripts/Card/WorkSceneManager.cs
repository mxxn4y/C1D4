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

    public Dictionary<string, int> tempPlayerCards = new Dictionary<string, int>();//테스트용 플레이어 덱
    private int[] tempSlotLimit = { 8, 8, 8 }; //테스트용 슬롯 한도
    private int[] tempGainAmount = { 5, 5, 5 };//테스트용 1회당 획득하는 카드 개수
    [SerializeField] private GameObject[] buttons; //카드 뽑기 버튼

    #endregion

    #region Fields and Properties

    [SerializeField] private Card cardPrefab; //각자 다른 CardData 가지고 복사될 프리팹
    [SerializeField] private GameObject cardCanvas; //플레이어가 가진 카드 생성될 캔버스
    [SerializeField] private Image fileImage;
    [SerializeField] private Image[] indexButtons;
    [SerializeField] private Text[] gemTexts;
    Color passionColor = new Color(255/255f, 136f / 255f, 252f / 255f);
    Color calmColor = new Color(248f / 255f, 224f / 255f, 120f / 255f);
    Color wisdomColor = new Color(139f / 255f, 215f / 255f, 253f / 255f); 
    private List<GameObject> displayedCards = new List<GameObject>(); //현재 캔버스에 존재하는 카드 객체 리스트


    #endregion

    #region Methods

    private void Start()
    {
        tempPlayerCards = SortDeck(tempPlayerCards);
        LoadPassionCards();
    }

    public void LoadPassionCards()
    {
        SetFileUI(CARD_TYPE.PASSION);
        DestroyAllCards();
        foreach (var card in tempPlayerCards)
        {
            if (card.Key.StartsWith('P'))
            {
                var newCard = Instantiate(cardPrefab, cardCanvas.transform); // 카드 생성
                newCard.SetCard(CardTable.Instance.GetData(card.Key), card.Value); //생성한 카드에 데이터 넣기
                displayedCards.Add(newCard.gameObject);
            }
        }
    }

    public void LoadCalmCards()
    {
        SetFileUI(CARD_TYPE.CALM);
        DestroyAllCards();
        foreach (var card in tempPlayerCards)
        {
            if (card.Key.StartsWith('C'))
            {
                var newCard = Instantiate(cardPrefab, cardCanvas.transform); // 카드 생성
                newCard.SetCard(CardTable.Instance.GetData(card.Key), card.Value); //생성한 카드에 데이터 넣기
                displayedCards.Add(newCard.gameObject);
            }
        }
    }

    public void LoadWisdomCards()
    {
        SetFileUI(CARD_TYPE.WISDOM);
        DestroyAllCards();
        foreach (var card in tempPlayerCards)
        {
            if (card.Key.StartsWith('W'))
            {
                var newCard = Instantiate(cardPrefab, cardCanvas.transform); // 카드 생성
                newCard.SetCard(CardTable.Instance.GetData(card.Key), card.Value); //생성한 카드에 데이터 넣기
                displayedCards.Add(newCard.gameObject);
            }
        }
    }

    private void SetFileUI(CARD_TYPE _type)
    {
        switch (_type)
        {
            case CARD_TYPE.PASSION:;
                indexButtons[0].color = passionColor;
                fileImage.color = indexButtons[0].color;
                indexButtons[1].color = Color.gray;
                indexButtons[2].color = Color.gray;
                break;
            case CARD_TYPE.CALM:
                indexButtons[1].color = wisdomColor;
                fileImage.color = indexButtons[1].color;
                indexButtons[0].color = Color.gray;
                indexButtons[2].color = Color.gray;
                break;
            case CARD_TYPE.WISDOM:
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

    private Dictionary<string,int> SortDeck(Dictionary<string, int> dic)
    {
        return dic.OrderBy(item => item.Key).ToDictionary(x => x.Key, x => x.Value);
    }

    //플레이어가 획득 가능한 카드 개수 -> 나중에 인벤토리로 이동하는게 맞을지도
    private int GainableNum(CARD_TYPE _type)
    {
        int availableNum = 0;
        int gainAmount = 0;

        switch (_type)
        {
            case CARD_TYPE.PASSION:
                availableNum = tempSlotLimit[0] - CountTypeNum(_type);
                gainAmount = tempGainAmount[0];
                break;
            case CARD_TYPE.CALM:
                availableNum = tempSlotLimit[1] - CountTypeNum(_type);
                gainAmount = tempGainAmount[1];
                break;
            case CARD_TYPE.WISDOM:
                availableNum = tempSlotLimit[2] - CountTypeNum(_type);
                gainAmount = tempGainAmount[2];
                break;
        }
        Debug.Log("획득 가능한 카드 개수: " + Mathf.Min(availableNum, gainAmount));
        return Mathf.Min(availableNum, gainAmount);
    }

    //플레이어 덱에 존재하는 특정 타입의 개수 반환 -> 나중에 인벤토리로 이동하는게 맞을지도
    private int CountTypeNum(CARD_TYPE _type)
    {
        int num = 0;

        switch (_type)
        {
            case CARD_TYPE.PASSION:
                foreach (var card in tempPlayerCards)
                {
                    if (card.Key.StartsWith('P'))
                    {
                        num += card.Value;
                    }
                }
                break;
            case CARD_TYPE.CALM:
                foreach (var card in tempPlayerCards)
                {
                    if (card.Key.StartsWith('C'))
                    {
                        num += card.Value;
                    }
                }
                break;
            case CARD_TYPE.WISDOM:
                foreach (var card in tempPlayerCards)
                {
                    if (card.Key.StartsWith('W'))
                    {
                        num += card.Value;
                    }
                }
                break;
        }
        return num;
    }

    //플레이어 덱에 매개변수로 받은 카드 1개 추가 -> 나중에 인벤토리로 이동하는게 맞을지도
    private void AddCard(string _cid)
    {
        Debug.Log("획득한 카드: " +  _cid);
        if (tempPlayerCards.ContainsKey(_cid))
        {
            tempPlayerCards[_cid]++;
        }
        else
        {
            tempPlayerCards.Add(_cid, 1);
        }
    }

    //플레이어가 고른 속성의 카드들 플레이어 덱에 추가
    private void AddCardsToDeck(CARD_TYPE _type)
    {
        var aCards = CardTable.Instance.GetAllCards(_type, CARD_GRADE.A);
        var bCards = CardTable.Instance.GetAllCards(_type, CARD_GRADE.B);
        var cCards = CardTable.Instance.GetAllCards(_type, CARD_GRADE.C);

        var amount = GainableNum(_type);

        for (int i = 0; i < amount; i++)
        {
            System.Random random = new System.Random(Guid.NewGuid().GetHashCode());
            var grade = ChooseGrade();
            switch (grade)
            {
                case CARD_GRADE.A:
                    AddCard(aCards[random.Next(0, aCards.Count)]);
                    break;
                case CARD_GRADE.B:
                    AddCard(bCards[random.Next(0, bCards.Count)]);
                    break;
                case CARD_GRADE.C:
                    AddCard(cCards[random.Next(0, cCards.Count)]);
                    break;
            }
        }
    }

    public void SelectPassionButton()
    {
        AddCardsToDeck(CARD_TYPE.PASSION);
        tempPlayerCards = SortDeck(tempPlayerCards);
    }
    public void SelectWisdomButton()
    {
        AddCardsToDeck(CARD_TYPE.WISDOM);
        tempPlayerCards = SortDeck(tempPlayerCards);
    }
    public void SelectCalmButton()
    {
        AddCardsToDeck(CARD_TYPE.CALM);
        tempPlayerCards = SortDeck(tempPlayerCards);
    }
    //등급별 획득 확률을 적용해서 등급 A,B,C 중 랜덤 선택 -> 나중에 인벤토리로 이동하는게 맞을지도
    private CARD_GRADE ChooseGrade()
    {
        System.Random random = new System.Random(Guid.NewGuid().GetHashCode());
        var chance = random.Next(1, 101);
        if ( chance <= 10)
        {
            return CARD_GRADE.A;
        }
        else if( chance <= 30)
        {
            return CARD_GRADE.B;
        }
        else
        {
            return CARD_GRADE.C;
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
