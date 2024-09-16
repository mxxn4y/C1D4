using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

/// <summary>
/// 플레이어가 가진 카드 업무 시작 시 생성,
/// 카드 랜덤 뽑기, 카드 배치하면 플레이어 덱에서 지우기
/// </summary>
public class DeckManager : MonoBehaviour
{
    #region Singleton

    private static DeckManager instance = null;

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

    public static DeckManager Instance
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


    #region Fields and Properties

    [SerializeField] private Dictionary<string,int> _sampleCards = new Dictionary<string, int>();//테스트용 플레이어 덱

    [SerializeField] private Card _cardPrefab; //각자 다른 CardData 가지고 복사될 프리팹
    [SerializeField] private GameObject _cardCanvas; //플레이어가 가진 카드 생성될 캔버스

    private List<Card> _cardList = new List<Card>(); //현재 화면에 존재하는 카드 객체 리스트

    #endregion

    #region Methods

    private void Start()
    {
        //임시로 넣은 카드
        _sampleCards.Add("C001", 2);
        _sampleCards.Add("C005", 2);
        _sampleCards.Add("W001", 2);
        _sampleCards.Add("P005", 7);
        _sampleCards.Add("P001", 2);
        _sampleCards.Add("P004", 1);
        _sampleCards.Add("P003", 4);
        //임시로 넣은 카드

        _sampleCards = SortDeck(_sampleCards);
        LoadPassionCards();
    }

    private void LoadPassionCards()
    {
        foreach (var card in _sampleCards)
        {
            if (card.Key.StartsWith('P'))
            {
                var newCard = Instantiate(_cardPrefab, _cardCanvas.transform); // 카드 생성
                newCard.SetCard(CardTable.Instance.GetData(card.Key), card.Value); //생성한 카드에 데이터 넣기
                _cardList.Add(newCard);
            }
        }
    }

    private void LoadCalmCards()
    {
        foreach (var card in _sampleCards)
        {
            if (card.Key.StartsWith('C'))
            {
                var newCard = Instantiate(_cardPrefab, _cardCanvas.transform); // 카드 생성
                newCard.SetCard(CardTable.Instance.GetData(card.Key), card.Value); //생성한 카드에 데이터 넣기
                _cardList.Add(newCard);
            }
        }
    }

    private void LoadWisdomCards()
    {
        foreach (var card in _sampleCards)
        {
            if (card.Key.StartsWith('W'))
            {
                var newCard = Instantiate(_cardPrefab, _cardCanvas.transform); // 카드 생성
                newCard.SetCard(CardTable.Instance.GetData(card.Key), card.Value); //생성한 카드에 데이터 넣기
                _cardList.Add(newCard);
            }
        }
    }

    private Dictionary<string,int> SortDeck(Dictionary<string, int> dic)
    {
        return dic.OrderBy(item => item.Key).ToDictionary(x => x.Key, x => x.Value);
    }


    public void UpdatePlayerDeck(Card card)
    {
        if (_sampleCards.ContainsKey(card._data._cid))
        {
            
            if(_sampleCards[card._data._cid] <= 1)
            {
                _sampleCards.Remove(card._data._cid);
                Destroy(card.gameObject);
            }
            else
            {
                _sampleCards[card._data._cid] -= 1;
                card.UpdateCard(_sampleCards[card._data._cid]);
            }
            
        }

    }

    //플레이어가 고를 수 있는 랜덤 카드 띄우기
    private void RandomCard()
    {

    }

    //플레이어가 고른 카드 플레이어 덱에 추가
    private void AddCardToPlayerDeck()
    {

    }



    //카드 섞기 -> 덱에서 랜덤으로 플레이어에게 카드 줄 때
    //private void ShuffleDeck()
    //{
    //    for(int i = _playerPile.Count - 1; i > 0; i--)
    //    {
    //        int j = Random.Range(0, i + 1);
    //        var temp = _playerPile[i];
    //        _playerPile[i] = _playerPile[j];
    //        _playerPile[j] = temp;
    //    }
    //}

    //public void DrawHand(int amount = 5)
    //{
    //    for(int i = 0; i < amount; i++)
    //    {
    //        if (_deckPile.Count <= 0)
    //        {
    //            //_placedPile = _deckPile;
    //            _placedPile.Clear();
    //            ShuffleDeck();
    //        }
    //        else
    //        {
    //            HandCards.Add(_deckPile[0]);
    //            _deckPile[0].gameObject.SetActive(true);
    //            _deckPile.RemoveAt(0);
    //        }
    //    }
    //}




    #endregion
}
