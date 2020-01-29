using System.Collections;
using System.Collections.Generic;
using Photon.Pun;
using UnityEngine;
using UnityEngine.Networking;

public class NonVRCharacterController : MonoBehaviourPun, IPunObservable
{

    public enum statusLayer
    {
        POISONED    =   0x1,
        IMMUNE      =   0x2,
        GOD         =   0x4
    };

    private uint status = 0;

    [Tooltip("The Health UI GO containing the scrollbar for health")]
    [SerializeField]
    private GameObject healthUIObject;

    public Score score;
    public Vector3 pig_pos;
    public float health = 100f;
    public static float moveSpeed = 5;
    public static float strength;
    public static float magic;
    public static float critical;
    public float turnSpeed = 50f;
    private string playerName;
    private bool isKeysEnabled = false;
    private float verticalAxis, horizontalAxis;

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

    }

    // Update is called once per frame
    void Update () {
        if(photonView.IsMine == false && PhotonNetwork.IsConnected == true)
        {
            return;
        }

        if (this.GetComponent<Collider>().enabled == false)
        {
            isKeysEnabled = false;
            
        }
        else
        {
            isKeysEnabled = true;
        }

        if (isKeysEnabled == true)
        {
            verticalAxis = Input.GetAxis("Vertical");
            horizontalAxis = Input.GetAxis("Horizontal");
            transform.Translate(Vector3.forward * moveSpeed * Time.deltaTime * verticalAxis);
            transform.Rotate(Vector3.up, turnSpeed * Time.deltaTime * horizontalAxis);
        }
        if (Input.GetKey(KeyCode.E) && this.GetComponent<Collider>().enabled == false)
        {
            GetComponentInParent<Pig_Controls>().enabled = false;
            isKeysEnabled = true;
            this.transform.position = new Vector3(-57.7f, 2.03f, -3.23f);
            transform.parent = null;
            this.GetComponent<Collider>().enabled = true;
            this.GetComponentInChildren<Camera>().enabled = true;
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        
        if (other.name.Equals("DodgeModel") || other.name.Equals("DodgeModifiersApplied_AddedInterior_11.16.19"))
        {
            // GetComponent<WeaponSystem>().LetGoOfWeapon();
            this.gameObject.SetActive(false);
            other.GetComponentInChildren<Camera>().enabled = true;
            other.GetComponent<New_Car_Con>().enabled = true;
        }
        if (other.name.Equals("Pig")) 
        {
            Debug.Log("collide (name) : " + other.gameObject.name);
            this.GetComponent<Collider>().enabled = false;
            this.GetComponent<Rigidbody>().isKinematic = true;
            this.GetComponentInChildren<Camera>().enabled = false;
            this.transform.parent = other.gameObject.transform;
            pig_pos = other.transform.position;
            this.transform.localPosition = new Vector3(0, pig_pos.y, 0);
            other.GetComponentInChildren<Camera>().enabled = true;
            other.GetComponent<Pig_Controls>().enabled = true;
        }
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
        if( (status & (uint) statusLayer.GOD) > 0)
        {
            return;
        }
        this.health -= amount;
    }

    public void AddScorePoints(int points)
    {
        score.ScoreValue += points;
    }

    public string PlayerName
    {
        get { return playerName; }
        set { playerName = value; }
    }

}
