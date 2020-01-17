using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class Class_Select : MonoBehaviour
{
    public BaseClass Warrior = new BaseClass("Warrior", 8, 15, 2, 5);
    public BaseClass Mage = new BaseClass("Mage", 8, 2, 15, 5);
    public BaseClass Rouge = new BaseClass("Rouge", 15, 6, 3, 6);
    public BaseClass Archer = new BaseClass("Archer", 10, 5, 0, 15);
    public Text Stats;

    public void Start()
    {
        Stats.enabled = false;
    }
    public void PlayGame(Button btn)
    {
        float getMove = NonVRCharacterController.moveSpeed;
        SceneManager.LoadScene("Hub World");

        if (btn.name == "WarriorButton")
        {
            float move = Warrior.Speed();
            NonVRCharacterController.moveSpeed = move;
        }
        else if (btn.name == "MageButton")
        {
            float move = Mage.Speed();
            NonVRCharacterController.moveSpeed = move;
        }
        Debug.Log(btn.name);
    }
    public void PointerEnter(Button btn)
    {
        Stats.enabled = true;
        if (btn.name == "WarriorButton")
        {
            Stats.text = "Name: " + Warrior.Name() + "\n Speed: " + Warrior.Speed() + "\n Strength: " + Warrior.Strength() + "\n Magic: " + Warrior.Magic() + "\n Critical: " + Warrior.Critical();
        }
        else if (btn.name == "MageButton")
        {
            Stats.text = "Name: " + Mage.Name() + "\n Speed: " + Mage.Speed() + "\n Strength: " + Mage.Strength() + "\n Magic: " + Mage.Magic() + "\n Critical: " + Mage.Critical();
        }
        else if (btn.name == "RougeButton")
        {
            Stats.text = "Name: " + Rouge.Name() + "\n Speed: " + Rouge.Speed() + "\n Strength: " + Rouge.Strength() + "\n Magic: " + Rouge.Magic() + "\n Critical: " + Rouge.Critical();
        }
        else if (btn.name == "ArcherButton")
        {
            Stats.text = "Name: " + Archer.Name() + "\n Speed: " + Archer.Speed() + "\n Strength: " + Archer.Strength() + "\n Magic: " + Archer.Magic() + "\n Critical: " + Archer.Critical();
        }
    }
    public void PointerExit()
    {
        Stats.enabled = false;
    }
}
