using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using Photon.Pun;

public class EnemyController : MonoBehaviourPun {

    public float lookRadius = 5f;

    [Tooltip("The amount of damge this slime does on collision")]
    [SerializeField]
    private int damage;
    [Tooltip("How long to wait when its in farStopDistance")]
    [SerializeField]
    private float waitSeconds;
    [Tooltip("How far out of target when it starts waiting")]
    [SerializeField]
    private float farStopDistance;
    private float closeStopDistance;
    private bool hasStopped = false;
    Transform target;
    NavMeshAgent agent;

	// Use this for initialization
	void Start () {
        
        agent = GetComponent<NavMeshAgent>();
        closeStopDistance = agent.stoppingDistance;
        agent.stoppingDistance = farStopDistance;
	}
	
	// Update is called once per frame
	void Update () {
        if (PlayerManager.instance.players.Length == 0)
        {
            return;
        }
        if(this.photonView.IsMine == false && PhotonNetwork.IsConnected == true )
        {
            return;
        }

        target = PlayerManager.instance.players[0].transform;

        float distance = Vector3.Distance(target.position, transform.position);

        if(distance <= lookRadius)
        {
            agent.SetDestination(target.position);

            if(distance <= agent.stoppingDistance)
            {
                if(!hasStopped)
                {
                    StartCoroutine(Stahp());
                    agent.stoppingDistance = closeStopDistance;
                    hasStopped = true;
                }
                FaceTarget();
            }
        }
	}

    //Plez
    IEnumerator Stahp()
    {

        yield return new WaitForSeconds(waitSeconds);
    }

    void FaceTarget()
    {
        Vector3 direction = (target.position - transform.position).normalized;
        Quaternion lookRotation = Quaternion.LookRotation(new Vector3(direction.x, 0, direction.z));
        transform.rotation = Quaternion.Slerp(transform.rotation, lookRotation, Time.deltaTime * 5f);
            
    }

    void OnCollisionEnter(Collision other) {
        if(this.photonView.IsMine == false && PhotonNetwork.IsConnected == true)
        {
            return;
        }
        if(other.gameObject.tag == "Player")
        {
            hasStopped = false;
            agent.stoppingDistance = farStopDistance;
            other.gameObject.GetComponent<NonVRCharacterController>().DamagePlayer(damage);
            Debug.Log("Damaged player");
        }
    }

    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, lookRadius);
    }
}
