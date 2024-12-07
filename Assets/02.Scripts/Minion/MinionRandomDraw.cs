using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class MinionRandomDraw : MonoBehaviour
{
    [SerializeField] private GameObject cardPrefab;
    [SerializeField] private GameObject buttons;
    [SerializeField] private GameObject randDrawResult;
    [SerializeField] private GameObject gridLayout;
    [SerializeField] private GameObject[] emptyCards = new GameObject[5];
    [SerializeField] Animation drawAnimation;
    private PlayerData playerData;
    private int newCardCount;

    private void Awake()
    {
        Init();
    }

    public void Init()
    {
        playerData = PlayerData.Instance;
        buttons.SetActive(true);
        randDrawResult.SetActive(false);
    }

    /// <summary>
    /// 선택된 속성의 미니언 랜덤으로 뽑아 리스트로 반환
    /// </summary>
    private List<string> RandomDraw(MinionEnums.TYPE _type)
    {
        var minionTable = MinionTable.Instance;
        var minions = new List<String>();
        
        for (int i = 0; i < playerData.GainNum; i++)
        {
            System.Random random = new(Guid.NewGuid().GetHashCode());
            var grade = ChooseGrade();
            switch (grade)
            {
                case MinionEnums.GRADE.A:
                    var aCards = minionTable.FindMinions(_type, MinionEnums.GRADE.A);
                    minions.Add(aCards[random.Next(0, aCards.Count)]);
                    break;
                case MinionEnums.GRADE.B:
                    var bCards = minionTable.FindMinions(_type, MinionEnums.GRADE.B);
                    minions.Add(bCards[random.Next(0, bCards.Count)]);
                    break;
                case MinionEnums.GRADE.C:
                    var cCards = minionTable.FindMinions(_type, MinionEnums.GRADE.C);
                    minions.Add(cCards[random.Next(0, cCards.Count)]);
                    break;
            }
        }
        return minions;
    }
    
    /// <summary>
    /// 등급 A, B, C 중 랜덤 선택, 확률(A:10% B:20% C:70%)
    /// </summary>
    private MinionEnums.GRADE ChooseGrade()
    {
        System.Random random = new (Guid.NewGuid().GetHashCode());
        return random.Next(1, 101) switch
        {
            <= 10 => MinionEnums.GRADE.A,
            <= 30 => MinionEnums.GRADE.B,
            _ => MinionEnums.GRADE.C
        };
    }

    /// <summary>
    /// 미니언 해금 + 강화 상태 체크해서 플레이어 데이터에 추가, 얻은 미니언 카드 형태로 화면에 표시
    /// </summary>
    private void AddToPlayerAndShow(List<string> _midList)
    {
        newCardCount = 0;
        foreach (var card in emptyCards)
        {
            card.SetActive(false);
        }
        foreach (var mid in _midList)
        {
            var playerMinions = PlayerData.Instance.MinionList;
            // 플레이어가 이미 소유한 미니언
            if (playerMinions.Exists(_minion => _minion.Data.mid == mid))
            {
                var minion = playerMinions.Find(_minion => _minion.Data.mid == mid);
                // 등급에 따른 최대 강화 횟수
                int maxGainNum = minion.Data.grade switch
                {
                    MinionEnums.GRADE.A => 5,
                    MinionEnums.GRADE.B => 10,
                    _ => 20
                };
                //최대 강화 한도 도달한 상태라면
                if (minion.GainCount >= maxGainNum)
                {
                    Debug.Log($"최대 한도 도달: {mid} -> {minion.GainCount} / {maxGainNum}");
                    // 최대 강화 상태라는 것 표시
                    return;
                }
                
                minion.IncreaseCount(); // 강화 +1 증가
                ShowEarnedCards(minion); // 얻은 미니언 카드 형태로 화면에 표시
                newCardCount++;
                Debug.Log($"미니언 강화 +1: {mid} -> {minion.GainCount} / {maxGainNum}");
            }
            // 플레이어가 처음 획득하는 미니언
            else
            {
                PlayerData.Instance.AddMinion(mid);
                ShowEarnedCards(new Minion(mid)); // 얻은 미니언 카드 형태로 화면에 표시
                newCardCount++;
                Debug.Log($"미니언 해금: {mid} -> " +
                          $"{PlayerData.Instance.MinionList.Find(_minion => _minion.Data.mid == mid).GainCount}");
            }
        }
    }

    private void ShowEarnedCards(Minion _newMinion)
    {
        GameObject card = emptyCards[newCardCount];
        card.SetActive(true);
        card.GetComponent<CardUI>().Set(_newMinion);
    }

    public void CloseRandomDrawUI()
    {
        gameObject.SetActive(false);
        FactoryManager.Instance.ShowFactoryUI();
    }
    
    public void SelectPassionButton()
    {
        List<string> minions = RandomDraw(MinionEnums.TYPE.PASSION);
        AddToPlayerAndShow(minions);
        buttons.SetActive(false);
        randDrawResult.SetActive(true);
    }
    public void SelectWisdomButton()
    {
        List<string> minions = RandomDraw(MinionEnums.TYPE.WISDOM);
        AddToPlayerAndShow(minions);
        buttons.SetActive(false);
        randDrawResult.SetActive(true);
    }
    public void SelectCalmButton()
    {
        var minions = RandomDraw(MinionEnums.TYPE.CALM);
        AddToPlayerAndShow(minions);
        buttons.SetActive(false);
        randDrawResult.SetActive(true);
    }

    // 테스트용!!
    public void TestButton()
    {
        var minions = RandomDraw(MinionEnums.TYPE.PASSION);
        AddToPlayerAndShow(minions);
    }
}