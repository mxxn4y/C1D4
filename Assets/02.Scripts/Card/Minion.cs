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
