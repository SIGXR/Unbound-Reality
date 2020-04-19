using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Skills : ScriptableObject
{
    [Tooltip("The name of the skill.")]
    [SerializeField]
    public string skillName;
    [Tooltip("What the skill does")]
    [SerializeField]
    public string description;
    [Tooltip("The amount of time from activation before you can use this skill again.")]
    [SerializeField]
    public float coolDownTime;
    [Tooltip("The animation index from the sanimator (linked to classes).")]
    [SerializeField]
    public int animationIndex;
    [HideInInspector]
    public Player player;

    // If the skill has been activated
    [HideInInspector]
    public bool activated = false;

    //Skill time management done through the UI
    public virtual void ActivateSkill()
    {
        activated = true;
    }

}
