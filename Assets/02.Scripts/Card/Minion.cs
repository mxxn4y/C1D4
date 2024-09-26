using System.Collections;
using System.Collections.Generic;
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
    private Image staminabar;
    private Vector3 staminabarPos = new Vector3(0.5f,0.6f,0);
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
                defaultTime -= 1.0f;
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
            staminabar = Instantiate(staminabarPrefab, transform.position + staminabarPos, Quaternion.identity, GameObject.FindGameObjectWithTag("StaminaCanvas").transform);
            CurrentStamina = data.stamina;
            SetImage();
            isActive = true;
            defaultTime = 0.0f;
            specialTime = 0.0f;
        }
    }

    private void DeactivateMinion()
    {
        isActive = false;
        tile.UnSelectTile();
        GetComponent<SpriteRenderer>().sprite = null;
        Destroy(staminabar.gameObject);

    }

    private bool CanProduceGem()
    {
        float temp = Time.time * 100f;
        Random.InitState((int)temp);
        if(Random.Range(1,11) <= data.goodsProbability)
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

    #endregion
}
