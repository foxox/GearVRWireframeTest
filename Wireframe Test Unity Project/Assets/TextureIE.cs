using UnityEngine;
using System.Collections;

[ExecuteInEditMode]
public class TextureIE : MonoBehaviour
{
    //public float intensity;
    public Texture2D TheTexture;
    
    private Material material;

    // Creates a private material used for the effect
    void Awake()
    {
        this.material = new Material(Shader.Find("John/TextureIE"));

        TheTexture = new Texture2D(64, 64);//, TextureFormat.RGB24, false);

        for (int i = 0; i < 10; i++)
        {
            for (int j = 0; j < TheTexture.height; j++)
            {
                //float normalizer = TheTexture.GetPixel(i, j).r / 10000000;
                //TheTexture.SetPixel(i, j, new Color(normalizer, normalizer, normalizer));
                TheTexture.SetPixel(i, j, new Color(0, 1, 0));

            }
        }

        TheTexture.Apply();
        Debug.Log("test"); ;

    }

    // Postprocess the image
    void OnRenderImage(RenderTexture source, RenderTexture destination)
    {
        //material.SetTexture("_PatternImage", this.TheTexture);
        Graphics.Blit(source, destination, material);
    }
}

