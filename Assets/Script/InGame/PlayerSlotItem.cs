using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class PlayerSlotItem : MonoBehaviour
{
    public int price;

    public TextMeshProUGUI priceText;
    public GameObject e;

    private bool isEnter;

    private void Start()
    {
        // E 스킬이 해금되어있다면 삭제
        if (GameManager.Instance.isCanESkill)
        {
            Destroy(gameObject);
        }

        e.SetActive(false);
        priceText.text = price.ToString() + "$";
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.F) && isEnter && GameManager.Instance.gold >= price)
        {
            GameManager.Instance.SkillSlotUpgrade();
            GameManager.Instance.gold -= price;
            Destroy(gameObject);
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            isEnter = true;
            e.SetActive(true);
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            isEnter = false;
            e.SetActive(false);
        }
    }
}