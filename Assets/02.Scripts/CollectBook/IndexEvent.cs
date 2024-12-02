using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IndexEvent : MonoBehaviour
{
    public RectTransform passionIndex;
    public RectTransform calmIndex;
    public RectTransform wisdomIndex;
    public string indexName;

    // ���� passionī�� ������ Ŭ���ϸ� passionIndex������Ʈ�� Ȱ��ȭ ��ġ��, �ٸ� ī�� �ε��� ������Ʈ ������ passionIndex������Ʈ�� ��Ȱ��ȭ ��ġ��
    // passionIndex
    private Vector2 passionActivePos = new Vector2(0, -190);
    private Quaternion passionActiveRot = Quaternion.Euler(0, 0, 0);

    private Vector2 passionUnactivePos = new Vector2(1270, -190);
    private Quaternion passionUnactiveRot = Quaternion.Euler(0, 180, 0);

    // calmIndex
    private Vector2 calmActivePos = new Vector2(-1280, -330);
    private Quaternion calmActiveRot = Quaternion.Euler(0, 180, 0);

    private Vector2 calmUnactivePos = new Vector2(0, -330);
    private Quaternion calmUnactiveRot = Quaternion.Euler(0, 0, 0);

    // wisdomIndex
    private Vector2 wisdomActivePos = new Vector2(-1280, -480);
    private Quaternion wisdomActiveRot = Quaternion.Euler(0, 180, 0);

    private Vector2 wisdomUnactivePos = new Vector2(0, -480);
    private Quaternion wisdomUnactiveRot = Quaternion.Euler(0, 0, 0);


    public enum IndexType 
    { 
        PASSION,
        CALM,
        WISDOM }

    void Start()
    {

        if (gameObject.name.Contains("PASSION"))
        {
            indexName = "PASSION";
        }
        else if (gameObject.name.Contains("CALM"))
        {
            indexName = "CALM";
        }
        else if (gameObject.name.Contains("WISDOM"))
        {
            indexName = "WISDOM";
        }
        else
        {
            Debug.Log("�ε��� �̸� ������ �� ����");
        }
    }


    public void IndexEventAction()
    {
        switch (indexName)
        {
            case "PASSION":
                PassionIndex();
                BookUI.Instance.DisplayMinionsByType(MinionEnums.TYPE.PASSION);

                break;

            case "CALM":
                CalmIndex();
                BookUI.Instance.DisplayMinionsByType(MinionEnums.TYPE.CALM);

                break;

            case "WISDOM":
                WisdomIndex();
                BookUI.Instance.DisplayMinionsByType(MinionEnums.TYPE.WISDOM);
                break;
            default:
                break;

        }
    }

    private void PassionIndex()
    {
        Vector2 currentPosition = passionIndex.anchoredPosition;
        Quaternion currentRotation = passionIndex.localRotation;

        if (currentPosition == passionActivePos && currentRotation == passionActiveRot)
        {
            Debug.Log("PassionIndex�� �⺻ ��ġ�� �����Դϴ�. �������� �ʽ��ϴ�.");
            return;
        }
        else if(currentPosition==passionUnactivePos&&currentRotation==passionUnactiveRot)
        {
            PassionActivePosRot();
            CalmUnactivePosRot();
            WisdomUnactivePosRot();
        }


        Debug.Log("passion �ε��� �Լ�");
    }

    private void CalmIndex()
    {

        Vector2 currentPosition = calmIndex.anchoredPosition;
        Quaternion currentRotation = calmIndex.localRotation;

        if (currentPosition == calmActivePos && currentRotation == calmActiveRot)
        {
            Debug.Log("calmIndex�� �⺻ ��ġ�� �����Դϴ�. �������� �ʽ��ϴ�.");
            return; 
        }
        else if (currentPosition == calmUnactivePos && currentRotation == calmUnactiveRot)
        {
            CalmActivePosRot();
            PassionUnActivePosRot();
            WisdomUnactivePosRot();
        }

        Debug.Log("calm �ε���");
    }


    private void WisdomIndex()
    {

        Vector2 currentPosition = wisdomIndex.anchoredPosition;
        Quaternion currentRotation = wisdomIndex.localRotation;

        if (currentPosition == wisdomActivePos && currentRotation == wisdomActiveRot)
        {
            Debug.Log("wisdomIndex�� �⺻ ��ġ�� �����Դϴ�. �������� �ʽ��ϴ�.");
            return; 
        }
        else if (currentPosition == wisdomUnactivePos && currentRotation == wisdomUnactiveRot)
        {
            WisdomActivePosRot();
            PassionUnActivePosRot();
            CalmUnactivePosRot();

        }
        Debug.Log("wisdom �ε���");
    }

    private void PassionActivePosRot()
    {
        passionIndex.anchoredPosition = passionActivePos;
        passionIndex.localRotation = passionActiveRot;
    }

    private void PassionUnActivePosRot()
    {
        passionIndex.anchoredPosition = passionUnactivePos;
        passionIndex.localRotation = passionUnactiveRot;
    }

    private void CalmActivePosRot()
    {
        calmIndex.anchoredPosition = calmActivePos;
        calmIndex.localRotation = calmActiveRot;
    }

    private void CalmUnactivePosRot()
    {
        calmIndex.anchoredPosition = calmUnactivePos;
        calmIndex.localRotation = calmUnactiveRot;
    }

    private void WisdomActivePosRot()
    {
        wisdomIndex.anchoredPosition = wisdomActivePos;
        wisdomIndex.localRotation = wisdomActiveRot;
    }

    private void WisdomUnactivePosRot()
    {
        wisdomIndex.anchoredPosition = wisdomUnactivePos;
        wisdomIndex.localRotation = wisdomUnactiveRot;
    }
}
