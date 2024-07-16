using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class RoomUI : MonoBehaviour
{
    #region Fields and Properties
    private Room _room;

    [Header("Prefab Elements")]
    [SerializeField] private Image _roomImage;
    [SerializeField] private Image _roomFurniture1Image;
    [SerializeField] private Image _roomFurniture2Image;

    [SerializeField] private TextMeshProUGUI _roomNum;

    #endregion

    #region Methods

    private void Awake()
    {
        _room = GetComponent<Room>();
        SetRoomUI();
    }

    public void SetRoomUI()
    {
        //카드가 널이 아니고 데이터가 입력되었는지 체크
        if (_room != null && _room.RoomData != null)
        {
            SetRoomNum();
            SetRoomImgae();
        }
    }
    public void SetMovingUI()
    {
        if (_room._moving)
        {
            Color color = _roomImage.GetComponent<Image>().color;
            color.a = 0.5f;
            _roomImage.GetComponent<Image>().color = color;
        }
        else
        {
            Color color = _roomImage.GetComponent<Image>().color;
            color.a = 1f;
            _roomImage.GetComponent<Image>().color = color;
        }
    }

    public void SetRoomNum()
    {
        _roomNum.text = _room.RoomData.Card.CardNum + "번";
    }
    public void SetRoomImgae()
    {
        _roomImage.sprite = _room.RoomData.Image;
    }

    #endregion
}
