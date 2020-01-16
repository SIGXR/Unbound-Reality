using Photon.Pun;
using System.IO;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.XR;

public class GameSetupController : MonoBehaviour {

    private GameObject localPlayer;
    private GameObject localScore;
    [Tooltip("The point where new players spawn")]
    [SerializeField]
    private GameObject spawnPoint;
    [Tooltip("The content container gameobject from the score scroll view")]
    [SerializeField]
    private GameObject scoreContent;
    [Tooltip("The Prefab for nonvr character controller")]
    [SerializeField]
    private GameObject nonVRPrefab;
    [Tooltip("The Prefab for vr character controller")]
    [SerializeField]
    private GameObject VRPrefab;
    [Tooltip("The prefab for text")]
    [SerializeField]
    private GameObject textPrefab;
    [Tooltip("The length of maximum characters allowed in a player name. Used for padding in score system")]
    [SerializeField]
    private int maxNameLength;

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

        localPlayer = PhotonNetwork.Instantiate(Path.Combine("Prefabs", nonVRPrefab.name), spawnPosition, Quaternion.identity);
        localScore = PhotonNetwork.Instantiate(Path.Combine("Prefabs", textPrefab.name), Vector3.zero, Quaternion.identity);

        NonVRCharacterController nvcc = localPlayer.GetComponent<NonVRCharacterController>();
        nvcc.PlayerName = PlayerPrefs.GetString("Name", "Dummy");

        Score score = localScore.GetComponent<Score>();
        score.InitText(scoreContent, nvcc.PlayerName.PadRight(maxNameLength));
        
        nvcc.score = score;

    }
}
