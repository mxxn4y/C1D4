using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CardEvent : MonoBehaviour
{
    //public static CardEvent Instance { get; private set; }

    private Minion minionData;
    private CollectCard collectCard;
    public bool isSelected;
    public RectTransform cardPropertyPrefab;
    private GameObject instantiatedProperty;
    public Canvas parentCanvas;

    //public RectTransform Rect;
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
        //Rect = GetComponent<RectTransform>();
        collectCard = GetComponent<CollectCard>();
        if (uiCamera == null)
        {
            uiCamera = Camera.main; // UI 카메라 설정
        }
        if (parentCanvas == null)
        {
            parentCanvas = GetComponentInParent<Canvas>();

        }
    }
    // Update is called once per frame
    void Update()
    {
    }

    public void CardClick()
    {
        //if (minionData.Exhaustion != false)
        //{
        if (isSelected)
        {
            PlayerData.Instance.SelectedMinions.Remove(minionData);
            Debug.Log(PlayerData.Instance.SelectedMinions);
            isSelected = false;
            collectCard.SetUnClickImg();
            Debug.Log("카드 선택 취소");
        }
        else
        {
            if (PlayerData.Instance.SelectedMinions.Count < 3)
            {
                PlayerData.Instance.SelectedMinions.Add(minionData);
                isSelected = true;
                collectCard.SetClickImg();
                Debug.Log("카드 선택");
            }
            else
            {
                Debug.Log("카드 선택할 수 없다요");
            }
        }
        Debug.Log("탈진으로 선택 불가");
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
        Debug.Log("호버링");
        /*
        if (cardPropertyPrefab == null)
        {
            Debug.LogError("cardPropertyPrefab이 설정되지 않았습니다!");
            return;
        }

        // 마우스 위치를 캔버스 공간으로 변환
        RectTransformUtility.ScreenPointToLocalPointInRectangle(
            parentCanvas.transform as RectTransform,
            Input.mousePosition,
            uiCamera,
            out Vector2 localPoint);

        // 프리팹 생성 및 위치 설정
        instantiatedProperty = Instantiate(cardPropertyPrefab, parentCanvas.transform).gameObject;
        RectTransform rectTransform = instantiatedProperty.GetComponent<RectTransform>();
        rectTransform.anchoredPosition = localPoint + new Vector2(200, -200); // 마우스 기준으로 오른쪽 하단
        Debug.Log("카드 속성 프리팹 생성");
        */
    }

    public void CardExit()
    {
        Debug.Log("호버링 취소");
        if (instantiatedProperty != null)
        {
            //Destroy(instantiatedProperty);
        }
    }

    public bool IsCard(GameObject _clickedObj)
    {
        return false;
    }
}
