using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[CreateAssetMenu (menuName = "Skills/testing")]
public class TestSkill : Skills
{
    public override void Initialize(GameObject obj)
    {

    }

    public override void ActivateSkill()
    {
        Debug.Log("Skill Activated");
    }
}
