using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FireBallSkill : SkillBase
{
    [SerializeField] private PlayerBullet bulletPrefab;

    public override void UseSkill(SkillDetails skillDetails)
    {
        for (int i = 0; i < 6; i++)
        {
            Vector2 dir = new Vector2(Mathf.Cos(Mathf.PI * 2 * i / 6),
                                      Mathf.Sin(Mathf.PI * 2 * i / 6));

            float angle = Mathf.Atan2(dir.y, dir.x) * Mathf.Rad2Deg;

            Instantiate(bulletPrefab, GameManager.Instance.curPlayer.transform.position, Quaternion.Euler(0, 0, angle)).Init(skillDetails.damage);
        }

        GameManager.Instance.CameraShake(20, 2f);
        GameManager.Instance.ShowEffectImage(0.5f, 1);
    }
}