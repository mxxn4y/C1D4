using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// �̱���
/// ī�� �� ��Ÿ���� Hand Script�� ��ȭ
/// </summary>
public class Deck : MonoBehaviour
{
    #region Fields and Properties
    public static Deck Instance { get; private set; } //singleton

    //�� ���� -> CardCollection
    //�� ������ �߰� ����(����� �÷��̾� ����)
    [SerializeField] private CardCollection _playerDeck; //���� ��
    [SerializeField] private Card _cardPrefab; //���� �ٸ� CardData ������ ����� ������

    [SerializeField] private Canvas _cardCanvas;

    public List<Card> _playerPile = new(); //��ü �÷��̾� ī�� ����
    public List<Card> _workingPile = new(); //��ġ�� ī�� ����

    //[field:SerializeField] public List<Card> HandCards { get; private set; } = new(); //�÷��̾ ���� ī��� ������ ���� ��ũ��Ʈ �ۼ� �����̶� public

    private RectTransform _rectTransform;
    [SerializeField] private Vector3 _cardStartPos = new Vector3(140,-120,0); //ī�� ���� ��ġ ��ġ
    [SerializeField] private int _cardMargin = -210; //ī�� ����
    #endregion

    #region Methods

    private void Awake() 
    {
        //�̱��� ����
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
        InstantiateDeck(); //������ �� �ѹ� �� ����
    }

    private void InstantiateDeck()
    {
        for(int i = 0; i < _playerDeck.CardsInCollection.Count; i++)
        {
            Card card = Instantiate(_cardPrefab, _cardCanvas.transform);
            InitPlaceDeck(card, i);
            card.SetUp(_playerDeck.CardsInCollection[i]); //ī�� ����
            _playerPile.Add(card); //Ȱ��ȭ ���� ī�� �ֱ�
            
        }
        
    }

    private void InitPlaceDeck(Card card , int i)
    {
        _rectTransform = card.GetComponent<RectTransform>();
        Vector3 margin = new Vector3(0, _cardMargin * i, 0);
        _rectTransform.anchoredPosition = _cardStartPos + margin;
    }

    //ī�� ���� -> ������ �������� �÷��̾�� ī�� �� ��
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
