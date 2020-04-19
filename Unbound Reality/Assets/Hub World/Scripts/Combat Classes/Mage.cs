using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;

public class Mage : BaseClass
{
    //Inputs
    private bool attackButtonPressed;
    private bool blockButtonHeld;

    //Animator Values
	static int idleState = Animator.StringToHash("Base Magic.Idle");
	static int attackState = Animator.StringToHash("Base Magic.Attack");
	static int locoState = Animator.StringToHash("Base Magic.Loco");
	static int blockStartState = Animator.StringToHash("Base Magic.Block Start");
    static int blockIdleState = Animator.StringToHash("Base Magic.Block Idle");
	static int hurtState = Animator.StringToHash("Base Magic.Hurt");
	private AnimatorStateInfo currentBaseState;

	[Tooltip("The animator controller specific to this class.")]
	[SerializeField]
	private RuntimeAnimatorController animatorController;

	public override void Awake()
	{
		base.Awake();
	}

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

        if(hurt)
        {
            anim.SetTrigger("Hurt");
            hurt = false;
            return;
        }

		anim.ResetTrigger("Hurt");

		// Let hurt and block start animations finish
		if(currentBaseState.fullPathHash == hurtState || currentBaseState.fullPathHash == blockStartState)
		{
			return;
		}

		attackButtonPressed = Input.GetKey(KeyCode.Mouse0);
		blockButtonHeld = Input.GetKey(KeyCode.E);

		if(!anim.IsInTransition(0) || !(currentBaseState.fullPathHash == attackState 
			|| currentBaseState.fullPathHash == hurtState
			|| currentBaseState.fullPathHash == locoState))
		{
			if(blockButtonHeld)
            {
                anim.SetBool("Blocking", true);
                return;
            }

            anim.SetBool("Blocking", false);

			if(skillPressed)
			{
				anim.SetInteger("AttackIndex", skillPressed.animationIndex);
				skillPressed.activated = false;
				return;
			}

			if(attackButtonPressed)
			{
				anim.SetInteger("AttackIndex", 0);
				return;
			}

			
		}

        base.FixedUpdate();


    }
}
