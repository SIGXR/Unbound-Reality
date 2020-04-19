using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;
using System.IO;

[CreateAssetMenu(menuName = "Skills/SummonProjectile")]

public class SummonProjectile : Skills
{
    [Tooltip("The projectile prefab to spawn.\n"
        + "Make sure it is in Resources/Prefabs folder")]
    [SerializeField]
    GameObject projectile;

    private GameObject firedProjectile;
    
    public override void ActivateSkill()
    {
        base.ActivateSkill();
        Collider col = player.gameObject.GetComponent<Collider>();
        firedProjectile = PhotonNetwork.Instantiate(Path.Combine("Prefabs", projectile.name), player.transform.position+player.transform.forward*col.bounds.size.x*2, Quaternion.LookRotation(player.transform.forward, player.transform.up));
    }

}
