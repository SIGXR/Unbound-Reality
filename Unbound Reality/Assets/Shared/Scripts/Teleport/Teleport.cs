using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Teleport : MonoBehaviour {

    public string areaName;
    public Animator fade;

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            fade.SetTrigger("FadeOut");
            StartCoroutine(SceneChange());
        }
    }

    IEnumerator SceneChange()
    {
        yield return new WaitForSeconds(1);
        SceneManager.LoadScene(areaName,LoadSceneMode.Single);
    }
}
