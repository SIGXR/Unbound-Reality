using System.Collections;
using System.Collections.Generic;
using Photon.Pun;
using UnityEngine;

public class CameraWork : MonoBehaviourPun
{

    [Tooltip("Set this as false if a component of a prefab being instanciated by Photon Network, and manually call OnStartFollowing() when and if needed.")]
    [SerializeField]
    private bool followOnStart = true;

    // cached transform of the target
    Transform cameraTransform;

    // maintain a flag internally to reconnect if target is lost or camera is switched
    bool isFollowing;

    // Start is called before the first frame update
    void Start()
    {
        CameraWork cameraWork = GetComponent<CameraWork>();

        if(cameraWork != null)
        {
            if(photonView.IsMine)
            {
                cameraWork.OnStartFollowing();
            }
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnStartFollowing()
    {
        cameraTransform = Camera.main.transform;
        isFollowing = true;

        Camera.main.transform.SetParent(transform);
        Camera.main.transform.localPosition = -transform.forward*2 + transform.up*2;
        Camera.main.transform.localRotation = Quaternion.Euler(30,0,0);

    }

}
