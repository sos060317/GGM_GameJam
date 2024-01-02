using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SkillPanel : MonoBehaviour
{
    [SerializeField] private Image backgroundImage;
    [SerializeField] private Image skillImage;

    public void Init(SkillDetails skillDetails)
    {
        if (skillDetails == null)
        {
            backgroundImage.gameObject.SetActive(false);
            skillImage.gameObject.SetActive(false);

            return;
        }

        backgroundImage.sprite = skillDetails.skillImage;
        skillImage.sprite = skillDetails.skillImage;

        backgroundImage.gameObject.SetActive(true);
        skillImage.gameObject.SetActive(true);
    }

    public void UpdateFillAmount(float amount)
    {
        skillImage.fillAmount = amount;
    }
}