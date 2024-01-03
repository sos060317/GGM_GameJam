using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "PlayerLevelData_", menuName = "ScriptableObject/PlayerLevelData")]
public class PlayerLevelData : ScriptableObject
{
    public float health;

    public float damage;

    public float moveSpeed;
}