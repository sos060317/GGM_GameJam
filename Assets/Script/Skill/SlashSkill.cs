using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SlashSkill : SkillBase
{
    public float skillTime;
    public PlayerBullet bulletPrefab;

    private SkillDetails skill;

    public override void UseSkill(SkillDetails skillDetails)
    {
        skill = skillDetails;
        StartCoroutine(SlashSkillRoutine());
    }

    private IEnumerator SlashSkillRoutine()
    {
        GameManager.Instance.curPlayer.sword.attackEvent += ShootBullet;
    }

    private void ShootBullet(float damage)
    {

    }
}