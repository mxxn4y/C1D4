using System;
using System.Collections.Generic;
using UnityEngine;

public class MinionRandomDraw : MonoBehaviour
{
    [SerializeField] private GameObject cardPrefab;
    [SerializeField] private GameObject[] buttons = new GameObject[3];
    [SerializeField] private GameObject closeBtn;
    private PlayerData playerData;

    private void Awake()
    {
        playerData = PlayerData.Instance;
        foreach (var button in buttons)
        {
            button.SetActive(true);
        }
        closeBtn.SetActive(false);
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
    
    private void ShowEarnedCards(List<string> _minions)
    {
        foreach (var mid in _minions)
        {
            var card = Instantiate(cardPrefab, transform);
            card.GetComponent<CardController>().Set(new Minion(mid));
        }
    }

    public void CloseRandomDrawUI()
    {
        gameObject.SetActive(false);
        FactoryManager.Instance.ShowFactoryUI();
    }
    
    public void SelectPassionButton()
    {
        var minions = RandomDraw(MinionEnums.TYPE.PASSION);
        playerData.AddMinions(minions);
        ShowEarnedCards(minions);
        foreach (var button in buttons)
        {
            button.SetActive(false);
        }
        closeBtn.SetActive(true);
    }
    public void SelectWisdomButton()
    {
        var minions = RandomDraw(MinionEnums.TYPE.WISDOM);
        playerData.AddMinions(minions);
        ShowEarnedCards(minions); 
        foreach (var button in buttons)
        {
            button.SetActive(false);
        }
        closeBtn.SetActive(true);
    }
    public void SelectCalmButton()
    {
        var minions = RandomDraw(MinionEnums.TYPE.CALM);
        playerData.AddMinions(minions);
        ShowEarnedCards(minions);
        foreach (var button in buttons)
        {
            button.SetActive(false);
        }
        closeBtn.SetActive(true);
    }
}