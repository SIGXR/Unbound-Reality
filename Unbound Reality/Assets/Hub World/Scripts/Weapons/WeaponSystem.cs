using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;
using Photon.Pun;

public class WeaponSystem : MonoBehaviourPun {

    // Private data
    private Weapon weapon;
    private int weaponLayer;

    void Awake()
    {
        weaponLayer = LayerMask.NameToLayer("Weapon");
    }

	// Update is called once per frame
	void Update () {
        
        if(this.photonView.IsMine == false && PhotonNetwork.IsConnected == true)
        {
            return;
        }

        // If we have a weapon and the player right clicks, drop the weapon
        if (weapon!=null && Input.GetMouseButtonDown(1))
        {
            PhotonNetwork.Destroy(weapon.gameObject.GetPhotonView());
        }

	}

    [PunRPC]
    public void DropWeapon(int playerViewID, int weaponViewID)
    {
        GameObject player = PhotonView.Find(playerViewID).gameObject;
        GameObject weaponObject = PhotonView.Find(weaponViewID).gameObject;

        if(player == null || weaponObject == null)
        {
            return;
        }

        Weapon wep = weaponObject.GetComponent<Weapon>();
        wep.BeingUsed = false;

        if(player == gameObject)
        {
            weapon = null;
        }
    }

    //Defaulting the behaviour to spawning a copy of the weapon on the ground
    void OnCollisionEnter(Collision col)
    {
        if(this.photonView.IsMine == false && PhotonNetwork.IsConnected == true)
        {
            return;
        }

        if(weapon == null && col.gameObject.layer == weaponLayer && col.gameObject.transform.parent == null)
        {
            weapon = col.gameObject.GetComponent<Weapon>();
            GameObject weaponObj = PhotonNetwork.Instantiate(Path.Combine("Prefabs", weapon.prefabName), Vector3.zero, Quaternion.identity);
            weaponObj.transform.SetParent(transform);
            photonView.RPC("PickUpWeapon", RpcTarget.AllBuffered, this.photonView.ViewID, weaponObj.GetPhotonView().ViewID);
            Debug.Log("PickUpWeapon called on " + this.photonView.ViewID + " with " + weaponObj.GetPhotonView().ViewID);
        }

    }

    [PunRPC]
    public void PickUpWeapon(int playerViewID, int weaponViewID )
    {
        GameObject player = PhotonView.Find(playerViewID).gameObject;
        GameObject weaponObject = PhotonView.Find(weaponViewID).gameObject;

        if(player == null || weaponObject == null)
        {
            return;
        }

        Weapon wep = weaponObject.GetComponent<Weapon>();
        wep.BeingUsed = true;

        if(player == gameObject)
        {
            weapon = wep;
        }
    }

    // Get the Weapon variable
    public Weapon GetWeapon()
    {
        return weapon;
    }
}
