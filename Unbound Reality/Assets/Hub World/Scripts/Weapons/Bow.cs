using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;
using System.IO;

public class Bow : Weapon
{

    [Tooltip("The projectile that will be fired by the bow")]
    [SerializeField]
    private GameObject projectile;
    [Tooltip("The amount of time to wait between shots")]
    [Range(0.2f, 1f)]
    [SerializeField]
    private float firedFrequency;

    private GameObject firedProjectile;
    private float firedTimeLeft = 0;

    // Start is called before the first frame update
    void Start()
    {
        rb.useGravity = false;
        supportedClasses = new System.Type[]{ typeof(Archer) };
    }

    void FixedUpdate() {
        if(beingUsed && gameObject.GetPhotonView().IsMine)
        {
            firedTimeLeft -= Time.deltaTime;
            if(Input.GetKeyUp(KeyCode.Mouse0) && firedTimeLeft < 0)
            {
                firedProjectile = PhotonNetwork.Instantiate(Path.Combine("Prefabs", projectile.name), transform.position+-transform.right*col.bounds.size.z*2, Quaternion.LookRotation(-transform.right, -transform.forward));
                firedTimeLeft = firedFrequency;
            }
            transform.localPosition = new Vector3(0, 0, col.bounds.size.y/2);
        }        
    }

    // Update is called once per frame
    void Update()
    {
        
    }


}
