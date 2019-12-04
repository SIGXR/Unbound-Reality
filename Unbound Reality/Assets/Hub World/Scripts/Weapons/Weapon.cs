using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Weapon : MonoBehaviour {

    // Private data (private with respect to the inspector)
    [HideInInspector]
    public Rigidbody rb;
    [HideInInspector]
    public bool beingUsed; //Represents weather the weapon is being used or not
    [HideInInspector]
    public Transform playerTransform;
    [HideInInspector]
    public string propersceneName;
    [HideInInspector]
    public bool letGoHold; // If set to true, a hold will be placed on allowing the player to let go of their weapon

    // Use this for initialization
    void Start () {

        rb = GetComponent<Rigidbody>(); // Get the rigidbody

        // Get the int of the scene that this object is supposed to be in
       propersceneName = SceneManager.GetActiveScene().name;

        // Set up the required data so we can call the OnSceneLoaded function
        SceneManager.sceneLoaded += OnSceneLoaded;

        beingUsed = false;
	}

    // Called everytime a scene has loaded
    void OnSceneLoaded(Scene scene, LoadSceneMode mode)
    {
        // Destroy this object if it loads in a scene whose build index does not match the sceneIndex
        if (propersceneName != null && propersceneName != SceneManager.GetActiveScene().name)
            Destroy(gameObject);
    }

    // Get or set the beingUsed variable
    public bool BeingUsed
    {
        get { return beingUsed; }
        set { beingUsed = value;
            if (value)
            {
                // Force stop any linear or angular velocity when the weapon is picked up
                rb.angularVelocity = Vector3.zero;
                rb.velocity = Vector3.zero;

                // Turn gravity off for this weapon so gravity has no effect while the player is holding the weapon
                rb.useGravity = false; 

                // Set the parent of this weapon's transform to the player's transform
                gameObject.transform.SetParent(playerTransform);
            }
            else
            {
                // Unparent the weapon's transform with the player's transform
                gameObject.transform.SetParent(null);

                // Turn gravity back on
                rb.useGravity = true;
            }
                
        }
    }

    // Get or set the LetGoHold variable
    public bool LetGoHold
    {
        get { return letGoHold; }
        set { letGoHold = value; }
    }

    // Gets the player transform
    public void GetPlayerTransform(GameObject obj)
    {
        playerTransform = obj.transform;
    }
}
