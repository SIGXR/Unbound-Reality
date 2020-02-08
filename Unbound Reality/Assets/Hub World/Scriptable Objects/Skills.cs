using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Skills : ScriptableObject
{
    public string SkillName;
    public float CoolDownTime;
    public string Description;

    public abstract void Initialize(GameObject obj);
    public abstract void ActivateSkill();
}
