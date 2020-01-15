using Photon.Pun;
using UnityEngine;

//Use a callback function in this script which is called by QuickStartLobbyController.cs 
public class QuickStartRoomController : MonoBehaviourPunCallbacks
{
    [SerializeField]
    private int multiplayerSceneIndex;

    public override void OnEnable()
    {
        PhotonNetwork.AddCallbackTarget(this);
    }

    public override void OnDisable()
    {
        PhotonNetwork.RemoveCallbackTarget(this);
    }

    //Callback function for when we succesfully join or create a room
    public override void OnJoinedRoom()
    {
        Debug.Log("Joined Room");
        StartGame();
    }

    //Function for loading into the multiplayer scene
    private void StartGame()
    {
        if (PhotonNetwork.IsMasterClient)
        {
            Debug.Log("Starting Game");
            //Because we set the AutoSyncScene... all players who joins this room after mastesrclient has loaded multiplayerScene will also be loaded into said scene.
            PhotonNetwork.LoadLevel(multiplayerSceneIndex);
        }
    }
}
