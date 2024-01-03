using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class SkillItem : MonoBehaviour
{
    public SkillDetails skillDetails;
    public TextMeshProUGUI priceText;

    private bool isEnter;

    private void Start()
    {
        Init();
    }

    private void Init()
    {
        priceText.text = skillDetails.price.ToString() + "$";
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.F) && isEnter)
        {
            GameManager.Instance.GetSkillItem(skillDetails, gameObject);
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            isEnter = true;
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            isEnter = false;
        }
    }
}