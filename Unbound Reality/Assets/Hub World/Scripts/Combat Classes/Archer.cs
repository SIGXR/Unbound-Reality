using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;

public class Archer : BaseClass {
	
	//Inputs
	private bool fireButtonHeld;

	//Animator Values
	static int idleState = Animator.StringToHash("Base Archer.Idle");
	static int fireState = Animator.StringToHash("Base Archer.Fire");
	static int fireLocoState = Animator.StringToHash("Base Archer.Fire Loco");
	static int locoState = Animator.StringToHash("Base Archer.Loco");
	private AnimatorStateInfo currentBaseState;
	
	[Tooltip("The animator controller specific to this class.")]
	[SerializeField]
	private RuntimeAnimatorController animatorController;

	public override void OnEnable()
	{
		base.OnEnable();
		anim.runtimeAnimatorController = animatorController;
	}

	public override void OnDisable() 
	{
		base.OnDisable();
		anim.runtimeAnimatorController = player.defaultAnimatorController;
	}

	public override void FixedUpdate() 
	{
		if(pv.IsMine == false && PhotonNetwork.IsConnected == true)
		{
			return;
		}

		// Set this to false unless proven otherwise
		player.ableToSkill = false;

		currentBaseState = anim.GetCurrentAnimatorStateInfo(0);

		// Check fire status before movement
		fireButtonHeld = Input.GetKey(KeyCode.Mouse0);
		anim.SetBool("FireButton", fireButtonHeld);
		if(!anim.IsInTransition(0))
		{
			if(fireButtonHeld && !(currentBaseState.fullPathHash == fireState || anim.GetBool("FireLoco")) )
			{
				anim.SetTrigger("Fire");
				return;
			} else 
			{
				anim.ResetTrigger("Fire");
			}
		}

		base.FixedUpdate();
	}


}
