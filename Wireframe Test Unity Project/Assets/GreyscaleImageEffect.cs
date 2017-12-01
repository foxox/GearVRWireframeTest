using UnityEngine;
using System.Collections;

[ExecuteInEditMode]
public class GreyscaleImageEffect : MonoBehaviour
{
    public float intensity;
    private Material material;

    // Creates a private material used to the effect
    void Awake()
    {
        material = new Material(Shader.Find("John/BWDiffuse"));
    }

    // Postprocess the image
    void OnRenderImage(RenderTexture source, RenderTexture destination)
    {
        if (intensity <= 0)
        {
            Graphics.Blit(source, destination);
            intensity = 0;
            return;
        }

        if (intensity > 1)
        {
            intensity = 1;
        }

        material.SetFloat("_BWBlend", intensity);
        Graphics.Blit(source, destination, material);
    }
}

