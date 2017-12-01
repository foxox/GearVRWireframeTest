using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ProceduralTextureTester : MonoBehaviour
{
    public Texture2D TextureOverride;

	// Use this for initialization
	void Start ()
    {
        this.TextureOverride = new Texture2D(64, 64);
        this.GetComponent<Renderer>().material.mainTexture = this.TextureOverride;

        for (int i = 0; i < this.TextureOverride.width; i++)
        {
            for (int j = 0; j < this.TextureOverride.height; j++)
            {
                this.TextureOverride.SetPixel(i, j, Color.blue);
            }
        }

        this.TextureOverride.Apply();

	}
	
	// Update is called once per frame
	//void Update ()
 //   {
		
	//}
}
