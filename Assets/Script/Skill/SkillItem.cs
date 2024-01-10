using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class SkillItem : MonoBehaviour
{
    public SkillDetails[] skillDetails;
    public TextMeshProUGUI priceText;
    public GameObject explanationObj;
    public TextMeshProUGUI explanationText;

    private SkillDetails skill;
    private SpriteRenderer sr;

    private bool isEnter;

    private void OnEnable()
    {
        Init();
    }

    private void Init()
    {
        sr = GetComponent<SpriteRenderer>();

        skill = skillDetails[Random.Range(0, skillDetails.Length)];

        sr.sprite = skill.skillImage;

        priceText.text = skill.price.ToString() + "$";

        explanationObj.SetActive(false);
        explanationText.text = skill.explanation;
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.F) && isEnter && GameManager.Instance.gold >= skill.price && !GameManager.Instance.isSkillFull)
        {
            GameManager.Instance.gold -= skill.price;
            GameManager.Instance.GetSkillItem(skill, gameObject);
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            isEnter = true;
            explanationObj.SetActive(true);
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            isEnter = false;
            explanationObj.SetActive(false);
        }
    }
}