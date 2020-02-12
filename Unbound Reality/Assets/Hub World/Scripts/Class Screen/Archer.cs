using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;

[RequireComponent(typeof(Player))]
[RequireComponent(typeof(Animator))]
public class Archer : MonoBehaviour {

	//Unity Components
	private Player player;
	private Animator anim;
	private PhotonView pv;

	//Movement Values
	private float horizontalAxis, verticalAxis;
	private Vector3 velocity;
	
	//Animator Values
	static int idleState = Animator.StringToHash("Base Layer.Idle");
	static int fireState = Animator.StringToHash("Base Layer.Fire");
	static int fireLocoState = Animator.StringToHash("Base Layer.Fire Loco");
	static int locoState = Animator.StringToHash("Base Layer.Loco");
	private AnimatorStateInfo currentBaseState;
	private bool fireButtonHeld;
	[Tooltip("The animator controller specific to this class.")]
	[SerializeField]
	private RuntimeAnimatorController animatorController;

	
	void Awake()
	{
		player = GetComponent<Player>();
		anim = GetComponent<Animator>();
		pv = gameObject.GetPhotonView();
	}

	void OnEnable()
	{
		player.doFixedUpdate = false;
		anim.runtimeAnimatorController = animatorController;
	}

	void OnDisable() 
	{
		player.doFixedUpdate = true;
		anim.runtimeAnimatorController = player.defaultAnimatorController;
	}

	void FixedUpdate() 
	{
		if(pv.IsMine == false && PhotonNetwork.IsConnected == true)
		{
			return;
		}

		currentBaseState = anim.GetCurrentAnimatorStateInfo(0);

		//Check fire status before movement
		fireButtonHeld = Input.GetKey(KeyCode.Alpha1);
		anim.SetBool("FireButton", fireButtonHeld);
		if(fireButtonHeld && (currentBaseState.fullPathHash != fireState || currentBaseState.fullPathHash != fireLocoState) )
		{
			Debug.Log("You have fired");
			anim.SetTrigger("Fire");
			return;
		} else 
		{
			anim.ResetTrigger("Fire");
		}

		horizontalAxis = Input.GetAxis("Horizontal");
		verticalAxis = Input.GetAxis("Vertical");
		anim.SetFloat("Speed", verticalAxis);
		anim.SetFloat("Direction", horizontalAxis);

		if(Mathf.Abs(verticalAxis) > Mathf.Abs(horizontalAxis))
		{
			velocity = new Vector3(0, 0, verticalAxis);
			if(verticalAxis > 0.1)
			{
				velocity *= player.forwardSpeed;
			} else if(verticalAxis < -0.1)
			{
				velocity *= player.backwardSpeed;
			}
			transform.Rotate(0, horizontalAxis*player.rotateSpeed, 0);
		} else 
		{
			velocity = new Vector3(horizontalAxis, 0, 0);
			if(horizontalAxis > 0.1)
			{
				velocity *= player.forwardSpeed;
			} else if(horizontalAxis < -0.1)
			{
				velocity *= player.backwardSpeed;
			}
		}

		transform.position += velocity*Time.fixedDeltaTime;




	}

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}
}
