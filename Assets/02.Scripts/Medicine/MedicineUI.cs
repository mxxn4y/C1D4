using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class MedicineUI : MonoBehaviour
{
    private int eatMedicine = 0;
    [SerializeField] TMP_Text text;
    void Start()
    {
        gameObject.SetActive(true);
    }

    public void EatMedicine()
    {
        ++eatMedicine;
        text.text = "Eat Medicine : " + eatMedicine;
        gameObject.SetActive(false);
    }
}
