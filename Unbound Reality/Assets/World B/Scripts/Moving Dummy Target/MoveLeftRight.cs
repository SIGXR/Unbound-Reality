using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoveLeftRight : MonoBehaviour {

    [SerializeField]
    public float movementMultiplier;

    // Use this for initialization
    void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
        Vector3 moveLeft = new Vector3(-1, 0, 0);

        moveRight();
    }

    void moveRight()
    {
        
        Vector3 moveRight = new Vector3(Mathf.Sin(Time.time) * movementMultiplier, 0, 0);
        transform.Translate(moveRight * Time.deltaTime);
    }
}
