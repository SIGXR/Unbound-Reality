using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR;

public class DisableVRCharacterController : MonoBehaviour
{
    [SerializeField]
    public Camera nonVRCamera;

	// Use this for initialization
	void Start ()
    {
        if (!XRDevice.isPresent)
        {
            Debug.Log("I couldn't find a HMD!!!");
            this.gameObject.GetComponent<OVRPlayerController>().enabled = false;
            this.gameObject.GetComponent<OVRSceneSampleController>().enabled = false;
            this.gameObject.GetComponent<OVRDebugInfo>().enabled = false;
            this.gameObject.GetComponent<CharacterCameraConstraint>().enabled = false;
            nonVRCamera.enabled = false;
        }
        else
        {
            Debug.Log("I found a HMD!!!");
        }
    }

    private void Update()
    {
        if (Input.GetKey(KeyCode.A))
        {
            transform.Translate(new Vector3(0,0,-1 * Time.deltaTime));
        }
        if (Input.GetKey(KeyCode.D))
        {
            transform.Translate(new Vector3(0, 0, 1 * Time.deltaTime));
        }
        if (Input.GetKey(KeyCode.W))
        {
            transform.Translate(new Vector3(-1 * Time.deltaTime, 0, 0));
        }
        if (Input.GetKey(KeyCode.S))
        {
            transform.Translate(new Vector3(1 * Time.deltaTime, 0, 0));
        }
    }
}