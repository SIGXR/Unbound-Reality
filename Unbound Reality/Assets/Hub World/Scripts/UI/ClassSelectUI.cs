using System;
using System.Collections;
using System.Collections.Generic;
using System.Threading;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using System.Threading.Tasks;

public class ClassSelectUI : MonoBehaviour
{
    public enum ClassTypes
    {
        WARRIOR,
        MAGE,
        ROUGE,
        ARCHER
    };

    public int classNum = Enum.GetNames(typeof(ClassTypes)).Length;

    //The default attribute values for the classes in order by ClassType enum
    private int[] baseSpeeds = new int[]{ 6, 6, 15, 8 };
    private int[] baseStrengths = new int[]{ 15, 3, 6, 6 };
    private int[] baseMagics = new int[]{ 3, 15, 3, 4 };
    private int[] baseCrits = new int[]{ 6, 6, 6, 15 };

    public Text Displayinfo;
    public Canvas hudCanvas;
    public GameObject gameSetup;
    public Player player;

    public void Start()
    {
        Displayinfo.enabled = false;
    }

    public void PointerEnter(Button btn)
    {
        int type;

        Displayinfo.enabled = true;
        type = GetClassType(btn.name);

        Displayinfo.text = "Name: " + btn.name 
            + "\nSpeed: " + baseSpeeds[type]
            + "\nStrength: " + baseStrengths[type]
            + "\nMagic: " + baseMagics[type]
            + "\nCritical: " + baseCrits[type];
        
    }

    public void PointerExit()
    {
        Displayinfo.enabled = false;
    }

    public void PlayGame(Button btn)
    {
        int type;

        Debug.Log(btn.name);
        type = GetClassType(btn.name);

        EnableGame();
        //wait for game to start to get the new player.
        player = (Player) FindObjectOfType(typeof(Player));
        
        player.SetGodMode(false);
        player.attributeList[(int) Player.Attributes.SPEED] = baseSpeeds[type];
        player.attributeList[(int) Player.Attributes.STRENGTH] = baseStrengths[type];
        player.attributeList[(int) Player.Attributes.MAGIC] = baseMagics[type];
        player.attributeList[(int) Player.Attributes.CRITICAL] = baseCrits[type];

        player.classType = GetBaseClass(type);

        this.gameObject.SetActive(false);
    }

    public void EnableGame()
    {
        hudCanvas.enabled = true;
    }

    int GetClassType(string name)
    {
        int type;
        Debug.Log("GetClassType: " + name);

        switch(name)
        {
            case "Warrior":
            {
                type = (int) ClassTypes.WARRIOR;
                break;
            }
            case "Mage":
            {
                type = (int) ClassTypes.MAGE;
                break;
            }
            case "Rouge":
            {
                type = (int) ClassTypes.ROUGE;
                break;
            }
            case "Archer":
            {
                type = (int) ClassTypes.ARCHER;
                break;
            }
            default:
            {
                type = -1;
                break;
            }
        }

        Debug.Log("Type: " + type);

        return type;
    }

    BaseClass GetBaseClass(int type)
    {
        BaseClass baseClass;

        switch(type)
        {
            case (int) ClassTypes.WARRIOR:
            {
                baseClass = (BaseClass) FindObjectOfType(typeof(Warrior));
                break;
            }
            case (int) ClassTypes.MAGE:
            {
                baseClass = (BaseClass) FindObjectOfType(typeof(Mage));
                break;
            }
            case (int) ClassTypes.ROUGE:
            {
                baseClass = (BaseClass) FindObjectOfType(typeof(Rouge));
                break;
            }
            case (int) ClassTypes.ARCHER:
            {
                baseClass = (BaseClass) FindObjectOfType(typeof(Archer));
                break;
            }
            default:
            {
                baseClass = null;
                break;
            }
        }

        return baseClass;
    }
}
