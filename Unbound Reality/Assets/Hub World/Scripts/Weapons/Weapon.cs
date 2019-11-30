using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Weapon : MonoBehaviour {

    // Declare variables:
    private Rigidbody rb;
    private bool beingUsed = false; //Represents weather the weapon is being used or not
    private Transform playerTransform;

    // Public settings
    public GameObject bullet;
    public float range;
    public float speed;
    public int ammo = 0;

	// Use this for initialization
	void Start () {
        rb = gameObject.GetComponent<Rigidbody>();

        // Rotate the shotgun at a rate of -90 degrees per second around the y axis
        rb.angularVelocity = new Vector2(0f, -Mathf.PI/2);
	}
	
	// Update is called once per frame
	void Update () {

        // If the object is being used...
        if (beingUsed)
        {
            // keep the shotgun to the players left
            transform.position = playerTransform.position + playerTransform.right * .9f;

            /* if this weapon is a shot shotgun, point it in a direction such that a bullet when fired will reach a point range units
            in front of the player*/
            float offset = 0;
            offset = 90 - Mathf.Atan(range / .9f) * Mathf.Rad2Deg;
            transform.eulerAngles = new Vector2(playerTransform.eulerAngles.x, playerTransform.eulerAngles.y + 90 - offset);

            //If the player right clicks on mouse and there is ammo, fire a bullet
            if (Input.GetMouseButtonDown(0) && ammo>0)
            {
                GameObject bulletInstance = Instantiate(bullet, transform.position, Quaternion.identity);
                bulletInstance.GetComponent<Bullet>().Speed = speed;
                bulletInstance.transform.eulerAngles = transform.eulerAngles;
                ammo--;
            }
        }
	}

    // Get or set the beingUsed variable
    public bool BeingUsed
    {
        get { return beingUsed; }
        set { beingUsed = value;
            if (value)
            {
                // If the shotgun is being used, force stop any linear or angular velocity
                rb.angularVelocity = new Vector2();
                rb.velocity = new Vector3();
            }
            else
                // If the shotgun is putdown, rotate the shotgun at a rate of -90 degrees per second around the y axis
                rb.angularVelocity = new Vector2(0f, -Mathf.PI/2);
        }
    }

    // Get or set the ammo variable
    public int Ammo
    {
        get { return ammo; }
        set { ammo = value; }
    }

    // Gets the player transform
    public void GetPlayerTransform(GameObject obj)
    {
        playerTransform = obj.transform;
    }
}
