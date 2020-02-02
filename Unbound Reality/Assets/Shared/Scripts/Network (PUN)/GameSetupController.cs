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

        if(XRDevice.isPresent == true)
        {
            localPlayer = PhotonNetwork.Instantiate(Path.Combine("Prefabs", VRPrefab.name), spawnPosition, Quaternion.identity);
        } else 
        {
            localPlayer = PhotonNetwork.Instantiate(Path.Combine("Prefabs", nonVRPrefab.name), spawnPosition, Quaternion.identity);
        }

        PlayerManager.SetLocalPlayer(localPlayer);

        Camera.main.transform.position = localPlayer.transform.position - localPlayer.transform.forward*5 + localPlayer.transform.up*2;
        Camera.main.transform.SetParent(localPlayer.transform);
        
        Player player = localPlayer.GetComponent<Player>();
        if(player == null)
        {
            Debug.LogError("Could not find player script on prefab spawned");
            Application.Quit(-1);
        }
        player.playerName = PlayerPrefs.GetString("Name", "Dummy");
        player.spawnPoint = spawnPoint;

        //TODO: Implement scoreboard?
        localScore = PhotonNetwork.Instantiate(Path.Combine("Prefabs", textPrefab.name), Vector3.zero, Quaternion.identity);
        Score score = localScore.GetComponent<Score>();
        score.InitText(scoreContent, player.playerName.PadRight(maxNameLength));


    }
}
