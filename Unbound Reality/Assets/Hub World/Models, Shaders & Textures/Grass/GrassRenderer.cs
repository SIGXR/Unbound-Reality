using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[ExecuteInEditMode]
public class GrassRenderer : MonoBehaviour
{
    public Mesh grassMesh;
    public Material material;
    
    
    public Vector2 size;

    [Range(1,1000)]
    public int grassNumber;

    public float startHeight = 1000;
    
    // Update is called once per frame
    void Update()
    {
        int seed = (int)(transform.position.x + transform.position.y);
        Random.InitState(seed);
        List<Matrix4x4> materices = new List<Matrix4x4>(grassNumber);
        for(int i = 0; i < grassNumber; ++i)
        {
            Vector3 origin = transform.position;
            origin.y = startHeight;
            origin.x += size.x * Random.Range(-0.5f, 0.5f);
            origin.z += size.y * Random.Range(-0.5f, 0.5f);   
            Ray ray = new Ray(origin, Vector3.down);
            RaycastHit hit;
            if(Physics.Raycast(ray, out hit))
            {
                origin = hit.point;
                materices.Add(Matrix4x4.TRS(origin, Quaternion.identity, Vector3.one));
            }
        }
        Graphics.DrawMeshInstanced(grassMesh, 0, material, materices);
    }
}
