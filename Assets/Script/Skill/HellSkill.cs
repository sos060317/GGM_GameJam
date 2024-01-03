using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HellSkill : SkillBase
{
    public PlayerBullet bulletPrefab;

    public int bulletCount;

    private SkillDetails skill;

    public override void UseSkill(SkillDetails skillDetails)
    {
        skill = skillDetails;
        GameManager.Instance.ShowEffectImage(0.3f, 1);
        StartCoroutine(BulletHell());
    }

    private IEnumerator BulletHell()
    {
        float temp = 0;

        for (int i = 0; i < bulletCount; i++)
        {
            Vector2 dir = new Vector2(Mathf.Cos(temp), Mathf.Sin(temp));

            dir.Normalize();

            float angle = Mathf.Atan2(dir.y, dir.x) * Mathf.Rad2Deg;

            Instantiate(bulletPrefab, GameManager.Instance.curPlayer.transform.position, Quaternion.Euler(0, 0, angle)).Init(skill.damage);

            temp += 0.5f;

            yield return new WaitForSeconds(0.05f);
        }
    }
}