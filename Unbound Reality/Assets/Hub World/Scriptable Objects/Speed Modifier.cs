using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
[CreateAssetMenu (menuName = "Skills/Speed")]
public class SpeedModifier : Skills
{
    public float speedModifier = 2;
    private bool status = false;
    private float time;
    private float activeRate;

    // Start is called before the first frame update
    void Start()
    {
        activeRate = ActivationTime;
    }

    // Update is called once per frame
    void Update()
    {
        if (Time.time > time && status == true)
        {
            NonVRCharacterController.moveSpeed /= speedModifier;
            status = false;
        }
    }
    public override void Initialize(GameObject obj)
    {

    }

    public override void ActivateSkill()
    {
        time = Time.time + activeRate;
        Debug.Log(time);
        NonVRCharacterController.moveSpeed *= speedModifier;
        status = true;
    }
}
