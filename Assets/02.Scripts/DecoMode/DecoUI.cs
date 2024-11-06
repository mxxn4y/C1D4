using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DecoUI : MonoBehaviour
{

    public static DecoUI Instance { get; private set;}

    public GameObject decoPanel;
    public GameObject decoPrefab;

    private Dictionary<ShopItemSO, GameObject> decoItems = new Dictionary<ShopItemSO, GameObject>();
    //private Dictionary<ShopItemSO, int> decoItemsCount = InventoryUI.Instance.GetInventoryDic();

    private void Awake()
    {
        if(Instance!=null)
        {
            Destroy(gameObject);
        }
        else
        {
            Instance = this;
        }
    }

    void Start()
    {
        UpdateDecoUI();   
    }


    void Update()
    {
        UpdateDecoUI();
    }

 public void UpdateDecoUI()
{
    // UI ���� ���� ���� UI ��� �ʱ�ȭ
    //ClearDecoUI();

    // InventoryUI���� ������ ��� ������
    Dictionary<ShopItemSO, int> decoItemsCount = InventoryUI.Instance.GetInventoryDic();

    // ItemType�� NONE �Ǵ� SLOT�� �ƴ� �����۸� ���͸��Ͽ� ǥ��
    foreach (var decoItem in decoItemsCount)
    {
        if (decoItem.Key.itemType != ShopItemSO.ItemType.NONE && decoItem.Key.itemType != ShopItemSO.ItemType.SLOT)
        {
            GameObject decoItemGO;
            if (!decoItems.ContainsKey(decoItem.Key))
            {
                // �������� ������ ���� �ν��Ͻ�ȭ
                decoItemGO = Instantiate(decoPrefab, decoPanel.transform);
                decoItems[decoItem.Key] = decoItemGO;
            }
            else
            {
                // �̹� �ִ� ��� ������
                decoItemGO = decoItems[decoItem.Key];
            }

            // ������ ������ UI�� ����
            var decoItemUI = decoItemGO.GetComponent<PurchasedCardUI>();
            //decoItemUI.SetItem(decoItem.Key, decoItem.Value); // ������ �Բ� ������ ���� ����
        }
    }
}
}

