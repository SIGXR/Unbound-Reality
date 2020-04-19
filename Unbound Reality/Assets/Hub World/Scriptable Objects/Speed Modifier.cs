using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Skills/Speed")]

public class SpeedModifier : Skills
{
    public float speedModifier = 2;
    public float previousSpeed;

    // Start is called before the first frame update
    void Start()
    {
            
    }

    public override void ActivateSkill()
    {
        if(available)
        {
            base.ActivateSkill();
        }
    }
}
