using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerStatItem : MonoBehaviour
{
    private bool isEnter;

    private void Start()
    {
        // �ִ뷹���̸� ����
        if (GameManager.Instance.curPlayerLevel >= GameManager.Instance.playerLevelDatas.Length)
        {
            Destroy(gameObject);
        }
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.F) && isEnter)
        {
            GameManager.Instance.PlayerLevelUp();
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