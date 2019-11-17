using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR;

public class DisableNonVRCharacterController : MonoBehaviour {

	// Use this for initialization
	void Start () {
        if (XRDevice.isPresent)
        {
            this.gameObject.SetActive(false);
        }
    }
}
