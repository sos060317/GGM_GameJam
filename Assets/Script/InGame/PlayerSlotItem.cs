using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerSlotItem : MonoBehaviour
{
    private bool isEnter;

    private void Start()
    {
        // E ��ų�� �رݵǾ��ִٸ� ����
        if (GameManager.Instance.isCanESkill)
        {
            Destroy(gameObject);
        }
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.F) && isEnter)
        {
            GameManager.Instance.SkillSlotUpgrade();
            Destroy(gameObject);
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