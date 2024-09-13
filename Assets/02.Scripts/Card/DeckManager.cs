using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

/// <summary>
/// �÷��̾ ���� ī�� ���� ���� �� ����,
/// ī�� ���� �̱�, ī�� ��ġ�ϸ� �÷��̾� ������ �����
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
            DontDestroyOnLoad(this.gameObject);
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

    [SerializeField] private Dictionary<string,int> _sampleCards = new Dictionary<string, int>();//�׽�Ʈ�� �÷��̾� ��

    [SerializeField] private Card _cardPrefab; //���� �ٸ� CardData ������ ����� ������

    [SerializeField] private GameObject _cardCanvas; //ī�尡 �׷��� ĵ����


    #endregion

    #region Methods

    private void Start()
    {
        _sampleCards.Add("C001", 2);
        _sampleCards.Add("C005", 2);
        _sampleCards.Add("W001", 2);
        _sampleCards.Add("P005", 7);
        _sampleCards.Add("P001", 2);
        _sampleCards.Add("P004", 1);
        _sampleCards.Add("P003", 4);

        _sampleCards = SortDeck(_sampleCards);
        LoadPassionCards();

    }

    private void LoadPassionCards()
    {
        foreach (var card in _sampleCards)
        {
            if (card.Key.StartsWith('P'))
            {
                Card cardObject = Instantiate(_cardPrefab, _cardCanvas.transform); // ī�� ����
                cardObject.SetCard(CardTable.Instance.GetData(card.Key)); //������ ī�忡 ������ �ֱ�
                cardObject.GetComponent<CardUI>().ActivateStacking(card.Value); //����ŷ ǥ�� ����
            }
        }
    }

    private void LoadCalmCards()
    {
        foreach (var card in _sampleCards)
        {
            if (card.Key.StartsWith('C'))
            {
                Card cardObject = Instantiate(_cardPrefab, _cardCanvas.transform); // ī�� ����
                cardObject.SetCard(CardTable.Instance.GetData(card.Key)); //������ ī�忡 ������ �ֱ�
                cardObject.GetComponent<CardUI>().ActivateStacking(card.Value); //����ŷ ǥ�� ����
            }
        }
    }

    private void LoadWisdomCards()
    {
        foreach (var card in _sampleCards)
        {
            if (card.Key.StartsWith('W'))
            {
                Card cardObject = Instantiate(_cardPrefab, _cardCanvas.transform); // ī�� ����
                cardObject.SetCard(CardTable.Instance.GetData(card.Key)); //������ ī�忡 ������ �ֱ�
                cardObject.GetComponent<CardUI>().ActivateStacking(card.Value); //����ŷ ǥ�� ����
            }
        }
    }

    private Dictionary<string,int> SortDeck(Dictionary<string, int> dic)
    {
        return dic.OrderBy(item => item.Key).ToDictionary(x => x.Key, x => x.Value);
    }


    public void PlaceCard(Card card)
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
                card.GetComponent<CardUI>().ActivateStacking(_sampleCards[card._data._cid]);
            }
            
        }

    }

    //�÷��̾ ���� �� �ִ� ���� ī�� ����
    private void RandomCard()
    {

    }

    //�÷��̾ ���� ī�� �÷��̾� ���� �߰�
    private void AddCardToPlayerDeck()
    {

    }



    //ī�� ���� -> ������ �������� �÷��̾�� ī�� �� ��
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