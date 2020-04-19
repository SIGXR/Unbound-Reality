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
    static int blockState = Animator.StringToHash("Base Magic.Block Idle");
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

		currentBaseState = anim.GetCurrentAnimatorStateInfo(0);

        if(hurt)
        {
            anim.SetTrigger("Hurt");
            hurt = false;
            return;
        } else
        {
            anim.ResetTrigger("Hurt");
        }

        //Check fire status before movement
		attackButtonPressed = Input.GetKey(KeyCode.Mouse0);
        if(!anim.IsInTransition(0))
		{
			if(attackButtonPressed && !(currentBaseState.fullPathHash == attackState || currentBaseState.fullPathHash == blockState) )
			{
				anim.SetTrigger("Fire");
				return;
			} else 
			{
				anim.ResetTrigger("Fire");
			}
		}

        blockButtonHeld = Input.GetKey(KeyCode.E);
        if(!anim.IsInTransition(0))
        {
            if(blockButtonHeld)
            {
                anim.SetBool("Blocking", true);
                return;
            } else
            {
                anim.SetBool("Blocking", false);
            }
        }

        base.FixedUpdate();


    }
}
