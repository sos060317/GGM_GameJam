using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SkillItem : MonoBehaviour
{
    public SkillDetails skillDetails;

    private bool isEnter;

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