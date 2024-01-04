using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class PlayerStatItem : MonoBehaviour
{
    public TextMeshProUGUI priceText;
    public TextMeshProUGUI explanationText;
    public GameObject textBG;

    private int price;

    private bool isEnter;

    private void Start()
    {
        // 최대레벨이면 삭제
        if (GameManager.Instance.curPlayerLevel >= GameManager.Instance.playerLevelDatas.Length)
        {
            Destroy(gameObject);
        }

        Init();
    }

    private void Init()
    {
        priceText.text = GameManager.Instance.playerLevelDatas[GameManager.Instance.curPlayerLevel].price.ToString() + "$";
        price = (int)GameManager.Instance.playerLevelDatas[GameManager.Instance.curPlayerLevel].price;

        explanationText.text = GameManager.Instance.playerLevelDatas[GameManager.Instance.curPlayerLevel].explanation;

        textBG.SetActive(false);
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.F) && isEnter && GameManager.Instance.gold >= price)
        {
            GameManager.Instance.gold -= price;
            GameManager.Instance.PlayerLevelUp();
            Destroy(gameObject);
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            isEnter = true;
            textBG.SetActive(true);
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            isEnter = false;
            textBG.SetActive(false);
        }
    }
}