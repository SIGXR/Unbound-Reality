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

    // The time before able to activate the skill
    protected float nextActivationTime = 0f;

    // If the skill is available to use
    [HideInInspector]
    public bool available = true;
    [HideInInspector]
    public bool activated = false;

    public virtual void Update()
    {
        if(!available)
        {
            if(Time.time > nextActivationTime)
            {
                available = true;
            }
        }
    }

    public virtual void ActivateSkill()
    {
        if(available)
        {
            nextActivationTime = Time.time + coolDownTime;
            available = false;
            activated = true;
        }
    }
}
