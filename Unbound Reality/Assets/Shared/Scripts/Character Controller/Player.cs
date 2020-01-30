using System;
using System.Collections;
using System.Collections.Generic;
using Photon.Pun;
using UnityEngine;
using UnityEngine.Networking;

[RequireComponent(typeof(Animator))]
[RequireComponent(typeof(CapsuleCollider))]
[RequireComponent(typeof(Rigidbody))]
public class Player : MonoBehaviourPun, IPunObservable
{

    public enum StatusLayer
    {
        POISONED    =   0x1,
        IMMUNE      =   0x2,
        GOD         =   0x4
    };

    private uint status = 0;

    public enum Attributes
    {
        STRENGTH,
        MAGIC,
        CRITICAL
    }

    //The values of attribute list. Access via attributeList[(int) Attributes.VALUE]
    private int[] attributeList  = new int[Enum.GetNames(typeof(Attributes)).Length];

    //Player Specific UI
    [Tooltip("The Health UI GO containing the scrollbar for health")]
    [SerializeField]
    private GameObject healthUIObject;

    //Character Info
    public float health = 100f;
    public string playerName;

    //Unity Components
    private Rigidbody rb;
    private CapsuleCollider col;
    private Animator anim;

    //Movement Values
    public float forwardSpeed = 7.0f;
    public float backwardSpeed = 2.0f;
    public float rotateSpeed = 2.0f;
    public float jumpPower = 3.0f;
    private float verticalAxis, horizontalAxis;
    private Vector3 velocity;
    private float orgColHeight;
    private Vector3 orgVectColCenter;

    //Animator Values
    static int idleState = Animator.StringToHash ("Base Layer.Idle");
    static int locoState = Animator.StringToHash ("Base Layer.Locomotion");
    static int jumpState = Animator.StringToHash ("Base Layer.Jump");
    static int restState = Animator.StringToHash ("Base Layer.Rest");
    private AnimatorStateInfo currentBaseState;
    public float animSpeed = 1.5f;
    public float lookSmoother = 3.0f;
    public float useCurvesHeight = 0.5f;
    public bool useCurves = true;

    public void OnPhotonSerializeView(PhotonStream stream, PhotonMessageInfo info)
    {
        if(stream.IsWriting)
        {
            stream.SendNext(health);
            stream.SendNext(status);
        } else 
        {
            this.health = (int) stream.ReceiveNext();
            this.status = (uint) stream.ReceiveNext();
        }
    }

    private void Start()
    {
        if(photonView.IsMine == false && PhotonNetwork.IsConnected == true)
        {
            return;
        }
        healthUIObject = GameObject.Find("Health Slider");
        HealthUI healthUI = healthUIObject.GetComponent<HealthUI>();
        healthUI.player = this;

        //transform.GetChild(0).gameObject.SetActive(false);
        rb = gameObject.GetComponent<Rigidbody>();
        col = GetComponent<CapsuleCollider>();
        rb = GetComponent<Rigidbody>();
    }

    // Update is called once per frame
    void Update () {
        if(photonView.IsMine == false && PhotonNetwork.IsConnected == true)
        {
            return;
        }

    }

    private void OnCollisionEnter(Collision collision)
    {
        if (photonView.IsMine == false && PhotonNetwork.IsConnected == true)
        {
            return;
        }
    }

    private void OnTriggerEnter(Collider other)
    {

    }

    public void DamagePlayer(int amount)
    {
        photonView.RPC("InternalDamagePlayer", RpcTarget.All, photonView.ViewID, amount);
    }

    [PunRPC]
    void InternalDamagePlayer(int playerID, int amount)
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
    }

}
