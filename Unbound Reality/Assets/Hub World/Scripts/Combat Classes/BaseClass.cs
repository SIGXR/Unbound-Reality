using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;

[RequireComponent(typeof(Player))]
[RequireComponent(typeof(Animator))]
public class BaseClass : MonoBehaviour {
    
    //Public Variables
    public string className;
    public bool hurt;
    [Tooltip("Whether this class uses a weapon for it's animations")]
    [SerializeField]
    public bool usesWeapon;
	[Tooltip("The current skills assigned to this class")]
	[SerializeField]
	Skills[] skills;

    //Unity Components
	protected Player player;
	protected Animator anim;
	protected PhotonView pv;
    
    //Movement Values
	private float horizontalAxis, verticalAxis;
	private Vector3 velocity;

    public virtual void Awake()
	{
		player = GetComponent<Player>();
		anim = GetComponent<Animator>();
		pv = gameObject.GetPhotonView();
	}
    
    public virtual void OnEnable()
    {
        player.doFixedUpdate = false;
    }

    public virtual void OnDisable()
    {
        player.doFixedUpdate = true;
    }

    // This FixedUpdate() does the movement and camera rotation for all classes
    // Every class shares this functionality
    public virtual void FixedUpdate()
    {
        horizontalAxis = Input.GetAxis("Horizontal");
		verticalAxis = Input.GetAxis("Vertical");
		anim.SetFloat("Speed", verticalAxis);
		anim.SetFloat("Direction", horizontalAxis);
		velocity = Vector3.zero;

		if(Mathf.Abs(verticalAxis) > 0.1 || Mathf.Abs(horizontalAxis) > 0.1)
		{
            anim.SetBool("Moving", true);
			if(Mathf.Abs(verticalAxis) > Mathf.Abs(horizontalAxis))
			{
				velocity = player.gameObject.transform.forward;
				if(verticalAxis > 0.1)
				{
					velocity *= player.forwardSpeed;
				} else if(verticalAxis < -0.1)
				{
					velocity *= -player.backwardSpeed;
				}
			} else 
			{
				velocity = player.gameObject.transform.right;
				if(horizontalAxis > 0.1)
				{
					velocity *= player.forwardSpeed;
				} else if(horizontalAxis < -0.1)
				{
					velocity *= -player.backwardSpeed;
				}
			}
		} else
        {
            anim.SetBool("Moving", false);
			player.ableToSkill = true;
        }
		
		if(Input.GetMouseButton(2))
		{
			player.gameObject.transform.Rotate(0, Input.GetAxis("Mouse X"), 0);
		}
		player.gameObject.transform.position += velocity*Time.fixedDeltaTime;
    }

	public Skills skillPressed
	{
		get
		{
			foreach(Skills skill in skills)
			{
				if(skill.activated)
				{
					return skill;
				}
			}
			return null;
		}
	}

}
