using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerManager : MonoBehaviour {

    #region Singleton

    public static PlayerManager instance;

    void Awake()
    {
        instance = this;
    }


    #endregion

    public GameObject[] players;

    void Update()
    {
        players = GameObject.FindGameObjectsWithTag("Player");
    }
}
