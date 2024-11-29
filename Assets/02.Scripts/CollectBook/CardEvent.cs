using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CardEvent : MonoBehaviour
{
    //public static CardEvent Instance { get; private set; }

    private Minion minionData;
    private Image collectCard;
    public Sprite unOutlineImg;
    public Sprite OutlineImg;
    private bool isSelected=false;
    public RectTransform cardPropertyPrefab;
    private GameObject InstantiatedProperty;
    public RectTransform Rect;
    public Camera uiCamera;
    private Vector2 screenPoint;
/*
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
*/

    // Start is called before the first frame update
    void Start()
    {
        uiCamera = Camera.main;
        Rect = GetComponent<RectTransform>();

        GameObject[] CardBack= GameObject.FindGameObjectsWithTag("CardBack");
        for (int i = 0; i < CardBack.Length; i++)
        {
            collectCard = CardBack[i].GetComponent<Image>();
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void CardClick()
    {
        
        if (isSelected)
        {
            PlayerData.Instance.SelectedMinions.Remove(minionData);
            isSelected = false;
            //UpdateCardOutline(false);
            // CollectCard의 SetOutline()호출
            //collectCard.sprite = unOutlineImg;
            Debug.Log("카드 선택 취소");
        }
        else
        {
            if (PlayerData.Instance.SelectedMinions.Count < 3)
            {
                PlayerData.Instance.SelectedMinions.Add(minionData);
                isSelected = true;
                //UpdateCardOutline(true);
                // CollectCard의 UnsetOutline() 호출
                //collectCard.sprite = OutlineImg;
                Debug.Log("카드 선택");
            }
            else
            {
                Debug.Log("카드 선택할 수 없다요");
            }
        }
    }

    private void UpdateCardOutline(bool _enable)
    {
        if (_enable)
        {

        }
        else
        {

        }
    }
    /*
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

            List<Minion> availableRandom = new List<Minion>(ownCards);
            System.Random rand = new System.Random();
            while (availableRandom.Count < 3 && availableRandom.Count > 0)
            {
                int index = rand.Next(availableRandom.Count); // 랜덤 인덱스 생성
                Minion randomMinion = availableRandom[index];

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
    }
    */

    public void CardEnter()
    {
        //sInstantiate(cardProperyImg,)
        Debug.Log("호버링");
        /*
        Vector3 mousePos = Input.mousePosition; 
        Vector3 worldPos = Camera.main.ScreenToWorldPoint(new Vector3(mousePos.x, mousePos.y, Camera.main.nearClipPlane));

        // ������ ���� (z�� ���� �ʿ�)
        worldPos.z = 0; // z �� ���� (UI ��� �� ����)

        if (cardPropertyPrefab != null)
        {
            InstantiatedProperty = Instantiate(cardPropertyPrefab, worldPos, Quaternion.identity);
            Debug.Log("���콺 ��ġ�� ������ ����");
        }
        else
        {
            Debug.LogError("cardPropertyPrefab�� �������� �ʾҽ��ϴ�!");
        }
        */
        //잠시 주석 처리
        // RectTransformUtility.ScreenPointToLocalPointInRectangle(Rect, Input.mousePosition, GetComponent<Camera>(), out screenPoint);
        //cardPropertyPrefab.localPosition = screenPoint;
    }

    public void CardExit()
    {
        Debug.Log("호버링 취소");
        if (InstantiatedProperty != null)
        {
            Destroy(InstantiatedProperty);
        }
    }

    public bool IsCard(GameObject _clickedObj)
    {
        return false;
    }
}
