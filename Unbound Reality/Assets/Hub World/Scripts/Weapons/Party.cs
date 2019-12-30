using UnityEngine;

[RequireComponent(typeof(MeshFilter))]
public class Party : Weapon {

    public enum partyType {
        Rock,   //Cube
        Paper,  //Cylinder
        Scissor //Sphere
    };

    private partyType status = partyType.Rock;

    [SerializeField]
    private Mesh cubeMesh, cylinderMesh, sphereMesh;

    private MeshFilter meshFilter;
    

    protected override void Awake() {
        meshFilter = gameObject.GetComponent<MeshFilter>();
        base.Awake(); 
    }

    void Update() {
        if(beingUsed)
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
        if(beingUsed)
        {
            transform.position = playerTransform.position + playerTransform.right * .95f;
        }
    }
    
    private void OnCollisionEnter(Collision other) {
        if(other.gameObject.tag == "Party" && other.gameObject.name != "Spawner")
        {
            partyType otherStatus = other.gameObject.GetComponent<Party>().Status;
            if(Status == otherStatus)
            {
                //Do Nothing
            } else if(Status == partyType.Paper && otherStatus == partyType.Rock)
            {
                Destroy(other.gameObject);
            } else if(Status == partyType.Rock && otherStatus == partyType.Scissor)
            {
                Destroy(other.gameObject);
            } else if(Status == partyType.Scissor && otherStatus == partyType.Paper)
            {
                Destroy(other.gameObject);
            }
        }
    }

    public partyType Status
    {
        get { return status;}
        set { status = value;
            switch(status) 
            {
                case partyType.Rock:
                    meshFilter.mesh = cubeMesh;
                    break;
                case partyType.Paper:
                    meshFilter.mesh = cylinderMesh;
                    break;
                case partyType.Scissor:
                    meshFilter.mesh = sphereMesh;
                    break;
            }
        }
    }

}