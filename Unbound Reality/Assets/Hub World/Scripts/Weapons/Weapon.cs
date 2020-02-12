using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Weapon : MonoBehaviour {

    public enum WeaponType
    {
        NONE,
        BOW,
        SWORD_1H,
        SWORD_2H,
        STAFF,
        PARTY
    };

    [Tooltip("How the player should handle the weapon. Used during pickup of weapon.")]
    public WeaponType weaponType;
    [Tooltip("Which hand the weapon should be placed on (Right/Left).")]
    public string hand = "Right";

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
    [HideInInspector]
    public Vector3 spawn;
    [HideInInspector]
    public Player player;

    [Tooltip("The amount of damage the weapon deals")]
    public float damage;
    [Tooltip("The prefab name of this weapon")]
    public string prefabName;

    // Use this for initialization
    protected virtual void Awake () {

        rb = GetComponent<Rigidbody>(); // Get the rigidbody
        spawn = transform.position;

        // Get the int of the scene that this object is supposed to be in
       propersceneName = SceneManager.GetActiveScene().name;

        // Set up the required data so we can call the OnSceneLoaded function
        SceneManager.sceneLoaded += OnSceneLoaded;

        beingUsed = false;

        if(prefabName == "")
        {
            prefabName = gameObject.name;
        }
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
                playerTransform = gameObject.transform.parent;

                player = playerTransform.gameObject.GetComponent<Player>();

            }
            else
            {

                // Turn gravity back on
                rb.useGravity = true;

                playerTransform = null;
            }
                
        }
    }

    
}
