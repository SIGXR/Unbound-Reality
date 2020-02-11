using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using Photon.Pun;

public class Sword : Weapon {

    // Private data
    private int swinging;

    // Public Settings
    [Tooltip("Measured in degrees per second")]
    public float swingSpeed;
    [Tooltip("Measured in degrees")]
    public float range;

	// Reset the sword data everytime this object is enabled
	void OnEnable() {

        rb = GetComponent<Rigidbody>(); // Get the rigidbody
        swinging = 0;
	}
	
    private void FixedUpdate() {
        // If the sword is being used...
        if (beingUsed && gameObject.GetPhotonView().IsMine == true)
        {

            // Keep the sword to the players left with respect to where they are facing
            transform.position = playerTransform.position + playerTransform.right * .95f;

            /* If this weapon is a shotgun, point it in a direction such that a bullet when fired will reach a point range units
            in front of the player*/
            if (swinging==0)
                transform.eulerAngles = new Vector3(playerTransform.eulerAngles.x, playerTransform.eulerAngles.y+ 45f
                    ,playerTransform.eulerAngles.z);
            else
                transform.eulerAngles = new Vector3(transform.eulerAngles.x, playerTransform.eulerAngles.y + 45f,
                    transform.eulerAngles.z);

            // If the player right clicks on mouse, swing the sword
            if (Input.GetMouseButtonDown(0) && swinging==0 && swingSpeed != 0)
            {
                StartCoroutine(Swing());
            }

            // Swing the sword if necessary
            if (swinging == 1)
            {
                transform.RotateAround(transform.position, transform.forward, Time.deltaTime * swingSpeed); // Swing forward
            }
            else if (swinging == 2)
            {
                transform.RotateAround(transform.position, transform.forward, Time.deltaTime * -swingSpeed); // Swing backward
            }
        }
    }

	// Update is called once per frame
	void Update () {
        
    }

    void OnCollisionEnter(Collision other) {
        if(gameObject.GetPhotonView().IsMine == true)
        {
            if(other.gameObject.tag == "Player" && other.gameObject != player.gameObject)
            {
                other.gameObject.GetComponent<Player>().DamagePlayer(damage);
            } else if(other.gameObject.tag == "Enemy")
            {
                other.gameObject.GetComponent<EnemyController>().DamageEnemy(damage);
            }
        }
    }

    // Start the sequence of events for a sword swing
    IEnumerator Swing()
    {
        letGoHold = true;

        //Compute how long it will take for the sword to rotate range degrees at an angular speed of swingSpeed
        float time;
        time = range / swingSpeed;

        swinging = 1;
        
        yield return new WaitForSeconds(time);
        swinging = 2;
        yield return new WaitForSeconds(time);
        swinging = 0;

        letGoHold = false;
    }

    // Get or set the Speed variable
    public float SwingSpeed
    {
        get { return swingSpeed; }
        set { swingSpeed = value; }
    }

    // Get or set the Range variable
    public float Range
    {
        get { return range; }
        set { range = value; }
    }
}
