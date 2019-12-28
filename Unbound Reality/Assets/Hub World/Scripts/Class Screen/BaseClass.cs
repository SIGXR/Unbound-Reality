using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BaseClass {
    private string characterClassName;
    private string characterClassDescription;

    private float speed;
    private float strength;
    private bool status;

    public string CharacterClassName
    {
        get { return characterClassName; }
        set { characterClassName = value; }
    }

    public string CharacterClassDescription
    {
        get { return characterClassDescription; }
        set { characterClassDescription = value; }
    }

    public float Speed
    {
        get { return speed; }
        set { speed = value; }
    }

    public float Strength
    {
        get { return strength; }
        set { strength = value; }
    }

    public bool Status
    {
        get { return status; }
        set { status = value; }
    }
}
