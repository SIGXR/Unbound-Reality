using Photon.Pun;
using System.IO;
using UnityEngine;
using UnityEngine.XR;

public class GameSetupController : MonoBehaviour {

    private GameObject localPlayer;
    [SerializeField]
    private GameObject spawnPoint;

	// Use this for initialization
	void Start ()
    {
        if(localPlayer == null)
        {
            CreatePlayer();
        }
	}

    //Creates a networked player object for each player that loads into the multiplayer room
    private void CreatePlayer()
    {
        Vector3 spawnPosition;
        Debug.Log("Creating Player");

        if(spawnPoint == null)
        {
            spawnPosition = Vector3.zero;
        } else {
            spawnPosition = spawnPoint.transform.position;
        }

        localPlayer = PhotonNetwork.Instantiate(Path.Combine("Prefabs", "Non VR Character Controller"), spawnPosition, Quaternion.identity);
        
        if(XRDevice.isPresent)
        {
            //Load player name from VRCharacterController
        } else
        {
            NonVRCharacterController nvcc = localPlayer.GetComponent<NonVRCharacterController>();
            nvcc.PlayerName = PlayerPrefs.GetString("Name", "Dummy");
        }

    }
}
