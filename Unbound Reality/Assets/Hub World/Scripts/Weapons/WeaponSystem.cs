using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeaponSystem : MonoBehaviour {

    // Private data
    private Weapon weapon;
	
	// Update is called once per frame
	void Update () {
        
        // If we have a weapon and the player left clicks, put down the weapon
        if (weapon!=null && Input.GetMouseButtonDown(1) && !weapon.LetGoHold)
        {
            LetGoOfWeapon();
        }

	}

    public void LetGoOfWeapon()
    {
        if(weapon.GetType() == typeof(Party))
        {
            Destroy(weapon);
            weapon = null;
        } else {
            weapon.LetGoHold = false;
            weapon.BeingUsed = false;
            weapon.gameObject.GetComponent<BoxCollider>().enabled = true;
            weapon = null;
        }
        
    }

    // Check for collisions
    void OnCollisionEnter(Collision col)
    {
        /* If we collided with a weapon, and the player is not holding a weapon, pickup 
          the weapon. And the weapon doesn't have an owner */
        if (weapon == null && col.gameObject.layer == LayerMask.NameToLayer("Weapon") && col.gameObject.transform.parent == null){
            if(col.gameObject.tag == "Party")
            {
                GameObject go = Instantiate(col.gameObject, col.gameObject.transform.position, Quaternion.identity);
                // go.GetComponent<BoxCollider>().enabled = false;
                weapon = go.GetComponent<Weapon>();
                weapon.GetPlayerTransform(gameObject);
                weapon.BeingUsed = true;
            } else {
                col.gameObject.GetComponent<BoxCollider>().enabled = false;
                weapon = col.gameObject.GetComponent<Weapon>();
                weapon.GetPlayerTransform(gameObject);
                weapon.BeingUsed = true;
            }
            
        }
    }

    // Get the Weapon variable
    public Weapon GetWeapon()
    {
        return weapon;
    }
}
