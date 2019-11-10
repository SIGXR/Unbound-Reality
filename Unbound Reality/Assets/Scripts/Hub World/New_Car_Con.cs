using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class New_Car_Con : MonoBehaviour
{
    public float m_horizontalInput, m_verticalInput, m_steeringAngle;
    public float maxSteerAngle = 30;
    public float motorForce, steerForce, brakeForce;
    public WheelCollider FR_L_Wheel, FR_R_Wheel, RE_L_Wheel, RE_R_Wheel;

    private void Update()
    {
        m_verticalInput = Input.GetAxis("Vertical");
        m_horizontalInput = Input.GetAxis("Horizontal");
        Accelerate();
        Steer();
        Brake();
    }

    private void Accelerate()
    {
        FR_L_Wheel.motorTorque = m_verticalInput * motorForce;
        FR_R_Wheel.motorTorque = m_verticalInput * motorForce;
        RE_L_Wheel.motorTorque = m_verticalInput * motorForce;
        RE_R_Wheel.motorTorque = m_verticalInput * motorForce;
    }

    private void Steer()
    {
        m_steeringAngle = maxSteerAngle * m_horizontalInput;
        FR_L_Wheel.steerAngle = m_steeringAngle;
        FR_R_Wheel.steerAngle = m_steeringAngle;
    }

    private void Brake()
    {
        if (Input.GetKey(KeyCode.Space))
        {
            Debug.Log("Breaking");
            RE_L_Wheel.brakeTorque = brakeForce;
            RE_R_Wheel.brakeTorque = brakeForce;
        }
        else if (Input.GetKeyUp(KeyCode.Space))
        {
            RE_L_Wheel.brakeTorque = 0;
            RE_R_Wheel.brakeTorque = 0;
        }
        //if (m_horizontalInput == 0)
        //{
        //    RE_L_Wheel.brakeTorque = BrakeForce;
        //    RE_R_Wheel.brakeTorque = BrakeForce;
        //}
        //else
        //{
        //RE_L_Wheel.brakeTorque = 0;
        //RE_R_Wheel.brakeTorque = 0;
        //}
    }
}
