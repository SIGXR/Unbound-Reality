using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Pig_Controls : MonoBehaviour {
    public float moveSpeed = 10f;
    public float turnSpeed = 50f;

    private float fob, rol;
    // Use this for initialization
    void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
        fob = Input.GetAxis("Vertical");
        rol = Input.GetAxis("Horizontal");
        transform.Translate(Vector3.forward * moveSpeed * Time.deltaTime * fob);
        transform.Rotate(Vector3.up, turnSpeed * Time.deltaTime * rol);
    }

}
