using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;

public class NonVRPlayer : Player
{
    // Start is called before the first frame update
    public override void Start()
    {
        base.Start();
        
    }

    // Update is called once per frame
    public override void Update()
    {
        base.Update();

    }

    public override void FixedUpdate()
    {
        base.FixedUpdate();

    }

    [PunRPC]
    void InternalDamagePlayer(int playerID, float amount)
    {
        if(this.photonView.ViewID != playerID)
        {
            return;
        }
        if( (status & (uint) StatusLayer.GOD) > 0)
        {
            return;
        }
        this.health -= amount;
        OnPlayerHealthChange(this);
    }
}
