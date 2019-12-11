using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class Teleport : MonoBehaviour
{

    public string areaName;
    public Animator fade;
    public Text bottomText;

    private void OnTriggerStay(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            Debug.Log("Wheeeeeee");
            bottomText.enabled = true;
            if (Input.GetKeyDown(KeyCode.E))
            {
                fade.SetTrigger("FadeOut");
                StartCoroutine(SceneChange());
            }
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            bottomText.enabled = false;
        }
    }

    IEnumerator SceneChange()
    {
        yield return new WaitForSeconds(1);
        SceneManager.LoadScene(areaName, LoadSceneMode.Single);
    }
}