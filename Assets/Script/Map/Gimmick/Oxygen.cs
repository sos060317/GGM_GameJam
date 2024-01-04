using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Oxygen : MonoBehaviour
{
    [SerializeField] private float maxOxygenAmount = 100.0f;
    [SerializeField] private float currentOxygenAmount;
    [SerializeField] private Slider oxygenSlider;

    private void OnEnable()
    {
        currentOxygenAmount = maxOxygenAmount;
        GameManager.Instance.oxygen = this;
    }

    private void OnDisable()
    {
        GameManager.Instance.oxygen = null;
    }

    public void AmountDown(float downAmount)
    {
        currentOxygenAmount -= downAmount;

        DownOxygen();

        if (currentOxygenAmount <= 0)
        {
            Debug.Log("플레이어가 사망한다.");
        }
    }

    private void DownOxygen()
    {

    }
}
