using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;
using Photon.Pun;

public class WeaponSystem : MonoBehaviourPun {

    // Private data
    private Player player;
    private Weapon weapon;
    private int weaponLayer;
    const string SPINE = "Character1_Reference/Character1_Hips/Character1_Spine/Character1_Spine1/Character1_Spine2";
    const string LEFT_HAND = SPINE + "/Character1_LeftShoulder/Character1_LeftArm/Character1_LeftForeArm/Character1_LeftHand";
    const string RIGHT_HAND = SPINE + "/Character1_RightShoulder/Character1_RightArm/Character1_RightForeArm/Character1_RightHand";

    void Awake()
    {
        weaponLayer = LayerMask.NameToLayer("Weapon");
        player = GetComponent<Player>();
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
            DropClass(weapon.weaponType);
            PhotonNetwork.Destroy(weapon.gameObject.GetPhotonView());
        }

	}

    void DropClass(Weapon.WeaponType weaponType)
    {
        switch(weaponType)
        {
            case Weapon.WeaponType.BOW:
                GetComponent<Archer>().enabled = true;
                break;
            default:
                break;
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
            Transform hand = null;
            if(weapon.hand == "Right")
            {
                hand = transform.Find(RIGHT_HAND);
            } else if(weapon.hand == "Left")
            {
                hand = transform.Find(LEFT_HAND);
            }
            if(hand != null)
            {
                weaponObj.transform.SetParent(hand);
                weaponObj.transform.localPosition = new Vector3(0, 0, weaponObj.GetComponent<Collider>().bounds.size.y/2);
                weaponObj.transform.localRotation = Quaternion.Euler(hand.transform.up);
                //TODO: Fix this for sword
                weaponObj.GetComponent<Collider>().enabled = false;
            } else 
            {
                weaponObj.transform.SetParent(transform);
            }
            

            SetClass(weapon.weaponType);

            photonView.RPC("PickUpWeapon", RpcTarget.AllBuffered, this.photonView.ViewID, weaponObj.GetPhotonView().ViewID);
            Debug.Log("PickUpWeapon called on " + this.photonView.ViewID + " with " + weaponObj.GetPhotonView().ViewID);
        }

    }

    void SetClass(Weapon.WeaponType weaponType)
    {
        switch(weaponType)
        {
            case Weapon.WeaponType.BOW:
                GetComponent<Archer>().enabled = true;
                break;
            default:
                break;
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
