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

        yield return new WaitForSeconds(skillTime);

        GameManager.Instance.curPlayer.sword.attackEvent -= ShootBullet;
    }

    private void ShootBullet()
    {
        Vector2 mousePos = Input.mousePosition;
        Vector3 mouseWorldPos = Camera.main.ScreenToWorldPoint(mousePos);

        Vector2 dir = mouseWorldPos - GameManager.Instance.curPlayer.transform.position;

        float angle = Mathf.Atan2(dir.y, dir.x) * Mathf.Rad2Deg;

        Instantiate(bulletPrefab, GameManager.Instance.curPlayer.transform.position, Quaternion.Euler(0, 0, angle)).Init(skill.damage);
    }
}