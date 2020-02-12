using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;

public class Projectile : MonoBehaviourPun
{
    [Tooltip("The amount of damage the projectile does to other entity on colllision.")]
    public float damage;
    [Tooltip("The speed of the projectile")]
    public float speed = 1;
    [Tooltip("The amount of time the projectile is allowed to be alive.")]
    [SerializeField]
    private float lifeTime;

    private Vector3 velocity;

    void Start()
    {
        velocity = transform.forward*speed;
    }

    void FixedUpdate()
    {
        transform.position += velocity;
    }

    void OnCollisionEnter(Collision other)
    {
        if(gameObject.GetPhotonView().IsMine == false && PhotonNetwork.IsConnected == true)
        {
            return;
        }
        if(other.transform == transform.root)
        {
            return;
        }
        if(other.gameObject.tag == "Player")
        {
            other.gameObject.GetComponent<Player>().DamagePlayer(damage);
        }

        Destroy(gameObject);       

    }
}
