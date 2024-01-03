using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RushSkill : SkillBase
{
    public override void UseSkill(SkillDetails skillDetails)
    {
        GameManager.Instance.curPlayer.RushSkill(this, skillDetails.damage);
    }
}