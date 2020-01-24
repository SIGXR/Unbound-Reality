using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;

public class Shotgun : Weapon {

    // Public data
    public GameObject bullet;

    // Public settings
    [Tooltip("Measured in units per second")]
    public float speed;
    [Tooltip("Measured in units")]
    public float range;
    public int ammo;
	
	// Update is called once per frame
	void Update () {

        // If the shotgun is being used...
        if (beingUsed && gameObject.GetPhotonView().IsMine == true)
        {
            // Keep the shotgun to the players left with respect to where they are facing
            transform.position = playerTransform.position + playerTransform.right * .95f;

            /* Point the weapon at the position that is range units directly in front of the player with respect
            to where they are facing*/
            float offset = 0;
            offset = 90 - Mathf.Atan(range / .9f) * Mathf.Rad2Deg;
            transform.eulerAngles = new Vector3(playerTransform.eulerAngles.x, playerTransform.eulerAngles.y + 90 - offset
                , playerTransform.eulerAngles.z);

            // If the player right clicks on mouse and there is ammo, fire a bullet
            if (Input.GetMouseButtonDown(0) && ammo > 0)
            {
                GameObject bulletInstance = Instantiate(bullet, transform.position, Quaternion.identity);
                bulletInstance.GetComponent<Bullet>().Speed = speed;
                bulletInstance.transform.eulerAngles = transform.eulerAngles;
                ammo--;
            }
        }
    }

    // Get or set the ammo variable
    public int Ammo
    {
        get { return ammo; }
        set { ammo = value; }
    }

    // Get or set the Range variable
    public float Range
    {
        get { return range; }
        set { range = value; }
    }

    // Get or set the Speed variable
    public float Speed
    {
        get { return speed; }
        set { speed = value; }
    }
}
