using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyHealth : MonoBehaviour
{
    [Tooltip("How often to update the UI of the enemy. Default: 0.137")]
    [SerializeField]
    private float UI_TICK = 0.137f;
    public float health;
    private float healthMax;
    private Vector3 scaleInit;
    private float timeLeft = 0;

    // Start is called before the first frame update
    void Start()
    {
        healthMax = health;
        scaleInit = transform.localScale;
    }

    // Update is called once per frame
    void Update()
    {
        timeLeft -= Time.deltaTime;
        if(timeLeft < UI_TICK)
        {
            FacePlayer();
            timeLeft = UI_TICK;
        }
    }

    void FacePlayer()
    {
        Vector3 direction = (Camera.main.transform.position - transform.position).normalized;
        Quaternion lookRotation = Quaternion.LookRotation(new Vector3(direction.x, 0, direction.z));
        transform.rotation = lookRotation;
    }

    public void OnEnemyHealthChange(float newHealth)
    {
        health = newHealth;
        transform.localScale = new Vector3(health/healthMax, scaleInit.y, scaleInit.z);
    }
}
