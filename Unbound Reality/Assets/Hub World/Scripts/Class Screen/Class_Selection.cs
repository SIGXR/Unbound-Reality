using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Class_Selection : MonoBehaviour
{
    public GameObject Hud;
    public GameObject classSelect;
    

    void Start()
    {
        Class_Select.pause = true;
    }

    // Update is called once per frame
    void Update()
    {

    }
    private void OnTriggerStay(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            Class_Select.pause = true;
            Hud.SetActive(false);
            classSelect.SetActive(true);
            Debug.Log("Please select a class");
        }
    }
    
}
    
