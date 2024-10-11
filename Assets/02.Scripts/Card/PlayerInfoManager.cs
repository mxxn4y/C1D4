using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class PlayerInfoManager
{
    //���� �ѵ�
    public static int[] slotLimit { get; private set; } = { 8, 8, 8 };

    //ī�� ȹ�� ����
    public static int[] gainNum { get; private set; } = { 5, 5, 5 };

    //������
    public static int gem { get; private set; }
    public static int specialGem { get; private set; }

    //���� ������


    //���� ī�� <cid,����>
    public static Dictionary<string, int> playerCards { get; private set; } = new Dictionary<string, int>();


    public static void SortCards()
    {
        playerCards = playerCards.OrderBy(item => item.Key).ToDictionary(x => x.Key, x => x.Value);
    }

    //�÷��̾� ���� �����ϴ� Ư�� Ÿ���� ���� ��ȯ
    public static int CountCard(CARD_TYPE _type)
    {
        int num = 0;

        switch (_type)
        {
            case CARD_TYPE.PASSION:
                foreach (var card in playerCards)
                {
                    if (card.Key.StartsWith('P'))
                    {
                        num += card.Value;
                    }
                }
                break;
            case CARD_TYPE.CALM:
                foreach (var card in playerCards)
                {
                    if (card.Key.StartsWith('C'))
                    {
                        num += card.Value;
                    }
                }
                break;
            case CARD_TYPE.WISDOM:
                foreach (var card in playerCards)
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

    //�÷��̾� ���� �Ű������� ���� ī�� 1�� �߰�
    public static void AddCard(string _cid)
    {
        Debug.Log("ȹ���� ī��: " + _cid);
        if (playerCards.ContainsKey(_cid))
        {
            playerCards[_cid]++;
        }
        else
        {
            playerCards.Add(_cid, 1);
        }
    }
    //�÷��̾ ȹ�� ������ ī�� ����
    public static int GainableCardNum(CARD_TYPE _type)
    {
        int availableNum = 0;
        int gainAmount = 0;

        switch (_type)
        {
            case CARD_TYPE.PASSION:
                availableNum = slotLimit[0] - CountCard(_type);
                gainAmount = gainNum[0];
                break;
            case CARD_TYPE.CALM:
                availableNum = slotLimit[1] - CountCard(_type);
                gainAmount = gainNum[1];
                break;
            case CARD_TYPE.WISDOM:
                availableNum = slotLimit[2] - CountCard(_type);
                gainAmount = gainNum[2];
                break;
        }
        Debug.Log("ȹ�� ������ ī�� ����: " + Mathf.Min(availableNum, gainAmount));
        return Mathf.Min(availableNum, gainAmount);
    }
}
