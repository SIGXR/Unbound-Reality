using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class SkillUI : MonoBehaviour
{
    [Tooltip("The text that appears on the button. Usually is skill name or cooldown time.")]
    [SerializeField]
    public Text buttonInfo;
    [Tooltip("The skill associated with the button.")]
    [SerializeField]
    public Skills skill;
    [Tooltip("The text box that shows the description of the skill.")]
    [SerializeField]
    public Text skillInfo;
    [Tooltip("The game object that contains skillInfo. Used to show/hide it.")]
    [SerializeField]
    public GameObject infoBox;

    //Time variables
    private float cooldownRate;
    private float nextActivationTime;
    private bool available = true;

    // Start is called before the first frame update
    void Start()
    {
        if(skill == null)
        {
            this.gameObject.SetActive(false);
            Debug.Log(this.name + ": No Skill assigned");
            return;
        }
        cooldownRate = skill.coolDownTime;
        infoBox.SetActive(false);
        
    }

    // Update is called once per frame
    void Update()
    {
        if (Time.time > nextActivationTime)
        {
            available = true;
            buttonInfo.text = skill.skillName;
        }
        else
        {
            buttonInfo.text = string.Format("{0:N2}", nextActivationTime - Time.time);
        }
    }

    public void Activate()
    {
        if (available == true)
        {
            nextActivationTime = Time.time + cooldownRate;
            skill.ActivateSkill();
            available = false;
        }
        else
        {
            Debug.Log("Cannot Activate");
        }
    }

    public void DisplayInfo()
    {
        infoBox.SetActive(true);
        skillInfo.text = "Name: " + skill.name + "\nCooldown Time: " + skill.coolDownTime + "\nDescription: " + skill.description;
    }

    public void DisableBox()
    {
        infoBox.SetActive(false);
    }
}
