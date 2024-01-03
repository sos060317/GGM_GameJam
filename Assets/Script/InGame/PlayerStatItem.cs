using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerStatItem : MonoBehaviour
{
    private void OnTriggerStay2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            if (Input.GetKeyDown(KeyCode.F))
            {
                GameManager.Instance.PlayerLevelUp();
                Destroy(gameObject);
            }
        }
    }
}