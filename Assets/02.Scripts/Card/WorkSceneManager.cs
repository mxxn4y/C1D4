using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;
using UnityEngine.UI;
using System;

/// <summary>
/// ī�� ����, �Ӽ����� �ε�, ���� �̱�
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

    public Dictionary<string, int> tempPlayerCards = new Dictionary<string, int>();//�׽�Ʈ�� �÷��̾� ��
    private int[] tempSlotLimit = { 8, 8, 8 }; //�׽�Ʈ�� ���� �ѵ�
    private int[] tempGainAmount = { 5, 5, 5 };//�׽�Ʈ�� 1ȸ�� ȹ���ϴ� ī�� ����
    [SerializeField] private GameObject[] buttons; //ī�� �̱� ��ư

    #endregion

    #region Fields and Properties

    [SerializeField] private Card cardPrefab; //���� �ٸ� CardData ������ ����� ������
    [SerializeField] private GameObject cardCanvas; //�÷��̾ ���� ī�� ������ ĵ����
    [SerializeField] private Image fileImage;
    [SerializeField] private Image[] indexButtons;
    [SerializeField] private Text[] gemTexts;
    Color passionColor = new Color(255/255f, 136f / 255f, 252f / 255f);
    Color calmColor = new Color(248f / 255f, 224f / 255f, 120f / 255f);
    Color wisdomColor = new Color(139f / 255f, 215f / 255f, 253f / 255f); 
    private List<GameObject> displayedCards = new List<GameObject>(); //���� ĵ������ �����ϴ� ī�� ��ü ����Ʈ


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
                var newCard = Instantiate(cardPrefab, cardCanvas.transform); // ī�� ����
                newCard.SetCard(CardTable.Instance.GetData(card.Key), card.Value); //������ ī�忡 ������ �ֱ�
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
                var newCard = Instantiate(cardPrefab, cardCanvas.transform); // ī�� ����
                newCard.SetCard(CardTable.Instance.GetData(card.Key), card.Value); //������ ī�忡 ������ �ֱ�
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
                var newCard = Instantiate(cardPrefab, cardCanvas.transform); // ī�� ����
                newCard.SetCard(CardTable.Instance.GetData(card.Key), card.Value); //������ ī�忡 ������ �ֱ�
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
    /// �����ϴ� ��� ī�� ��ü ����
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

    //�÷��̾ ȹ�� ������ ī�� ���� -> ���߿� �κ��丮�� �̵��ϴ°� ��������
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
        Debug.Log("ȹ�� ������ ī�� ����: " + Mathf.Min(availableNum, gainAmount));
        return Mathf.Min(availableNum, gainAmount);
    }

    //�÷��̾� ���� �����ϴ� Ư�� Ÿ���� ���� ��ȯ -> ���߿� �κ��丮�� �̵��ϴ°� ��������
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

    //�÷��̾� ���� �Ű������� ���� ī�� 1�� �߰� -> ���߿� �κ��丮�� �̵��ϴ°� ��������
    private void AddCard(string _cid)
    {
        Debug.Log("ȹ���� ī��: " +  _cid);
        if (tempPlayerCards.ContainsKey(_cid))
        {
            tempPlayerCards[_cid]++;
        }
        else
        {
            tempPlayerCards.Add(_cid, 1);
        }
    }

    //�÷��̾ �� �Ӽ��� ī��� �÷��̾� ���� �߰�
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
    //��޺� ȹ�� Ȯ���� �����ؼ� ��� A,B,C �� ���� ���� -> ���߿� �κ��丮�� �̵��ϴ°� ��������
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
        gemTexts[0].text = "�Ϲ� ��ȭ: " + _string;
    }

    public void SetSpecialGemText(string _string)
    {
        gemTexts[1].text = "Ư�� ��ȭ: " + _string;
    }

    #endregion
}
