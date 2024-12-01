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
    public GameObject[] gridLayouts; // GridLayoutGroup이 부착된 오브젝트 배열
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
        //SpawnCards();
        DisplayMinionsByType(MinionEnums.TYPE.PASSION);
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void DisplayMinionsByType(MinionEnums.TYPE type)
    {
        // 기존 카드 삭제
        foreach (GameObject grid in gridLayouts)
        {
            foreach (Transform child in grid.transform)
            {
                Destroy(child.gameObject); // grid 내부의 자식 오브젝트를 삭제
            }
        }
        /*
        // PlayerData에서 미니언 리스트 가져와 필터링
        var filteredMinions = PlayerData.Instance.MinionList
            .Where(minion => minion.Data.type == type)
            .ToList();
        int minionsPerGrid = 4; // 각 grid에 표시할 최대 미니언 개수
        int currentMinionIndex = 0; // 현재 표시된 미니언의 인덱스

        foreach (GameObject grid in gridLayouts)
        {
            for (int i = 0; i < minionsPerGrid; i++)
            {
                if (currentMinionIndex >= filteredMinions.Count)
                {
                    // 더 이상 표시할 미니언이 없으면 반복 종료
                    break;
                }

                // 필터링된 미니언으로 카드 생성
                var minion = filteredMinions[currentMinionIndex];
                GameObject card = Instantiate(unlockCard, grid.transform);
                // 카드에 미니언 정보 연결
                card.GetComponent<CollectCard>().Setup(minion);
                card.GetComponent<CollectCard>().SetCollectCardImg(minion.Data);
                card.GetComponent<CollectCard>().SetColorImg(minion);

                currentMinionIndex++;
            }
        }
        */
        var stringAllList=MinionTable.Instance.FindAllMinions(type);
        var minionAllList = MinionTable.Instance.AllMinionList(stringAllList); // 인덱스 이벤트에서 호출하면 type에 따른 모든 미니언 호출
        //var filteredMinions = PlayerData.Instance.MinionList
        //.Where(minion => minion.Data.type == type)
        //.ToList();

        var ownMinonList = PlayerData.Instance.MinionList;

        var ownedMinionIds = ownMinonList.Select(minion => minion.Data.mid).ToHashSet(); // 보유한 미니언 ID 집합
        var selectedMinion = PlayerData.Instance.SelectedMinions;

        int currentMinionIndex = 0;

        foreach (GameObject grid in gridLayouts)
        {
            int cardsPerGrid = grid == gridLayouts[0] ? 4 : 1; // 첫 번째 그리드: 4개, 두 번째 그리드: 1개

            for (int i = 0; i < cardsPerGrid; i++)
            {
                if (currentMinionIndex >= minionAllList.Count)
                    break;

                var minion = minionAllList[currentMinionIndex];

                GameObject card;
                if (ownedMinionIds.Contains(minion.Data.mid))
                {
                    // 보유한 미니언: unlockCard 프리팹 사용
                    card = Instantiate(unlockCard, grid.transform);
                    card.GetComponent<CollectCard>().Setup(minion);
                    card.GetComponent<CollectCard>().SetCollectCardImg(minion.Data);
                    card.GetComponent<CollectCard>().SetColorImg(minion);
                  
                }
                else
                {
                    // 보유하지 않은 미니언: lockCard 프리팹 사용
                    card = Instantiate(lockCard, grid.transform);
                    card.GetComponent<CollectCard>().Setup(minion);
                    card.GetComponent<CollectCard>().SetCollectCardImg(minion.Data);
                    card.GetComponent<CollectCard>().SetColorImg(minion);

                }

                currentMinionIndex++; // 다음 미니언으로 이동
            }
        }

       
    }

    public void RandomCard()
    {
        var selectedCards = PlayerData.Instance.SelectedMinions;
        var ownCards = PlayerData.Instance.MinionList;
        if (selectedCards.Count > 0)
        {
            Debug.Log("최소 1개 이상 선택.랜덤 카드 선택하지 않음");
        }
        else
        {
            Debug.Log("랜덤 카드 뽑기 진행");

            List<Minion> availableRandom = new List<Minion>();
            System.Random rand = new System.Random();
            while (availableRandom.Count < 3 && ownCards.Count > 0)
            {
                int index = rand.Next(ownCards.Count); // 랜덤 인덱스 생성
                Minion randomMinion = ownCards[index];

                if (!availableRandom.Contains(randomMinion))
                {
                    availableRandom.Add(randomMinion);
                }
            }

            // 선택한 랜덤 카드 정보를 PlayerData에 추가
            foreach (var minion in availableRandom)
            {
                selectedCards.Add(minion);
                Debug.Log($"랜덤으로 선택된 카드: 이름={minion.Data.name}, ID={minion.Data.mid}");
            }
        }
        bookUI.SetActive(false);
    }

    //이거 아님
    public void SpawnCards()
    {
        var unLockCard = PlayerData.Instance.MinionList;
        foreach(var card in unLockCard)
        {
            //Debug.Log(card.mid);
        }

        foreach (GameObject grid in gridLayouts)
        {
            // 지정된 카드 수만큼 생성
            for (int i = 0; i < cardsPerGrid; i++)
            {
                GameObject newCard = Instantiate(unlockCard, grid.transform);
                newCard.name = $"Card_{grid.name}_{i + 1}";
                Debug.Log($"카드 생성됨: {newCard.name}");
            }
        }
    }
}
