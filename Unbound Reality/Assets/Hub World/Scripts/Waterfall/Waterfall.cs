using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Waterfall : MonoBehaviour
{
    public Transform _ParticleSystem;

    private MeshRenderer _MeshRenderer;

    private void Awake()
    {
        _MeshRenderer = GetComponent<MeshRenderer>();
    }
    //Allows updating position of the waterfall shader
    private void OnTriggerStay(Collider other)
    {
        UpdateStream(GetHeight(other));
    }

    //Resets the position of the waterfall shader
    private void OnTriggerExit(Collider other)
    {
        UpdateStream(0);
    }

    //Adds up to half the collider's y height to the waterfall. Bare in mind this changes based on the collided object's pivot location.
    private float GetHeight(Collider collider)
    {
        return collider.transform.position.y + collider.bounds.size.y/2;
    }

    private void UpdateStream(float newHeight)
    {
        //Particle
        Vector3 newPosition = new Vector3(transform.position.x, newHeight, transform.position.z);
        _ParticleSystem.position = newPosition;

        //Height cutoff, namely accounts for any y-scaling that's hapepning in the scene
        newHeight /= transform.localScale.y;

        Debug.Log(newHeight);
        _MeshRenderer.material.SetFloat("_Cutoff", newHeight);
    }
}
