using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR;

public class DisableVRCharacterController : MonoBehaviour
{
    // Use this for initialization
    void Start ()
    {
        if (!XRDevice.isPresent)
        {
            Debug.Log("I couldn't find a HMD!!!");
            this.gameObject.SetActive(false);
        }
        else
        {
            Debug.Log("I found a HMD!!!");
        }
    }
}