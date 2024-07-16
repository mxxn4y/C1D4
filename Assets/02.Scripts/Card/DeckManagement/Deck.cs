using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// 싱글톤
/// 카드 덱 나타내고 Hand Script랑 조화
/// </summary>
public class Deck : MonoBehaviour
{
    #region Fields and Properties
    public static Deck Instance { get; private set; } //singleton

    //덱 참조 -> CardCollection
    //덱 여러개 추가 가능(현재는 플레이어 덱만)
    [SerializeField] private CardCollection _playerDeck; //샘플 덱
    [SerializeField] private Card _cardPrefab; //각자 다른 CardData 가지고 복사될 프리팹

    [SerializeField] private Canvas _cardCanvas;

    public List<Card> _playerPile = new(); //전체 플레이어 카드 모음
    public List<Card> _workingPile = new(); //배치한 카드 모음

    //[field:SerializeField] public List<Card> HandCards { get; private set; } = new(); //플레이어가 가진 카드는 움직임 관련 스크립트 작성 예정이라 public

    private RectTransform _rectTransform;
    [SerializeField] private Vector3 _cardStartPos = new Vector3(140,-120,0); //카드 시작 배치 위치
    [SerializeField] private int _cardMargin = -210; //카드 간격
    #endregion

    #region Methods

    private void Awake() 
    {
        //싱글톤 선언
        if(Instance == null)
        {
            Instance = this;
        }
        else
        {
            Destroy(gameObject);
        }
    }

    private void Start()
    {
        InstantiateDeck(); //시작할 때 한번 덱 생성
    }

    private void InstantiateDeck()
    {
        for(int i = 0; i < _playerDeck.CardsInCollection.Count; i++)
        {
            Card card = Instantiate(_cardPrefab, _cardCanvas.transform);
            InitPlaceDeck(card, i);
            card.SetUp(_playerDeck.CardsInCollection[i]); //카드 세팅
            _playerPile.Add(card); //활성화 덱에 카드 넣기
            
        }
        
    }

    private void InitPlaceDeck(Card card , int i)
    {
        _rectTransform = card.GetComponent<RectTransform>();
        Vector3 margin = new Vector3(0, _cardMargin * i, 0);
        _rectTransform.anchoredPosition = _cardStartPos + margin;
    }

    //카드 섞기 -> 덱에서 랜덤으로 플레이어에게 카드 줄 때
    private void ShuffleDeck()
    {
        for(int i = _playerPile.Count - 1; i > 0; i--)
        {
            int j = Random.Range(0, i + 1);
            var temp = _playerPile[i];
            _playerPile[i] = _playerPile[j];
            _playerPile[j] = temp;
        }
    }

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



    public void PlaceCard(Card card)
    {
        if (_playerPile.Contains(card))
        {
            _playerPile.Remove(card);
            _workingPile.Add(card);
            card.gameObject.SetActive(false);
        }
    }

    #endregion
}
