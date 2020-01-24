using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Skills : ScriptableObject
{
    public string SkillName = "Test Skill";
    public float CoolDownTime = 1f;

    public abstract void Initialize(GameObject obj);
    public abstract void ActivateSkill();
}
