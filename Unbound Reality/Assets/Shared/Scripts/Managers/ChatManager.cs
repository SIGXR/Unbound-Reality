using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Photon.Pun;
using TMPro;

public class ChatManager : MonoBehaviourPun
{
    [SerializeField]
    private int maxMessages;
    [Tooltip("The scroll view that is responsible for the chat system")]
    [SerializeField]
    private GameObject chatScrollView;
    [Tooltip("The prefab for text that gets initialized for every message")]
    [SerializeField]
    private GameObject textPrefab;

    
    private Queue<GameObject> textQueue;
    private string localPlayerName;
    //The object to set the new text messages under (transform.setparent)
    private GameObject content;

    // Start is called before the first frame update
    void Start()
    {
        textQueue = new Queue<GameObject>(maxMessages);
        localPlayerName = PlayerPrefs.GetString("Name", "Dummy");
        content = chatScrollView.transform.GetChild(0).GetChild(0).gameObject;
        Application.runInBackground = true;
    }

    public void Message(TMP_InputField input)
    {
        if(input.text == "")
        {
            return;
        }

        string prettyText = localPlayerName + ": " + input.text;

        this.photonView.RPC("InternalMessage", RpcTarget.All, prettyText);
        input.text = "";
    }

    [PunRPC]
    void InternalMessage(string message)
    {
        if(textQueue.Count == maxMessages)
        {
            Destroy(textQueue.Dequeue());
        }
        GameObject newTextObj = Instantiate(textPrefab, Vector3.zero, Quaternion.identity, content.transform);
        newTextObj.GetComponent<TMP_Text>().text = message;
        newTextObj.transform.localPosition = Vector3.zero;
        newTextObj.transform.localRotation = Quaternion.identity;

        textQueue.Enqueue(newTextObj);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
