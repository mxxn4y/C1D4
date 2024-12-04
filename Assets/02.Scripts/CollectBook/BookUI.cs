using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

public class BookUI : MonoBehaviour
{
    public static BookUI Instance { get; private set; }

    public GameObject bookUI;
    public GameObject unlockCard;
    public GameObject lockCard;
    public GameObject[] gridLayouts; // GridLayoutGroup�� ������ ������Ʈ �迭
    public GameObject closeButton;
    //private List allMinions;
    //private List passionMinions;
    //private List calmMinions;
    //private List wisdomMinions;

    private PlayerData playerData;
    public int cardsPerGrid = 4;
    // Start is called before the first frame update

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
        }
        else
        {
            Destroy(gameObject);
        }
    }

    void Start()
    {

        DisplayMinionsByType(MinionEnums.TYPE.PASSION);
    }

    // Update is called once per frame
    void Update()
    {
        if(PlayerData.Instance.SelectedMinions.Count == 3)
        {
            closeButton.SetActive(true);
        }
        else if(PlayerData.Instance.SelectedMinions.Count < 3)
        {
            closeButton.SetActive(false);
        }
    }

    void OnEnable()
    {
        
    }

        public void DisplayMinionsByType(MinionEnums.TYPE type)
    {
        
        // ���� ī�� ����
        foreach (GameObject grid in gridLayouts)
        {
            foreach (Transform child in grid.transform)
            {
                Destroy(child.gameObject); // grid ������ �ڽ� ������Ʈ�� ����
            }
        }

        // ��� �̴Ͼ�� �÷��̾� ������ ��������
        var stringAllList = MinionTable.Instance.FindAllMinions(type);
        var minionAllList = MinionTable.Instance.AllMinionList(stringAllList); // type�� ���� ��� �̴Ͼ� ȣ��
        var ownMinionList = PlayerData.Instance.MinionList; //�÷��̾ ������ �̴Ͼ� ����Ʈ
        var ownedMinionIds = ownMinionList.Select(minion => minion.Data.mid).ToHashSet(); // ������ �̴Ͼ� ID ����
        var selectedMinions = PlayerData.Instance.SelectedMinions; // ���õ� �̴Ͼ� ����Ʈ

    

        int currentMinionIndex = 0;

        foreach (GameObject grid in gridLayouts)
        {
            int cardsPerGrid = grid == gridLayouts[0] ? 4 : 1; // ù ��° �׸���: 4��, �� ��° �׸���: 1��

            for (int i = 0; i < cardsPerGrid; i++)
            {
                if (currentMinionIndex >= minionAllList.Count)
                    break;

                var minion = minionAllList[currentMinionIndex];
                GameObject card;

                // ī�� ���� �� ���� ����
                if (ownedMinionIds.Contains(minion.Data.mid)) // ������ �̴Ͼ��� ���
                {
                    card = Instantiate(unlockCard, grid.transform);
                    var collectCard = card.GetComponent<CollectCard>();
                    collectCard.Setup(minion);
                    collectCard.SetCollectCardImg(minion.Data);
                    collectCard.SetColorImg(minion);
                    collectCard.IsUnlockCard = true;

                    bool exsists = false;

                    foreach(var m in selectedMinions)
                    {
                        if(m.Data.mid == minion.Data.mid)
                        {
                            exsists = true;
                            continue;
                        }
                    }
                    selectedMinions.ForEach(_ => _.Equals(minion.Data.mid));
                    // ���� ���� �ݿ�
                    if (exsists)
                    {
                        collectCard.SetClickImg(); // ���õ� ���·� ������
                        Debug.Log("BookUI�� Ŭ��img ȣ���");
                    }
                    else
                    {
                        collectCard.SetUnClickImg(); // ���õ��� ���� ���·� ������
                        Debug.Log("BookUI�� ��Ŭ��img ȣ���");
                    }
                }
                else // �������� ���� �̴Ͼ��� ���
                {
                    card = Instantiate(lockCard, grid.transform);
                    var collectCard = card.GetComponent<CollectCard>();
                    collectCard.Setup(minion);
                    collectCard.SetCollectCardImg(minion.Data);
                    collectCard.SetColorImg(minion);
                    collectCard.IsUnlockCard = false;
                }

                currentMinionIndex++; // ���� �̴Ͼ����� �̵�
            }
        }
    }

    public void RandomCard()
    {
        var selectedCards = PlayerData.Instance.SelectedMinions;
        var ownCards = PlayerData.Instance.MinionList;
        if (selectedCards.Count > 0)
        {
            Debug.Log("�ּ� 1�� �̻� ����.���� ī�� �������� ����");
        }
        else
        {
            Debug.Log("���� ī�� �̱� ����");

            List<Minion> availableRandom = new List<Minion>();
            System.Random rand = new System.Random();
            while (availableRandom.Count < 3 && ownCards.Count > 0)
            {
                int index = rand.Next(ownCards.Count); // ���� �ε��� ����
                Minion randomMinion = ownCards[index];

                if (!availableRandom.Contains(randomMinion))
                {
                    availableRandom.Add(randomMinion);
                }
            }

            // ������ ���� ī�� ������ PlayerData�� �߰�
            foreach (var minion in availableRandom)
            {
                selectedCards.Add(minion);
                Debug.Log($"�������� ���õ� ī��: �̸�={minion.Data.name}, ID={minion.Data.mid}");
            }
        }
    }

    public void CloseBookUI()
    {
        bookUI.SetActive(false);
    }
}
