using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
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
    [SerializeField] private GameObject minionGo;
    [SerializeField] private Image staminaBarPrefab;
    [SerializeField] private Button eventButtonPrefab;
    [SerializeField] private Text coolTimeTextPrefab;
    private Image staminaBar;
    private SpriteRenderer minionSprite;
    private Button eventButton;
    private TextMeshProUGUI eventBtnText;
    private Text coolTimeText;
    private readonly Vector3 staminaBarPos = new (0.5f,0.4f,0);
    private readonly Vector3 eventButtonPos = new (0f,1.0f,0);
    private readonly Vector3 coolTimeTextPos = new (0f,0.5f,0);
    
    private Minion minion;
    private float currentStamina;
    private float CurrentStamina { 
        get => currentStamina;
        set { 
            currentStamina = value;
            SetStaminaBar();
        } 
    }
    private bool isActive;
    private bool isResting;
    private int restTimer; 
    private float gemTimer; 
    private float sGemAndStaminaTimer; 
    private float gainStaminaTimer;
    private float eventTime; // 상호작용 주기
    private Coroutine[] eventCoroutine;

    #endregion

    #region Methods
    private void Awake()
    {
        CardPlaceManager.Instance.OnCardPlace += SetMinion;
    }

    private void Start()
    {
        minionGo.SetActive(false);
        isActive = false;
        // 체력바 생성
        staminaBar = Instantiate(staminaBarPrefab, 
            Camera.main.WorldToScreenPoint(transform.position + staminaBarPos) , 
                    Quaternion.identity, GameObject.FindGameObjectWithTag("Canvas").transform);
        staminaBar.gameObject.SetActive(false);
        // 이벤트 버튼 생성
        eventButton = Instantiate(eventButtonPrefab, 
            Camera.main.WorldToScreenPoint(transform.position + eventButtonPos),
                    Quaternion.identity, GameObject.FindGameObjectWithTag("Canvas").transform);
        eventBtnText = eventButton.GetComponentInChildren<TextMeshProUGUI>();
        eventButton.gameObject.SetActive(false);
        // 쿨타임 텍스트 생성
        coolTimeText = Instantiate(coolTimeTextPrefab,
            Camera.main.WorldToScreenPoint(transform.position + coolTimeTextPos),
            Quaternion.identity, GameObject.FindGameObjectWithTag("Canvas").transform);
        coolTimeText.gameObject.SetActive(false);
        minionSprite = minionGo.GetComponent<SpriteRenderer>();
    }
    
    private void Update()
    {
        if (isActive)
        {
            gemTimer += Time.deltaTime;
            if (gemTimer >= minion.Data.speed)
            {
                gemTimer -= minion.Data.speed;
                FactoryManager.Instance.AddGem(minion.Data.efficiency);
            }

            sGemAndStaminaTimer += Time.deltaTime;
            if (sGemAndStaminaTimer >= 1.0f)
            {
                sGemAndStaminaTimer -= 1.0f;
                // 특수재화 생산 시도
                if (minion.TryEarnSpecialGem())
                {
                    FactoryManager.Instance.AddSpecialGem();
                }

                // 체력 감소   
                if (--CurrentStamina <= 0)
                {
                    minion.SetExhaustion(true);
                    DeactivateMinion();
                }
            }
        }
        else if (isResting)
        {
            gainStaminaTimer += Time.deltaTime;
            if (gainStaminaTimer >= 1.0f)
            {
                gainStaminaTimer -= 1.0f;
                CurrentStamina = Math.Min(CurrentStamina + 0.5f, minion.Data.stamina);
            }
        }
    }
    
    /// <summary>
    /// 선택된 카드가 해당 미니언이 존재하는 타일과 일치한다면 미니언 활성화
    /// </summary>
    private void SetMinion()
    {
        CardPlaceManager placeManager = CardPlaceManager.Instance;
        if (placeManager.SelectedTile != tile) { return; }

        minion = placeManager.SelectedCard.Minion;
        minionGo.SetActive(true);
        staminaBar.gameObject.SetActive(true);
        CurrentStamina = minion.Data.stamina;
        SetImage();
        eventCoroutine = new Coroutine[2];
        FactoryManager.Instance.ActiveMinionList.Add(this);
        eventTime = FactoryManager.Instance.ActiveMinionList.Count switch
        {
            1 => 4.0f,
            2 => 6.0f,
            3 => 7.0f,
            4 => 9.0f,
            _ => 11.0f
        };
        ActivateMinion();
    }

    private void ActivateMinion()
    {
        isActive = true;
        isResting = false;
        gemTimer = 0.0f;
        sGemAndStaminaTimer = 0.0f;
        minionSprite.color = Color.white;
        coolTimeText.color = Color.clear;
        eventCoroutine[0] = StartCoroutine(MinionEvent(eventTime));
    }

    private void RestMinion()
    {
        isActive = false;
        isResting = true;
        gainStaminaTimer = 0.0f;
        eventButton.gameObject.SetActive(false);
        minionSprite.color = Color.black;
        coolTimeText.color = Color.white;
        foreach (Coroutine coroutine in eventCoroutine.Where(_c=>_c!=null))
        {
            StopCoroutine(coroutine);
        }
    }

    private void DeactivateMinion()
    {
        isActive = false;
        tile.TileState = TILE_STATE.DEFAULT;
        minionGo.SetActive(false);
        staminaBar.gameObject.SetActive(false);
        eventButton.gameObject.SetActive(false);
        coolTimeText.gameObject.SetActive(false);
        StopAllCoroutines();
    }

    

    private void SetStaminaBar()
    {
        staminaBar.fillAmount = CurrentStamina / minion.Data.stamina;
    }
    
    //이미지 -> 애니메이션으로 넣으면 수정 필요
    /// <summary>
    /// mid 값과 일치하는 이미지 할당
    /// </summary>
    private void SetImage()
    {
        Sprite[] characterImages = Resources.LoadAll<Sprite>("Character/CharacterImage");
        if (0 == characterImages.Length)
        {
            Debug.LogError($"캐릭터 이미지가 없음 imagePath: {characterImages}");
            return;
        }
        
        foreach (Sprite image in characterImages)
        {
            if (image.name != minion.Data.mid) { continue; }

            minionSprite.sprite = image;
            return;
        }

        Debug.LogError($"스프라이트가 없음. imageName : {minion.Data.mid}");

    }

    // 랜덤으로 선택된 상호작용 버튼을 띄우고 버튼을 누르거나 1초가 지나면 사라지도록
    // 지금은 버튼 누르면 로그 출력만. 기능은 이후 추가해야함
    private IEnumerator MinionEvent(float _repeatTime)
    {
        while (true)
        {
            yield return new WaitForSeconds(_repeatTime);
            GameObject buttonGo = eventButton.gameObject;
            //상호작용 표시
            switch (SelectRandomEvent())
            {
                case MinionEnums.EVENT.EXTRA_GEM:
                    buttonGo.SetActive(true);
                    eventBtnText.text = "추가재화";
                    eventButton.onClick.AddListener(() => {
                        Debug.Log("extraGem event");
                        buttonGo.SetActive(false);
                        });
                    eventCoroutine[1] = StartCoroutine(EndEvent());
                    break;
                case MinionEnums.EVENT.TRUST:
                    buttonGo.SetActive(true);
                    eventBtnText.text = "신뢰도";
                    eventButton.onClick.AddListener(() => {
                        Debug.Log("trust event");
                        buttonGo.SetActive(false);
                    });
                    eventCoroutine[1] = StartCoroutine(EndEvent());
                    break;
                case MinionEnums.EVENT.FEVER_TIME:
                    buttonGo.SetActive(true);
                    eventBtnText.text = "피버타임";
                    eventButton.onClick.AddListener(() => {
                        Debug.Log("fever event");
                        buttonGo.SetActive(false);
                    });
                    eventCoroutine[1] = StartCoroutine(EndEvent());
                    break;
            }
        }
    }

    private IEnumerator EndEvent()
    {
        yield return new WaitForSeconds(1.0f);
        eventButton.gameObject.SetActive(false);
    }

    /// <summary>
    /// 3개의 상호작용 중 하나를 랜덤으로 반환
    /// </summary>
    private MinionEnums.EVENT SelectRandomEvent()
    {
        System.Random random = new (Guid.NewGuid().GetHashCode());
        int chance = random.Next(1, 31);
        return chance switch
        {
            <= 10 => MinionEnums.EVENT.EXTRA_GEM,
            <= 20 => MinionEnums.EVENT.TRUST,
            _ => MinionEnums.EVENT.FEVER_TIME
        };
    }

    public void TryChangeRestState()
    {
        if (restTimer > 0f)
        {
            return;
        }
        
        StartCoroutine(RestTimerStart());
        if (isActive)
        {
            RestMinion();
        }
        else if(isResting)
        {
            ActivateMinion();
        }
    }

    private IEnumerator RestTimerStart()
    {
        restTimer = 15;
        coolTimeText.gameObject.SetActive(true);
        while (restTimer > 0)
        {
            coolTimeText.text = restTimer.ToString("00");
            yield return new WaitForSeconds(1.0f);
            if (--restTimer == 0)
            {
                coolTimeText.gameObject.SetActive(false);
            }
        }
    }
    
    #endregion
}

