using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AI_Party : MonoBehaviour
{
    [SerializeField]
    public GameObject spawnPoint;
    [SerializeField]
    public GameObject partyPrefab;
    [SerializeField]
    public float spawnInterval = 5f;

    private float timeLeft = 0;
    private GameObject partyObject;

    // Start is called before the first frame update
    void Start()
    {
        if(spawnPoint == null)
        {
            this.enabled = false;
        }
    }

    // Update is called once per frame
    void Update()
    {
        if(partyObject == null)
        {
            timeLeft -= Time.deltaTime;
            if(timeLeft < 0)
            {
                partyObject = Instantiate(partyPrefab, spawnPoint.transform.position, Quaternion.identity, transform);
                Party party = partyObject.GetComponent<Party>();
                party.Status = (Party.partyType) Random.Range(0, System.Enum.GetNames(typeof(Party.partyType)).Length);
                timeLeft = spawnInterval;
            }
        }
        
    }

    void FixedUpdate() {
        if(partyObject != null)
        {
            partyObject.transform.position = spawnPoint.transform.position;
        }
    }
}
