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
    private GameObject product;

    //생성 주기
    [SerializeField] float generateDelay = 20f;

    //전체 생산 완료 개수
    public int productNumber { get; private set; } = 0;

    private void Start()
    {
        StartCoroutine(GenerateProducts());
    }

    private void Update()
    {
        if (product == null)
            return;

        if (product.transform.position == endPos.position)
        {
            //상품 개수 더하기
            productNumber++;

            //상품 개수 정산
            productUIManager.SetProductNumberText(productNumber);

            //이펙트나 상품 쌓기 등등 할거면 여기

            //상품 삭제
            Destroy(product);
        } 
    }

    private IEnumerator GenerateProducts()
    {
        while (true)
        {
            //나중에 첫 배치 됐는지 판단하기
            if (true)
            {
                //상품 생성
                product = Instantiate(productPrefeb, startPos);

                //딜레이만큼 대기
                yield return new WaitForSeconds(generateDelay);
            }
        }
    }
}
