using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;

public class ScoreManager : MonoBehaviourPun
{
    private int maxPlayers = 1;
    public static PhotonView pv;
    private static List<Score> scoreList;

    void Awake() 
    {
        pv = photonView;
    }

    // Start is called before the first frame update
    void Start()
    {
        if(PhotonNetwork.CurrentRoom != null)
        {
            maxPlayers = PhotonNetwork.CurrentRoom.MaxPlayers;
        }
        scoreList = new List<Score>(maxPlayers);
    }

    [PunRPC]
    public void AddScore(Score score)
    {
        scoreList.Add(score);
    }

    private static int CompareScores(Score a, Score b)
    {
        return a.ScoreValue.CompareTo(b.ScoreValue);
    }

    public static void UpdateScoreList()
    {
        scoreList.Sort(CompareScores);
    }

    // Update is called once per frame
    void Update()
    {

    }
}
