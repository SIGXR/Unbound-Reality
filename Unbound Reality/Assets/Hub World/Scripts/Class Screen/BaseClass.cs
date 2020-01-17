using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BaseClass {
    public float speed;
    public float strength;
    public float critical;
    public float magic;
    public string name;

    public BaseClass(string ClassName, float sped, float phyStrength, float magStrength, float crit)
    {
        speed = sped;
        strength = phyStrength;
        name = ClassName;
        magic = magStrength;
        critical = crit;
    }

    public float Speed()
    {
        return speed;
    }

    public float Strength()
    {
        return strength;
    }
    public float Critical()
    {
        return critical;
    }
    public float Magic()
    {
        return magic;
    }
    public string Name()
    {
        return name;
    }
}
