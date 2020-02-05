using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerManager : MonoBehaviour {

    #region Singleton

    public static PlayerManager instance;
    public static GameObject localPlayer;

    void Awake()
    {
        instance = this;
    }

    public static void SetLocalPlayer(GameObject player)
    {
        localPlayer = player;
    }

    #endregion

    public GameObject[] players;

    void Update()
    {
        players = GameObject.FindGameObjectsWithTag("Player");
    }
}
