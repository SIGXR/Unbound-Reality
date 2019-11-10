using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NonVRCharacterController : MonoBehaviour
{
    public float moveSpeed = 10f;
    public float turnSpeed = 50f;

    private float verticalAxis, horizontalAxis;
	
	// Update is called once per frame
	void Update () {
        verticalAxis = Input.GetAxis("Vertical");
        horizontalAxis = Input.GetAxis("Horizontal");
        transform.Translate(Vector3.forward * moveSpeed * Time.deltaTime * verticalAxis);
        transform.Rotate(Vector3.up, turnSpeed * Time.deltaTime * horizontalAxis);
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.name.Equals("DodgeModel"))
        {
            this.gameObject.SetActive(false);
            other.GetComponentInChildren<Camera>().enabled = true;
            other.GetComponent<New_Car_Con>().enabled = true;
        }
    }
}
