using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Minion : MonoBehaviour
{
    #region Fields and Properties

    [field: SerializeField] public ScriptableMinion MinionData { get; private set; }

    [SerializeField] private Sprite minionImage;

    #endregion

    #region Methods

    private void Start()
    {
        minionImage = GetComponentInChildren<SpriteRenderer>().sprite;
    }


    public void SetMinion(ScriptableMinion minionData)
    {
        MinionData = minionData;
        minionImage = MinionData.Image;
    }

    #endregion
}
