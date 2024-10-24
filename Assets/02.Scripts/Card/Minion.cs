using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
/// <summary>
/// ī�� ������ ��ġ�� Ÿ�� ������ ������ Ȱ��ȭ �� ��ȭ ���� 
/// </summary>
public class Minion : MonoBehaviour
{
    #region Fields and Properties

    public CardData data { get; private set; }
    [SerializeField] private TileInfo tile;
    [SerializeField] private Image staminabarPrefab;
    [SerializeField] private Button eventButtonPrefab;
    private Image staminabar;
    private Button eventButton;
    private Vector3 staminabarPos = new Vector3(0.5f,0.6f,0);
    private Vector3 eventButtonPos = new Vector3(0f,1.0f,0);
    private float currentStamina;
    private float CurrentStamina { 
        get { return currentStamina; } 
        set { 
            currentStamina = value;
            SetHealthbar();
        } 
    }
    private bool isActive;
    private float defaultTime;
    private float specialTime;
    private float eventTime; //��ȣ�ۿ� �ֱ�

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
            if (defaultTime >= data.produceSpeed)
            {
                defaultTime -= data.produceSpeed;
                Produce.AddGem(data.productYield);
            }

            specialTime += Time.deltaTime;
            if (specialTime >= 10.0f)
            {
                specialTime -= 10.0f;
                if (CanProduceGem())
                {
                    Produce.AddSpecialGem();
                }
                if(--CurrentStamina <= 0)
                {
                    DeactivateMinion();
                }

            }

        }
    }
    /// <summary>
    /// ���õ� ī�尡 �ش� �̴Ͼ��� �����ϴ� Ÿ�ϰ� ��ġ�Ѵٸ� �̴Ͼ� Ȱ��ȭ
    /// </summary>
    private void ActivateMinion()
    {
        var placeManager = CardPlaceManager.Instance;
        if (placeManager.selectedTile == tile)
        {  
            data = placeManager.selectedCard.data;
            staminabar = Instantiate(staminabarPrefab, Camera.main.WorldToScreenPoint(transform.position + staminabarPos) , 
                Quaternion.identity, GameObject.FindGameObjectWithTag("StaminaCanvas").transform);
            CurrentStamina = data.stamina;
            SetImage();
            isActive = true;
            defaultTime = 0.0f;
            specialTime = 0.0f;
            WorkSceneManager.Instance.minions.Add(this);
            switch (WorkSceneManager.Instance.minions.Count)
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
        tile.UnSelectTile();
        GetComponent<SpriteRenderer>().sprite = null;
        Destroy(staminabar.gameObject);
        StopAllCoroutines();

    }

    private bool CanProduceGem()
    {
        System.Random random = new System.Random(Guid.NewGuid().GetHashCode());
        if (random.Next(1, 11) <= data.goodsProbability)
        {
            return true;
        }
        return false;
    }

    private void SetHealthbar()
    {
        staminabar.fillAmount = CurrentStamina / data.stamina;
    }
    
    //�̹��� -> �ִϸ��̼����� ������ ���� �ʿ�
    /// <summary>
    /// cid ���� ��ġ�ϴ� �̹��� �Ҵ�
    /// </summary>
    private void SetImage()
    {
        Sprite[] characterImages = Resources.LoadAll<Sprite>("Character/CharacterImage");
        if (0 == characterImages.Length)
        {
            Debug.LogError($"ĳ���� �̹����� ���� imagePath: {characterImages}");
            return;
        }

        foreach (var image in characterImages)
        {
            if (image.name == data.cid)
            {
                GetComponent<SpriteRenderer>().sprite =image;
                return;
            }
        }

        Debug.LogError($"��������Ʈ�� ����. imageName : {data.cid}");
        return;
    }

    // �������� ���õ� ��ȣ�ۿ� ��ư�� ���� ��ư�� �����ų� 1�ʰ� ������ ���������
    // ������ ��ư ������ �α� ��¸�. ����� ���� �߰��ؾ���
    IEnumerator MinionEvent(float repeatTime)
    {
        while (true)
        {
            yield return new WaitForSeconds(repeatTime);
            //��ȣ�ۿ� ǥ��
            switch (selectRandomEvent())
            {
                case MINON_EVENT.EXTRA_GEM:
                    eventButton = Instantiate(eventButtonPrefab, Camera.main.WorldToScreenPoint(transform.position + eventButtonPos),
                                                Quaternion.identity, GameObject.FindGameObjectWithTag("StaminaCanvas").transform);
                    eventButton.gameObject.GetComponentInChildren<TextMeshProUGUI>().text = "�߰���ȭ";
                    eventButton.onClick.AddListener(() => {
                        Debug.Log("extragem event");
                        Destroy(eventButton.gameObject);
                        });
                    StartCoroutine(DestroyEventButton());
                    break;
                case MINON_EVENT.TRUST:
                    eventButton = Instantiate(eventButtonPrefab, Camera.main.WorldToScreenPoint(transform.position + eventButtonPos),
                                                Quaternion.identity, GameObject.FindGameObjectWithTag("StaminaCanvas").transform);
                    eventButton.gameObject.GetComponentInChildren<TextMeshProUGUI>().text = "�ŷڵ�";
                    eventButton.onClick.AddListener(() => {
                        Debug.Log("trust event");
                        Destroy(eventButton.gameObject);
                    });
                    StartCoroutine(DestroyEventButton());
                    break;
                case MINON_EVENT.FEVER_TIME:
                    eventButton = Instantiate(eventButtonPrefab, Camera.main.WorldToScreenPoint(transform.position + eventButtonPos),
                                                Quaternion.identity, GameObject.FindGameObjectWithTag("StaminaCanvas").transform);
                    eventButton.gameObject.GetComponentInChildren<TextMeshProUGUI>().text = "�ǹ�Ÿ��";
                    eventButton.onClick.AddListener(() => {
                        Debug.Log("fever event");
                        Destroy(eventButton.gameObject);
                    });
                    StartCoroutine(DestroyEventButton());
                    break;
            }
        }
    }
    IEnumerator DestroyEventButton()
    {
        yield return new WaitForSeconds(1.0f);
        if (eventButton != null)
        {
            Destroy(eventButton.gameObject);
        }
    }

    /// <summary>
    /// 3���� ��ȣ�ۿ� �� �ϳ��� �������� ��ȯ
    /// </summary>
    /// <returns></returns>
    private MINON_EVENT selectRandomEvent()
    {
        System.Random random = new System.Random(Guid.NewGuid().GetHashCode());
        var chance = random.Next(1, 31);
        if (chance <= 10)
        {
            return MINON_EVENT.EXTRA_GEM;
        }
        else if (chance <= 20)
        {
            return MINON_EVENT.TRUST;
        }
        else
        {
            return MINON_EVENT.FEVER_TIME;
        }
    }
    #endregion
}

public enum MINON_EVENT
{
    EXTRA_GEM,
    TRUST,
    FEVER_TIME
}