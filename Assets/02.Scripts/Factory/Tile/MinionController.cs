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
    [SerializeField] private MinionUI minionUI;
    
    private Minion minion;
    private float currentStamina;
    private float CurrentStamina { 
        get => currentStamina;
        set { 
            currentStamina = value;
            minionUI.SetStaminaBar(CurrentStamina / minion.Data.stamina);
        } 
    }
    private bool isActive;
    private bool isResting;
    private float gemTimer; 
    private float sGemAndStaminaTimer; 
    private float gainStaminaTimer;
    private float eventTime; // 상호작용 주기
    private Coroutine[] eventCoroutine;

    private int restTimer; 
    private int RestTimer
    {
        get => restTimer;
        set
        {
            restTimer = value;
            minionUI.SetCoolTimeTxt(restTimer);
            if (restTimer == 0)
            {
                minionUI.SetCoolTimeTxtActive(false);
            }
        }
    }

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
        minionUI.Init();
        minionUI.tryChangeState += TryChangeRestState;
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
        CurrentStamina = minion.Data.stamina;
        minionUI.SetImage(minion.Data.mid);
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
        minionUI.ActivateMinion();
        eventCoroutine[0] = StartCoroutine(MinionEvent(eventTime));
    }

    private void RestMinion()
    {
        isActive = false;
        isResting = true;
        gainStaminaTimer = 0.0f;
        minionUI.RestMinion();
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
        minionUI.DeactivateMinion();
        StopAllCoroutines();
    }

    // 랜덤으로 선택된 상호작용 버튼을 띄우고 버튼을 누르거나 1초가 지나면 사라지도록
    // 지금은 버튼 누르면 로그 출력만. 기능은 이후 추가해야함
    private IEnumerator MinionEvent(float _repeatTime)
    {
        while (true)
        {
            yield return new WaitForSeconds(_repeatTime);
            //상호작용 표시
            switch (SelectRandomEvent())
            {
                case MinionEnums.EVENT.EXTRA_GEM:
                    minionUI.ActivateEventBtn(MinionEnums.EVENT.EXTRA_GEM);
                    break;
                case MinionEnums.EVENT.TRUST:
                    minionUI.ActivateEventBtn(MinionEnums.EVENT.TRUST);
                    break;
                case MinionEnums.EVENT.FEVER_TIME:
                    minionUI.ActivateEventBtn(MinionEnums.EVENT.FEVER_TIME);
                    break;
            }
            eventCoroutine[1] = StartCoroutine(EndEvent());
        }
    }

    private IEnumerator EndEvent()
    {
        yield return new WaitForSeconds(1.0f);
        minionUI.DeactivateBtn();
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
        if (RestTimer > 0f)
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
        RestTimer = 15;
        minionUI.SetCoolTimeTxtActive(true);
        while (RestTimer > 0)
        {
            yield return new WaitForSeconds(1.0f);
            RestTimer--;
        }
    }
    
    #endregion
}

