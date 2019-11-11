using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GunUI : MonoBehaviour
{
    public int curAmm = 0; //will hold the current ammo for us
    public int MaxAmm = 0; //Will hold max ammo
    public Text AmmoDisp; //we will use this to check whether our ammo in the format <CUrrent>/<Max>
    Gun gunElements;
    private int currAmm;

    public GunUI(Gun curGun)
    {
        gunElements = curGun;

        void start(Gun gunElements)
        {

            MaxAmm = gunElements.maxAmmo;
            curAmm = gunElements.getCurAmmo();


        }
        void DecCurrAmmo() //intent is to decrement the ammo count whenever we use it
        {
            curAmm -= 1;
        }
        void Update(Gun gunElements)
        {
            if (Input.GetButtonDown("Fire1")) {
                gunElements.Shoot();
                DecCurrAmmo();
            }
            AmmoDisp.text = curAmm.ToString() + "/\n" + MaxAmm.ToString();

            if (currAmm == 0)
            {
                StartCoroutine(gunElements.Reload());
            }

        }
    }
}
