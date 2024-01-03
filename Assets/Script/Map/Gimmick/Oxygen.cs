using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Oxygen : MonoBehaviour
{
    [SerializeField] private float oxygenAmount = 100.0f;

    private void Start()
    {
        GameManager.Instance.oxygen = this;
    }

    private void OnDisable()
    {
        GameManager.Instance.oxygen = null;
    }

    public void AmountDown(float downAmount)
    {
        oxygenAmount -= downAmount;

        if (oxygenAmount <= 0)
        {
            Debug.Log("플레이어의 체력이 감소한다.");
        }
    }
}
