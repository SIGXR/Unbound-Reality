using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class SkillUI : MonoBehaviour
{
    public Text cooldownTime;
    [SerializeField] public Skills skill;
    private float coolRate;
    private float NextSkill;
    private bool pressStat;

    // Start is called before the first frame update
    void Start()
    {
        coolRate = skill.CoolDownTime;
    }

    // Update is called once per frame
    void Update()
    {
        if (Time.time > NextSkill)
        {
            pressStat = true;
            cooldownTime.text = skill.SkillName;
        }
        else
        {
            cooldownTime.text = string.Format("{0:N2}", NextSkill - Time.time);
        }
    }

    public void coolDown()
    {
        if (pressStat == true)
        {
            NextSkill = Time.time + coolRate;
            skill.ActivateSkill();
            pressStat = false;
        }
        else
        {
            Debug.Log("Cannot Activate");
        }
    }
}
