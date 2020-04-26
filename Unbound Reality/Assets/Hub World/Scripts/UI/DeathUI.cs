using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

[RequireComponent(typeof(TMP_Text))]
public class DeathUI : MonoBehaviour
{
    [Tooltip("The text script connected to the death counter")]
    [SerializeField]
    private TMP_Text text;
    private int deathCount = 0;

    void Awake() {
        text = GetComponent<TMP_Text>();

    }

    public void OnPlayerDeath(Player player)
    {
        deathCount++;
        text.text = "Deaths: " + deathCount;
        Debug.Log("Player Health: " + player.health);
    }

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
