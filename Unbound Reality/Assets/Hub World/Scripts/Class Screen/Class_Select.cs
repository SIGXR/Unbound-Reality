using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class Class_Select : MonoBehaviour
{
    public BaseClass Warrior = new BaseClass("Warrior", 6, 15, 3, 6);
    public BaseClass Mage = new BaseClass("Mage", 6, 3, 15, 6);
    public BaseClass Rouge = new BaseClass("Rouge", 15, 6, 3, 6);
    public BaseClass Archer = new BaseClass("Archer", 8, 6, 4, 15);
    public Text Displayinfo;
    public GameObject hud;
    public GameObject gameSetup;
    public Text ClassName;
    public static bool pause;

    public void Start()
    {
        Displayinfo.enabled = false;
    }

    public void PointerEnter(Button btn)
    {
        Displayinfo.enabled = true;
        if (btn.name == "WarriorButton")
        {
            Displayinfo.text = "Name: " + Warrior.ClassName() + "\nSpeed: " + Warrior.Speed() + "\nStrength: " + Warrior.Strength() + "\nMagic: " + Warrior.MagicStrength() + "\nCritical: " + Warrior.Critical(); 
        }
        else if (btn.name == "MageButton")
        {
            Displayinfo.text = "Name: " + Mage.ClassName() + "\nSpeed: " + Mage.Speed() + "\nStrength: " + Mage.Strength() + "\nMagic: " + Mage.MagicStrength() + "\nCritical: " + Mage.Critical();
        }
        else if (btn.name == "RougeButton")
        {
            Displayinfo.text = "Name: " + Rouge.ClassName() + "\nSpeed: " + Rouge.Speed() + "\nStrength: " + Rouge.Strength() + "\nMagic: " + Rouge.MagicStrength() + "\nCritical: " + Rouge.Critical();
        }
        else if (btn.name == "ArcherButton")
        {
            Displayinfo.text = "Name: " + Archer.ClassName() + "\nSpeed: " + Archer.Speed() + "\nStrength: " + Archer.Strength() + "\nMagic: " + Archer.MagicStrength() + "\nCritical: " + Archer.Critical();
        }
    }
    public void PointerExit()
    {
        Displayinfo.enabled = false;
    }
    public void PlayGame(Button btn)
    {
        float getMove = NonVRCharacterController.moveSpeed;
        

        if (btn.name == "WarriorButton")
        {
            NonVRCharacterController.class_name = Warrior.ClassName();
            NonVRCharacterController.moveSpeed = Warrior.Speed();
            NonVRCharacterController.strength = Warrior.Strength();
            NonVRCharacterController.magic = Warrior.MagicStrength();
            NonVRCharacterController.critical = Warrior.Critical();
            pause = false;
            Debug.Log(pause);

        }
        else if (btn.name == "MageButton")
        {
            NonVRCharacterController.class_name = Mage.ClassName();
            NonVRCharacterController.moveSpeed = Mage.Speed();
            NonVRCharacterController.strength = Mage.Strength();
            NonVRCharacterController.magic = Mage.MagicStrength();
            NonVRCharacterController.critical = Mage.Critical();
            pause = false;
            Debug.Log(pause);
        }
        else if (btn.name == "ArcherButton")
        {
            NonVRCharacterController.class_name = Archer.ClassName();
            NonVRCharacterController.moveSpeed = Archer.Speed();
            NonVRCharacterController.strength = Archer.Strength();
            NonVRCharacterController.magic = Archer.MagicStrength();
            NonVRCharacterController.critical = Archer.Critical();
            pause = false;
            Debug.Log(pause);
        }
        else if (btn.name == "RougeButton")
        {
            NonVRCharacterController.class_name = Rouge.ClassName();
            NonVRCharacterController.moveSpeed = Rouge.Speed();
            NonVRCharacterController.strength = Rouge.Strength();
            NonVRCharacterController.magic = Rouge.MagicStrength();
            NonVRCharacterController.critical = Rouge.Critical();
            pause = false;
            Debug.Log(pause);
        }
        Debug.Log(btn.name);
        ClassNameDisplay();
        DisableMovement();
        this.gameObject.SetActive(false);
    }
    public void DisableMovement()
    {
        if (pause == false)
        {
            hud.SetActive(true);
            gameSetup.SetActive(true);
        }

    }
    public void ClassNameDisplay()
    {
        ClassName.text = NonVRCharacterController.class_name;
    }
}
