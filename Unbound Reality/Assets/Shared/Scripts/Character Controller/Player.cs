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

    protected uint status = 0;

    public enum Attributes
    {
        STRENGTH,
        MAGIC,
        CRITICAL
    }

    //The values of attribute list. Access via attributeList[(int) Attributes.VALUE]
    protected int[] attributeList  = new int[Enum.GetNames(typeof(Attributes)).Length];

    //Player Specific UI
    [Tooltip("The Health UI Script")]
    [SerializeField]
    private HealthUI healthUI;
    [Tooltip("The Deauth UI Script")]
    [SerializeField]
    private DeathUI deathUI;

    //World Values
    [Tooltip("The position of where the player needs to spawn")]
    [SerializeField]
    public GameObject spawnPoint;

    //Delegates (AKA EventBasedCoding)
    public delegate void PlayerDeathDelegate(Player player);
    public delegate void PlayerHealthDelegate(Player player);
    public static PlayerDeathDelegate OnPlayerDeath;
    public static PlayerHealthDelegate OnPlayerHealthChange;

    //Character Info
    public float health = 100f;
    public float healthMax = 100f;
    public float healthReset = 100f;
    public string playerName;

    //Unity Components
    protected Rigidbody rb;
    protected CapsuleCollider col;
    protected Animator anim;

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
    public RuntimeAnimatorController defaultAnimatorController;
    public float animSpeed = 1.5f;
    public float lookSmoother = 3.0f;
    public float useCurvesHeight = 0.5f;
    public bool useCurves = true;

    //Extra
    public bool doFixedUpdate = true;

    public void OnPhotonSerializeView(PhotonStream stream, PhotonMessageInfo info)
    {
        if(stream.IsWriting)
        {
            stream.SendNext(health);
            stream.SendNext(status);
        } else 
        {
            this.health = (float) stream.ReceiveNext();
            this.status = (uint) stream.ReceiveNext();
        }
    }

    public virtual void Start()
    {
        if(photonView.IsMine == false && PhotonNetwork.IsConnected == true)
        {
            return;
        }
        
        healthUI = FindObjectOfType<HealthUI>();
        OnPlayerHealthChange += healthUI.OnPlayerHealthChange;

        deathUI = FindObjectOfType<DeathUI>();
        OnPlayerDeath += deathUI.OnPlayerDeath;

        rb = GetComponent<Rigidbody>();
        col = GetComponent<CapsuleCollider>();
        anim = GetComponent<Animator>();

        orgColHeight = col.height;
        orgVectColCenter = col.center;

        defaultAnimatorController = anim.runtimeAnimatorController;

        DontDestroyOnLoad(this.gameObject);
        
    }

    // Update is called once per frame
    public virtual void Update () {
        if(photonView.IsMine == false && PhotonNetwork.IsConnected == true)
        {
            return;
        }

    }

    public virtual void FixedUpdate() {
        if(photonView.IsMine == false && PhotonNetwork.IsConnected == true)
        {
            return;
        }

        if(health <= 0)
        {
            if(OnPlayerDeath != null)
            {
                OnPlayerDeath(this);
            }
            
            health = healthReset;
            if(OnPlayerHealthChange != null)
            {
                OnPlayerHealthChange(this);
            }
            
            anim.SetTrigger("Dead");
            transform.position = spawnPoint.transform.position;
        } else 
        {
            anim.ResetTrigger("Dead");
        }

        if(doFixedUpdate == false)
        {
            return;
        }
        horizontalAxis = Input.GetAxis("Horizontal");
        verticalAxis = Input.GetAxis("Vertical");

        anim.SetFloat("Speed", verticalAxis);
        anim.SetFloat("Direction", horizontalAxis);
        anim.speed = animSpeed;
        currentBaseState = anim.GetCurrentAnimatorStateInfo(0);
        rb.useGravity = true;

        velocity = new Vector3(0, 0, verticalAxis);
        velocity = transform.TransformDirection(velocity);
        if(verticalAxis > 0.1)
        {
            velocity *= forwardSpeed;
        } else if(verticalAxis < -0.1)
        {
            velocity *= backwardSpeed;
        }

        if(Input.GetButtonDown("Jump"))
        {
            if(currentBaseState.fullPathHash == locoState)
            {
                if(!anim.IsInTransition(0))
                {
                    rb.AddForce(Vector3.up * jumpPower, ForceMode.VelocityChange);
                    anim.SetBool("Jump", true);
                }
            }
        }

        transform.localPosition += velocity * Time.fixedDeltaTime;
        transform.Rotate(0, horizontalAxis*rotateSpeed, 0);

        if(currentBaseState.fullPathHash == locoState)
        {
            if(useCurves)
            {
                //Reset Collider
                col.height = orgColHeight;
                col.center = orgVectColCenter;
            }
        } else if(currentBaseState.fullPathHash == jumpState)
        {
            if(!anim.IsInTransition(0))
            {
                if(useCurves)
                {
                    float jumpHeight = anim.GetFloat("JumpHeight");
                    float gravityControl = anim.GetFloat("GravityControl");
                    if(gravityControl > 0)
                    {
                        rb.useGravity = false;
                    }
                    Ray ray = new Ray(transform.position + Vector3.up, -Vector3.up);
                    RaycastHit hitInfo = new RaycastHit();
                    if(Physics.Raycast(ray, out hitInfo))
                    {
                        if(hitInfo.distance > useCurvesHeight)
                        {
                            col.height = orgColHeight - jumpHeight;
                            float adjCenterY = orgVectColCenter.y + jumpHeight;
                            col.center = new Vector3(0, adjCenterY, 0);
                        } else 
                        {
                            col.height = orgColHeight;
                            col.center = orgVectColCenter;
                        }
                    }
                }
                anim.SetBool("Jump", false);
            }
        } else if(currentBaseState.fullPathHash == idleState)
        {
            if(useCurves)
            {
                col.height = orgColHeight;
                col.center = orgVectColCenter;
            }
            if(Input.GetButtonDown("Jump"))
            {
                anim.SetBool("Rest", true);
            }
        } else if(currentBaseState.fullPathHash == restState)
        {
            if(!anim.IsInTransition(0))
            {
                anim.SetBool("Rest", false);
            }
        }

    }

    public virtual void OnCollisionEnter(Collision collision)
    {
        if (photonView.IsMine == false && PhotonNetwork.IsConnected == true)
        {
            return;
        }

    }

    public virtual void OnTriggerEnter(Collider other)
    {

    }

    //Internal Damage Player has to be defined in the child classes because PUN doesn't do inheritance.
    public void DamagePlayer(float amount)
    {
        photonView.RPC("InternalDamagePlayer", RpcTarget.All, photonView.ViewID, amount);
    }

}
