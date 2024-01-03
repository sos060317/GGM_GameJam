using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TestSkill01 : SkillBase
{
    public GameObject testBullet;

    public override void UseSkill(SkillDetails skillDetails)
    {
        int bulletCount = 3;

        for (int i = 0; i < bulletCount; i++)
        {
            Vector2 dir = new Vector2(Mathf.Cos(Mathf.PI * 2 * i / bulletCount),
                                      Mathf.Sin(Mathf.PI * 2 * i / bulletCount));

            float angle = Mathf.Atan2(dir.y, dir.x) * Mathf.Rad2Deg;
            
            Instantiate(testBullet, GameManager.Instance.curPlayer.transform.position, Quaternion.Euler(0, 0, angle));
        }
    }
}