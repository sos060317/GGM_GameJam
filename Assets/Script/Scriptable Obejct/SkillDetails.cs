using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "SkillData_", menuName = "ScriptableObject/SkillData")]
public class SkillDetails : ScriptableObject
{
    public Sprite skillImage;

    public float coolTime;

    public float price;

    public float damage;

    public SkillBase skillPrefab;
}
