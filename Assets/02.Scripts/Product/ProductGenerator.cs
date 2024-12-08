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
    public int whiteProductNumber { get; private set; } = 0;
    public int yellowProductNumber { get; private set; } = 0;
    public int cyanProductNumber { get; private set; } = 0;
    public int magentaProductNumber { get; private set; } = 0;


    private void Start()
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
            switch (currentProduct.GetComponent<Product>().currentProductColor)
            {
                //흰색
                case FactoryGameManager.FactoryColor.WHITE:
                    //상품 개수 더하기
                    whiteProductNumber++;
                    break;

                //노란색
                case FactoryGameManager.FactoryColor.YELLOW:
                    //상품 개수 더하기
                    yellowProductNumber++;
                    break;

                //시안색
                case FactoryGameManager.FactoryColor.CYAN:
                    //상품 개수 더하기
                    cyanProductNumber++;
                    break;

                //마젠타색
                case FactoryGameManager.FactoryColor.MAGENTA:
                    //상품 개수 더하기
                    magentaProductNumber++;
                    break;

            }
            //이펙트나 상품 쌓기 등등 할거면 여기

            //상품 개수 정산
            productUIManager.SetProductNumberText(yellowProductNumber, cyanProductNumber, magentaProductNumber, whiteProductNumber);

            //상품 삭제
            Destroy(currentProduct);
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
                currentProduct = Instantiate(productPrefeb, startPos);

                for(int i = 0; i < machineInteractions.Length; i++)
                {
                    machineInteractions[i].currentProduct = currentProduct.GetComponent<Product>();
                }

                //딜레이만큼 대기
                yield return new WaitForSeconds(generateDelay);
            }
        }
    }
}
