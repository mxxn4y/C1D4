using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
/// <summary>
/// ī�� ��ġ�ϸ� �����Ǵ� �̴Ͼ� ��ü Ŭ����
/// </summary>
public class Minion : MonoBehaviour
{
    #region Fields and Properties

    public CardData _data { get; private set; }
    [SerializeField] private TileInfo _tile;
    private int _curStamina;
    private int _moneyPerSec;
    private bool _isActive;
    private float _defaultTime;
    private float _specialTime;

    #endregion

    #region Methods
    private void Awake()
    {
        CardPlaceManager.Instance.OnCardPlace += ActivateMinion;
    }

    private void Start()
    {
        _isActive = false;
    }
    private void Update()
    {
        if (_isActive)
        {
            _defaultTime += Time.deltaTime;
            if (_defaultTime >= 1.0f)
            {
                _defaultTime -= 1.0f;
                WorkSceneManager.Instance.AddMoney(_moneyPerSec);
            }

            _specialTime += Time.deltaTime;
            if (_specialTime >= 10.0f)
            {
                _specialTime -= 10.0f;
                if (canProduceGem())
                {
                    WorkSceneManager.Instance.AddGem(1);
                }
                if(--_curStamina <= 0)
                {
                    _isActive=false;
                    _tile.UnSelectTile();
                    GetComponent<SpriteRenderer>().sprite = null;
                }

            }

        }
    }
    private void ActivateMinion()
    {
        var placeManager = CardPlaceManager.Instance;
        if (placeManager._selectedTile == _tile)
        {  
            _data = placeManager._selectedCard._data;
            _curStamina = _data._stamina;
            _moneyPerSec = _data._productSpeed * _data._productYield / 10;
            SetImage();
            _isActive = true;
            _defaultTime = 0.0f;
            _specialTime = 0.0f;
        }
    }

    private bool canProduceGem()
    {
        float temp = Time.time * 100f;
        Random.InitState((int)temp);
        if(Random.Range(1,11) <= _data._goodsProbability)
        {
            return true;
        }
        return false;
    }
    
    //�̹��� -> �ִϸ��̼����� ������ ���� ���ɼ� ����
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
            if (image.name == _data._cid)
            {
                GetComponent<SpriteRenderer>().sprite =image;
                return;
            }
        }

        Debug.LogError($"��������Ʈ�� ����. imageName : {_data._cid}");
        return;
    }

    #endregion
}
