using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Photon.Pun;

[RequireComponent(typeof(Text))]
public class Score : MonoBehaviourPun, IPunObservable
{
    private GameObject content;
    private Text text;
    private string playerName;
    private int scoreValue;

    public void OnPhotonSerializeView(PhotonStream stream, PhotonMessageInfo info)
    {
        if(stream.IsWriting)
        {
            stream.SendNext(ScoreValue);
        } else
        {
            this.ScoreValue = (int) stream.ReceiveNext();
        }
    }

    public void InitText(GameObject contentContainer, string name)
    {
        playerName = name;
        content = contentContainer;
        transform.SetParent(content.transform);
        transform.localPosition = Vector3.zero;
        transform.localRotation = Quaternion.Euler(Vector3.zero);
        transform.localScale = Vector3.one;
        text = GetComponent<Text>();
        ScoreValue = 0;

    }

    public int ScoreValue
    {
        get { return scoreValue; }
        set 
        {
            scoreValue = value;
            text.text = playerName + "| " + scoreValue;
        }
    } 


}
