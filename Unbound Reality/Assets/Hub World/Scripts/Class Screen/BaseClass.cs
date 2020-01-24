using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BaseClass {
    private string className;
    private float speed;
    private float strength;
    private float magicStrength;
    private float critical;

    public BaseClass(string Name, float sped, float physStrength, float magStrength, float Crit)
    {
        className = Name;
        speed = sped;
        strength = physStrength;
        magicStrength = magStrength;
        critical = Crit;
    }

    public string ClassName()
    {
        return className;
    }
    public float Speed()
    {
        return speed;
    }

    public float Strength()
    {
        return strength;
    }
    public float MagicStrength()
    {
        return magicStrength;
    }
    public float Critical()
    {
        return critical;
    }
}
