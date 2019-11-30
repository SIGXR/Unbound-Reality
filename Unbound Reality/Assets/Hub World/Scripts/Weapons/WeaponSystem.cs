using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeaponSystem : MonoBehaviour {

    //Initialize variables
    private Weapon weapon;

	// Use this for initialization
	void Start () {

	}
	
	// Update is called once per frame
	void Update () {
        
        // If we have a weapon and the player left clicks, put down the weapon
        if (weapon!=null && Input.GetMouseButtonDown(1))
        {
            weapon.BeingUsed = false;
            weapon.gameObject.GetComponent<BoxCollider>().enabled = true;
            weapon = null;

        }

	}

    // Check for collisions
    void OnCollisionEnter(Collision col)
    {
        /* If we collided with a weapon, and the player is not holding a weapon, pickup 
          the weapon. */
        if (weapon == null && col.gameObject.layer == LayerMask.NameToLayer("Weapon")){
            col.gameObject.GetComponent<BoxCollider>().enabled = false;
            weapon = col.gameObject.GetComponent<Weapon>();
            weapon.GetPlayerTransform(gameObject);
            weapon.BeingUsed = true;
            
        }
    }
}
