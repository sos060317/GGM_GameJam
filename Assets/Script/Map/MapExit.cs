using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MapExit : MonoBehaviour
{
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            MapManager.Instance.OpenMap();
        }
    }
}
