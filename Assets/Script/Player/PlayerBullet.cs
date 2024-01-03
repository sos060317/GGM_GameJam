using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerBullet : MonoBehaviour
{
    [SerializeField] private bool isEnemyPenetrate; // 적 관통
    [SerializeField] private bool isWallPenetrate;  // 벽 관통
}