using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour {

    // Declare variables:
    private Rigidbody rb;
    private float speed = 0;

    // Get or set the beingUsed variable
    public float Speed
    {
        get { return speed; }
        set { speed = value; }
    }

    // Use this for initialization
    void Start () {
        // Shoot the bullet at a speed of x units per second (x determined by speed variable in inspector on shotgun)
        rb = GetComponent<Rigidbody>();
        rb.velocity = transform.right * -speed;
        StartCoroutine(Destroyin5());
	}

    // Destroy the bullet in 5 seconds
    IEnumerator Destroyin5()
    {
        yield return new WaitForSeconds(5);
        Destroy(gameObject);
    }
}
