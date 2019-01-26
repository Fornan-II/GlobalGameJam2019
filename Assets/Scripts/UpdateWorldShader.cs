using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UpdateWorldShader : MonoBehaviour
{
    public Material TrackedMaterial;

    // Update is called once per frame
    void Update()
    {
        //Shader.SetGlobalVector("_Position", transform.position);

        if(TrackedMaterial)
        {
            TrackedMaterial.SetVector("_Position", transform.position);
        }
    }
}
