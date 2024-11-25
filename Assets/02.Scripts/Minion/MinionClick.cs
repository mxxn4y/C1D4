using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MinionClick : MonoBehaviour
{
    [SerializeField] private MinionController minionController;

    private void OnMouseDown()
    {
        minionController.TryChangeRestState();
    }

    private void OnMouseEnter()
    {
        
    }

    private void OnMouseExit()
    {
        
    }
}
