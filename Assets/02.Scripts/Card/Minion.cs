using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
/// <summary>
/// 카드 배치하면 생성되는 미니언 객체 클래스
/// </summary>
public class Minion : MonoBehaviour
{
    #region Fields and Properties

    [field: SerializeField] public CardData _data { get; private set; }

    #endregion

    #region Methods

    private void Start()
    {
        
    }


    public void SetMinion(CardData data)
    {
        _data = data;
        SetImage();
    }

    //이미지 -> 애니메이션으로 넣으면 수정 가능성 존재
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
            if (image.name == _data._cid)
            {
                GetComponent<SpriteRenderer>().sprite =image;
                return;
            }
        }

        Debug.LogError($"스프라이트가 없음. imageName : {_data._cid}");
        return;
    }

    #endregion
}
