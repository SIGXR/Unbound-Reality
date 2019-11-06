using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NonVRCharacterController : MonoBehaviour
{
    public float moveSpeed = 10f;
    public float turnSpeed = 50f;
	
	// Update is called once per frame
	void Update () {
        if (Input.GetKey(KeyCode.UpArrow))
            transform.Translate(Vector3.forward * moveSpeed * Time.deltaTime);

        if (Input.GetKey(KeyCode.DownArrow))
            transform.Translate(-Vector3.forward * moveSpeed * Time.deltaTime);

        if (Input.GetKey(KeyCode.LeftArrow))
            transform.Rotate(Vector3.up, -turnSpeed * Time.deltaTime);

        if (Input.GetKey(KeyCode.RightArrow))
            transform.Rotate(Vector3.up, turnSpeed * Time.deltaTime);
        Gizmos.DrawRay(transform.position, Vector3.forward);
    }
}
