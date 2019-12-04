using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;

public class NonVRCharacterController : NetworkBehaviour
{
    public float moveSpeed = 10f;
    public float turnSpeed = 50f;

    private float verticalAxis, horizontalAxis;

    private void Start()
    {
        if (!isLocalPlayer)
        {
            transform.GetChild(0).gameObject.SetActive(false);
        }
    }

    // Update is called once per frame
    void Update () {
        if (!isLocalPlayer)
        {
            return;
        }
        verticalAxis = Input.GetAxis("Vertical");
        horizontalAxis = Input.GetAxis("Horizontal");
        transform.Translate(Vector3.forward * moveSpeed * Time.deltaTime * verticalAxis);
        transform.Rotate(Vector3.up, turnSpeed * Time.deltaTime * horizontalAxis);
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.name.Equals("DodgeModel") || other.name.Equals("DodgeModifiersApplied_AddedInterior_11.16.19"))
        {
            GetComponent<WeaponSystem>().LetGoOfWeapon();
            this.gameObject.SetActive(false);
            other.GetComponentInChildren<Camera>().enabled = true;
            other.GetComponent<New_Car_Con>().enabled = true;
        }
    }
}
