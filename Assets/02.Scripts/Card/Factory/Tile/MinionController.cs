using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.Serialization;
using UnityEngine.UI;
/// <summary>
/// 카드 정보와 위치한 타일 정보를 가지고 활성화 시 재화 생산
/// </summary>
public class MinionController : MonoBehaviour
{
    #region Fields and Properties
    
    [SerializeField] private TileInfo tile;
    [SerializeField] private GameObject minionGO;
    [SerializeField] private Image staminaBarPrefab;
    [SerializeField] private Button eventButtonPrefab;
    private Image staminaBar;
    private Button eventButton;
    private readonly Vector3 staminaBarPos = new Vector3(0.5f,0.4f,0);
    private readonly Vector3 eventButtonPos = new Vector3(0f,1.0f,0);
    
    private Minion minion;
    private float currentStamina;
    private float CurrentStamina { 
        get { return currentStamina; } 
        set { 
            currentStamina = value;
            SetHealthbar();
        } 
    }
    private bool isActive;
    private float defaultTime; //일반재화 생산 주기
    private float specialTime; //특수재화 생산 주기
    private float eventTime; //상호작용 주기

    #endregion

    #region Methods
    private void Awake()
    {
        CardPlaceManager.Instance.OnCardPlace += ActivateMinion;
    }

    private void Start()
    {
        isActive = false;
    }
    private void Update()
    {
        if (isActive)
        {
            defaultTime += Time.deltaTime;
            if (defaultTime >= minion.BaseData.speed)
            {
                defaultTime -= minion.BaseData.speed;
                FactoryManager.Instance.AddGem(minion.BaseData.efficiency);
            }

            specialTime += Time.deltaTime;
            if (specialTime >= 10.0f)
            {
                specialTime -= 10.0f;
                if(minion.TryEarnSpecialGem()) { FactoryManager.Instance.AddSpecialGem(); }
                
                if(--CurrentStamina <= 0)
                {
                    DeactivateMinion();
                }

            }

        }
    }
    /// <summary>
    /// 선택된 카드가 해당 미니언이 존재하는 타일과 일치한다면 미니언 활성화
    /// </summary>
    private void ActivateMinion()
    {
        var placeManager = CardPlaceManager.Instance;
        if (placeManager.SelectedTile == tile)
        {  
            minionGO.SetActive(true);
            minion = placeManager.SelectedCard.Minion;
            staminaBar = Instantiate(staminaBarPrefab, 
                Camera.main.WorldToScreenPoint(transform.position + staminaBarPos) , 
                        Quaternion.identity, GameObject.FindGameObjectWithTag("StaminaCanvas").transform);
            CurrentStamina = minion.BaseData.stamina;
            SetImage();
            isActive = true;
            defaultTime = 0.0f;
            specialTime = 0.0f;
            FactoryManager.Instance.minions.Add(this);
            switch (FactoryManager.Instance.minions.Count)
            {
                case 1:
                    eventTime = 2.0f;
                    break;
                case 2:
                    eventTime = 3.0f;
                    break;
                case 3:
                    eventTime = 5.0f;
                    break;
                case 4:
                    eventTime = 7.0f;
                    break;
                default:
                    eventTime = 10.0f;
                    break;
            }
            StartCoroutine(MinionEvent(eventTime));
        }
    }

    private void DeactivateMinion()
    {
        isActive = false;
        tile.TileState = TILE_STATE.DEFAULT;
        minionGO.SetActive(false);
        Destroy(staminaBar.gameObject);
        if (eventButton != null)
        {
            Destroy(eventButton.gameObject);
        }
        StopAllCoroutines();

    }

    

    private void SetHealthbar()
    {
        staminaBar.fillAmount = CurrentStamina / minion.BaseData.stamina;
    }
    
    //이미지 -> 애니메이션으로 넣으면 수정 필요
    /// <summary>
    /// cid 값과 일치하는 이미지 할당
    /// </summary>
    private void SetImage()
    {
        Sprite[] characterImages = Resources.LoadAll<Sprite>("Character/CharacterImage");
        if (0 == characterImages.Length)
        {
            Debug.LogError($"캐릭터 이미지가 없음 imagePath: {characterImages}");
            return;
        }

        foreach (var image in characterImages)
        {
            if (image.name == minion.BaseData.mid)
            {
                minionGO.GetComponent<SpriteRenderer>().sprite =image;
                return;
            }
        }

        Debug.LogError($"스프라이트가 없음. imageName : {minion.BaseData.mid}");

    }

    // 랜덤으로 선택된 상호작용 버튼을 띄우고 버튼을 누르거나 1초가 지나면 사라지도록
    // 지금은 버튼 누르면 로그 출력만. 기능은 이후 추가해야함
    private IEnumerator MinionEvent(float repeatTime)
    {
        while (true)
        {
            yield return new WaitForSeconds(repeatTime);
            //상호작용 표시
            switch (SelectRandomEvent())
            {
                case MinionEnums.EVENT.EXTRA_GEM:
                    eventButton = Instantiate(eventButtonPrefab, Camera.main.WorldToScreenPoint(transform.position + eventButtonPos),
                                                Quaternion.identity, GameObject.FindGameObjectWithTag("StaminaCanvas").transform);
                    eventButton.gameObject.GetComponentInChildren<TextMeshProUGUI>().text = "추가재화";
                    eventButton.onClick.AddListener(() => {
                        Debug.Log("extragem event");
                        Destroy(eventButton.gameObject);
                        });
                    StartCoroutine(DestroyEventButton());
                    break;
                case MinionEnums.EVENT.TRUST:
                    eventButton = Instantiate(eventButtonPrefab, Camera.main.WorldToScreenPoint(transform.position + eventButtonPos),
                                                Quaternion.identity, GameObject.FindGameObjectWithTag("StaminaCanvas").transform);
                    eventButton.gameObject.GetComponentInChildren<TextMeshProUGUI>().text = "신뢰도";
                    eventButton.onClick.AddListener(() => {
                        Debug.Log("trust event");
                        Destroy(eventButton.gameObject);
                    });
                    StartCoroutine(DestroyEventButton());
                    break;
                case MinionEnums.EVENT.FEVER_TIME:
                    eventButton = Instantiate(eventButtonPrefab, Camera.main.WorldToScreenPoint(transform.position + eventButtonPos),
                                                Quaternion.identity, GameObject.FindGameObjectWithTag("StaminaCanvas").transform);
                    eventButton.gameObject.GetComponentInChildren<TextMeshProUGUI>().text = "피버타임";
                    eventButton.onClick.AddListener(() => {
                        Debug.Log("fever event");
                        Destroy(eventButton.gameObject);
                    });
                    StartCoroutine(DestroyEventButton());
                    break;
            }
        }
    }

    private IEnumerator DestroyEventButton()
    {
        yield return new WaitForSeconds(1.0f);
        if (eventButton != null)
        {
            Destroy(eventButton.gameObject);
        }
    }

    /// <summary>
    /// 3개의 상호작용 중 하나를 랜덤으로 반환
    /// </summary>
    private MinionEnums.EVENT SelectRandomEvent()
    {
        System.Random random = new System.Random(Guid.NewGuid().GetHashCode());
        var chance = random.Next(1, 31);
        if (chance <= 10)
        {
            return MinionEnums.EVENT.EXTRA_GEM;
        }
        else if (chance <= 20)
        {
            return MinionEnums.EVENT.TRUST;
        }
        else
        {
            return MinionEnums.EVENT.FEVER_TIME;
        }
    }
    #endregion
}

