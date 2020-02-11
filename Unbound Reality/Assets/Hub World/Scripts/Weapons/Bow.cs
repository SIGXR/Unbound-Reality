using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;

public class Bow : Weapon
{

    // Start is called before the first frame update
    void Start()
    {
        
    }

    void FixedUpdate() {
        if(beingUsed && gameObject.GetPhotonView().IsMine)
        {
            
        }        
    }

    // Update is called once per frame
    void Update()
    {
        
    }


}
