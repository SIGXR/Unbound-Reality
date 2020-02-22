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
            Hud.SetActive(true);
            classSelect.SetActive(false);
            Debug.Log("Please select a class");
        }
    }
    
}
    
