using UnityEngine;
using Photon.Pun;

[RequireComponent(typeof(MeshFilter))]
public class Party : Weapon, IPunObservable {

    public enum partyType {
        Rock,   //Cube
        Paper,  //Cylinder
        Scissor //Sphere
    };

    private partyType status = partyType.Rock;

    [SerializeField]
    private Mesh rockMesh, paperMesh, scissorMesh;

    private MeshFilter meshFilter;
    

    protected override void Awake() {
        meshFilter = gameObject.GetComponent<MeshFilter>();
        base.Awake(); 
    }

    public void OnPhotonSerializeView(PhotonStream stream, PhotonMessageInfo info)
    {
        if(stream.IsWriting)
        {
            stream.SendNext(status);
        } else 
        {
            this.status = (partyType) stream.ReceiveNext();
        }
    }

    void Update() {
        if(beingUsed && gameObject.GetPhotonView().IsMine == true)
        {
            if(Input.GetMouseButtonDown(0))
            {
                int stat = (int) Status;
                stat++;
                if(stat > System.Enum.GetNames(typeof(partyType)).Length - 1)
                {
                    stat = 0;
                }
                Status = (partyType) stat;


                
            }
        }
        
    }

    void FixedUpdate() {
        if(beingUsed && gameObject.GetPhotonView().IsMine == true)
        {
            transform.position = playerTransform.position + playerTransform.right * .95f;
        }
    }
    
    private void OnCollisionEnter(Collision other) {
        if(gameObject.GetPhotonView().IsMine == true)
        {
            if(other.gameObject.tag == "Party" && other.gameObject.name != "Spawner")
            {
                partyType otherStatus = other.gameObject.GetComponent<Party>().Status;
                if(Status == otherStatus)
                {
                    //Do Nothing
                } else if(Status == partyType.Paper && otherStatus == partyType.Rock)
                {
                    gameObject.GetPhotonView().RPC("DestroyParty", RpcTarget.All, other.gameObject.GetPhotonView().ViewID);
                } else if(Status == partyType.Rock && otherStatus == partyType.Scissor)
                {
                    gameObject.GetPhotonView().RPC("DestroyParty", RpcTarget.All, other.gameObject.GetPhotonView().ViewID);
                } else if(Status == partyType.Scissor && otherStatus == partyType.Paper)
                {
                    gameObject.GetPhotonView().RPC("DestroyParty", RpcTarget.All, other.gameObject.GetPhotonView().ViewID);
                }
            }
        }
        
    }

    [PunRPC]
    public void DestroyParty(int viewID)
    {
        PhotonView pv = PhotonView.Find(viewID);

        if(pv.IsMine == true)
        {
            PhotonNetwork.Destroy(pv);
        }
    }

    public partyType Status
    {
        get { return status;}
        set { status = value;
            switch(status) 
            {
                case partyType.Rock:
                    meshFilter.mesh = rockMesh;
                    break;
                case partyType.Paper:
                    meshFilter.mesh = paperMesh;
                    break;
                case partyType.Scissor:
                    meshFilter.mesh = scissorMesh;
                    break;
            }
        }
    }

}