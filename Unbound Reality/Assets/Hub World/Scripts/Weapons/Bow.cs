using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;

public class Bow : Weapon
{

    [Tooltip("The projectile that will be fired by the bow")]
    [SerializeField]
    private GameObject projectile;
    [Tooltip("The amount of time to wait between shots")]
    [Range(0.2f, 1f)]
    [SerializeField]
    private float firedFrequency;

    private float firedTimeLeft = 0;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    void FixedUpdate() {
        if(beingUsed && gameObject.GetPhotonView().IsMine)
        {
            firedTimeLeft -= Time.deltaTime;
            if(Input.GetKey(KeyCode.Alpha1) && firedTimeLeft < 0)
            {
                //TODO: Network maybe?
                Instantiate(projectile);
                firedTimeLeft = firedFrequency;
            }
        }        
    }

    // Update is called once per frame
    void Update()
    {
        
    }


}
