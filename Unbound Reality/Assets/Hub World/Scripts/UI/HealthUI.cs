using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

[RequireComponent(typeof(Slider))]
public class HealthUI : MonoBehaviour
{
    public NonVRCharacterController nonvrplayer = null;
    public Player player = null;
    private Slider slider;

    //Called When Script Activated
    void Awake()
    {
        slider = GetComponent<Slider>();
    }

    // Update is called once per frame
    void Update()
    {
        if(player)
        {
            slider.value = player.health;
        }
    }
}
