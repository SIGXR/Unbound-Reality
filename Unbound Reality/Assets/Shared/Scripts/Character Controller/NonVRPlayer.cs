using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;

public class NonVRPlayer : Player
{
    [Tooltip("Please drag the Inventory UI from Canvas")]
    [SerializeField]
    private GameObject inventoryUI;

    // Start is called before the first frame update
    public override void Start()
    {
        inventoryUI = GameObject.Find("Inventory UI");
        inventoryUI.SetActive(false);
        base.Start();
    }

    // Update is called once per frame
    public override void Update()
    {
        base.Update();
        if (Input.GetKeyDown(KeyCode.I))
        {
            inventoryUI.SetActive(!inventoryUI.activeSelf);
        }
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
        if(OnPlayerHealthChange == null)
        {
            return;
        }
        OnPlayerHealthChange(this);
    }
}
