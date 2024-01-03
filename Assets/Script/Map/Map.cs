using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Map : MonoBehaviour
{
    public Transform enterPoint;

    public MapState mapState;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            MapManager.Instance.OpenMap();
        }
    }
}

public enum MapState
{
    normal = 0,
    oxygen,
    wind
}
