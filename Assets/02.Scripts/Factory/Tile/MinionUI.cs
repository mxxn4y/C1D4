using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.Serialization;
using UnityEngine.UI;

public class MinionUI : MonoBehaviour
{
    [SerializeField] private Image staminaBarPrefab;
    [SerializeField] private Button eventBtnPrefab;
    [SerializeField] private Text coolTimeTxtPrefab;
    [FormerlySerializedAs("minionSpriteRenderer")] [SerializeField] private SpriteRenderer minionSprite;
    
    private Image staminaBar;
    private Button eventBtn;
    private TextMeshProUGUI eventBtnTxt;
    private Text coolTimeTxt;
    private readonly Vector3 staminaBarPos = new (0.5f,0.4f,0);
    private readonly Vector3 eventBtnPos = new (0f,1.0f,0);
    private readonly Vector3 coolTimeTxtPos = new (0f,0.5f,0);

    public Action tryChangeState;
    
    private void OnMouseDown()
    {
        tryChangeState?.Invoke();
    }

    private void OnMouseEnter()
    {
        if (minionSprite.color == Color.white)
        {
            minionSprite.color = Color.gray;
            coolTimeTxt.color = Color.white;
        }
    }

    private void OnMouseExit()
    {
        if (minionSprite.color == Color.gray)
        {
            minionSprite.color = Color.white;
            coolTimeTxt.color = Color.clear;
        }
    }

    public void Init()
    {
        CreateStaminaBar();
        CreateEventButton();
        CreateCoolTimeText();
    }
    private void CreateStaminaBar()
    {
        staminaBar = Instantiate(staminaBarPrefab, 
            Camera.main.WorldToScreenPoint(transform.position + staminaBarPos) , 
            Quaternion.identity, GameObject.FindGameObjectWithTag("Canvas").transform);
        staminaBar.gameObject.SetActive(false);
    }

    private void CreateEventButton()
    {
        eventBtn = Instantiate(eventBtnPrefab, 
            Camera.main.WorldToScreenPoint(transform.position + eventBtnPos),
            Quaternion.identity, GameObject.FindGameObjectWithTag("Canvas").transform);
        eventBtnTxt = eventBtn.GetComponentInChildren<TextMeshProUGUI>();
        eventBtn.gameObject.SetActive(false);
    }

    private void CreateCoolTimeText()
    {
        coolTimeTxt = Instantiate(coolTimeTxtPrefab,
            Camera.main.WorldToScreenPoint(transform.position + coolTimeTxtPos),
            Quaternion.identity, GameObject.FindGameObjectWithTag("Canvas").transform);
        coolTimeTxt.gameObject.SetActive(false);
    }

    public void ActivateMinion()
    {
        staminaBar.gameObject.SetActive(true);
        minionSprite.color = Color.white;
        coolTimeTxt.color = Color.clear;
    }

    public void RestMinion()
    {
        eventBtn.gameObject.SetActive(false);
        minionSprite.color = Color.black;
        coolTimeTxt.color = Color.white;
    }

    public void DeactivateMinion()
    {
        staminaBar.gameObject.SetActive(false);
        eventBtn.gameObject.SetActive(false);
        coolTimeTxt.gameObject.SetActive(false);
    }
    
    public void SetStaminaBar(float _fillAmount)
    {
        staminaBar.fillAmount = _fillAmount;
    }
    
    //이미지 -> 애니메이션으로 넣으면 수정 필요
    /// <summary>
    /// mid 값과 일치하는 이미지 할당
    /// </summary>
    public void SetImage(string _mid)
    {
        Sprite[] characterImages = Resources.LoadAll<Sprite>("Character/CharacterImage");
        if (0 == characterImages.Length)
        {
            Debug.LogError($"캐릭터 이미지가 없음 imagePath: {characterImages}");
            return;
        }
        
        foreach (Sprite image in characterImages)
        {
            if (image.name == _mid)
            {
                minionSprite.sprite = image;
                return;
            }
        }

        Debug.LogError($"스프라이트가 없음. imageName : {_mid}");

    }

    public void ActivateEventBtn(MinionEnums.EVENT _event)
    {
        GameObject btnGo = eventBtn.gameObject;
        btnGo.SetActive(true);

        switch (_event)
        {
            case MinionEnums.EVENT.EXTRA_GEM:
                eventBtnTxt.text = "추가재화";
                eventBtn.onClick.AddListener(() => {
                    Debug.Log("extraGem event");
                    btnGo.SetActive(false);
                });
                break;
            case MinionEnums.EVENT.TRUST:
                eventBtnTxt.text = "신뢰도";
                eventBtn.onClick.AddListener(() => {
                    Debug.Log("trust event");
                    btnGo.SetActive(false);
                });
                break;
            case MinionEnums.EVENT.FEVER_TIME:
                eventBtnTxt.text = "피버타임";
                eventBtn.onClick.AddListener(() => {
                    Debug.Log("fever event");
                    btnGo.SetActive(false);
                });
                break;
        }
    }

    public void DeactivateBtn()
    {
        eventBtn.gameObject.SetActive(false);
    }

    public void  SetCoolTimeTxtActive(bool _isActive)
    {
        coolTimeTxt.gameObject.SetActive(_isActive);
    }

    public void SetCoolTimeTxt(int _time)
    {
        coolTimeTxt.text = _time.ToString("00");
    }
}
