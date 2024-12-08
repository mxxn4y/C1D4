using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ProductGenerator : MonoBehaviour
{
    //UI매니저
    [SerializeField] private ProductUIManager productUIManager;

    //생산 시작 위치
    [SerializeField] private Transform startPos;
    //생산 종료 위치
    [SerializeField] private Transform endPos;

    //상품 프리펩
    [SerializeField] GameObject productPrefeb;
    public GameObject currentProduct { get; private set; }

    [SerializeField] private MachineInteraction[] machineInteractions;

    //생성 주기
    [SerializeField] float generateDelay = 20f;

    //전체 생산 완료 개수
    public int productNumber { get; private set; } = 0;


    private void OnEnable()
    {
        FactoryGameManager.Instance.SetPlace(1, true);
        FactoryGameManager.Instance.SetPlace(0, true);
        FactoryGameManager.Instance.SetPlace(2, true);
        StartCoroutine(GenerateProducts());
    }

    private void Update()
    {
        if (currentProduct == null)
            return;

        if (currentProduct.transform.position == endPos.position)
        {

            //이펙트나 상품 쌓기 등등 할거면 여기

            //상품 개수 정산
            productNumber++;
            productUIManager.SetProductColor(currentProduct.GetComponent<Product>().currentProductColor, productNumber);

            //상품 삭제
            Destroy(currentProduct);
        } 
    }

    private IEnumerator GenerateProducts()
    {
        yield return new WaitForSeconds(5f);

        while (true)
        {
            //나중에 첫 배치 됐는지 판단하기
            if (true)
            {
                //상품 생성
                currentProduct = Instantiate(productPrefeb, startPos);

                for(int i = 0; i < machineInteractions.Length; i++)
                {
                    Debug.Log(currentProduct.GetComponent<Product>());
                    machineInteractions[i].currentProduct = currentProduct.GetComponent<Product>();
                }

                //딜레이만큼 대기
                yield return new WaitForSeconds(generateDelay);
            }
        }
    }
}
