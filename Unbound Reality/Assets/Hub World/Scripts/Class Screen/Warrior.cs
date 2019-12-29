using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Warrior
{
    public float speed;
    public float strength;

    public Warrior()
    {
        speed = 10;
        strength = 15;
    }

    public float Speed()
    {
        return speed;
    }

    public float Strength()
    {
        return strength;
    }
}
