using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class Class_Select : MonoBehaviour
{
    public Warrior w = new Warrior();
    public Mage m = new Mage();

    public void PlayGame(Button btn)
    {
        float getMove = NonVRCharacterController.moveSpeed;
        SceneManager.LoadScene("Hub World");

        if (btn.name == "WarriorButton")
        {
            float move = w.Speed();
            NonVRCharacterController.moveSpeed = move;
        }
        else if (btn.name == "MageButton")
        {
            float move = m.Speed();
            NonVRCharacterController.moveSpeed = move;
        }
        Debug.Log(btn.name);
    }
}
