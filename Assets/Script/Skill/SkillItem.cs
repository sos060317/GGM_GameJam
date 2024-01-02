using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SkillItem : MonoBehaviour
{
    public SkillDetails skillDetails;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            GameManager.Instance.GetSkillItem(skillDetails);
            Destroy(gameObject);
        }
    }
}