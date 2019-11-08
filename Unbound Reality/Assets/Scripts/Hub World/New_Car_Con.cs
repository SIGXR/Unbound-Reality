using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class New_Car_Con : MonoBehaviour
{
    public float MotorForce, SteerForce, BrakeForce;
    public WheelCollider FR_L_Wheel, FR_R_Wheel, RE_L_Wheel, RE_R_Wheel;

    private void Start()
    {
        
    }

    private void Update()
    {
        float v = Input.GetAxis("Vertical") * MotorForce;
        float h = Input.GetAxis("Horizontal") * SteerForce;

       RE_L_Wheel.motorTorque = v;
       RE_R_Wheel.motorTorque = v;

       FR_L_Wheel.motorTorque = h;
       FR_R_Wheel.motorTorque = h;

        if (Input.GetKey(KeyCode.Space))
        {
            RE_L_Wheel.brakeTorque = BrakeForce;
            RE_R_Wheel.brakeTorque = BrakeForce;
        }

        if (Input.GetKeyUp(KeyCode.Space))
        {
            RE_L_Wheel.brakeTorque = 0;
            RE_R_Wheel.brakeTorque = 0;
        }

        if (Input.GetAxis("Vertical") == 0)
        {
           RE_L_Wheel.brakeTorque = BrakeForce;
           RE_R_Wheel.brakeTorque = BrakeForce;
        }
        else
        {
           RE_L_Wheel.brakeTorque = 0;
           RE_R_Wheel.brakeTorque = 0;
        }
    }
}
