using UnityEngine;
using System.Collections;

[ExecuteInEditMode]
public class ASIE : MonoBehaviour
{
    //public float intensity;
    public Texture2D DepthImage;
    public Texture2D PatternImage;
    public float PatternScale;

    public Texture2D OffsetImage;



    private Material material;

    // Creates a private material used to the effect
    void Awake()
    {
        material = new Material(Shader.Find("John/ASIE"));

        OffsetImage = new Texture2D(DepthImage.width * 2, DepthImage.height * 2, TextureFormat.RFloat, false);
        //OffsetImage = new Texture2D(DepthImage.width * 2, DepthImage.height * 2, TextureFormat.RGB24, true);

        //OffsetImage.wrapMode = TextureWrapMode.Repeat;
        //OffsetImage.dimension = UnityEngine.Rendering.TextureDimension.Tex2D;
        //OffsetImage.filterMode = FilterMode.Point;
        //OffsetImage.format = TextureFormat.R16;

        bool supported = SystemInfo.SupportsTextureFormat(TextureFormat.RFloat);
        Debug.Log("format supported? " + supported.ToString());

        float[,] offsetData = new float[OffsetImage.width,OffsetImage.height];

        //initialize offsetimage at the center
        int centeri = OffsetImage.width / 2;
        //for (int j = 0; j < OffsetImage.height; j++)
        //{
        //    //OffsetImage.SetPixel(centeri, j, Color.black);
        //    offsetData[centeri, j] = 0;
        //}

        float min = float.PositiveInfinity;
        float max = float.NegativeInfinity;

        int shift = OffsetImage.width / 4;

        //for (int i = 0; i < OffsetImage.width / 2; i++)
        //{
        //    int lefti = centeri - i;

        //    if (lefti >= 0)
        //    {
        //        for (int j = 0; j < OffsetImage.height; j++)
        //        {
        //            float depthOffset = DepthImage.GetPixel(lefti, j).r;
        //            float adjacent = offsetData[lefti + 1, j];
        //            float c = adjacent + depthOffset;// + 1;
        //            //OffsetImage.SetPixel(lefti, j, new Color(c, c, c));
        //            offsetData[lefti, j] = c;
        //            if (c < min) min = c;
        //            if (c > max) max = c;
        //        }
        //    }

        //    int righti = centeri + i;
        //    if (righti < OffsetImage.width)
        //    {
        //        for (int j = 0; j < OffsetImage.height; j++)
        //        {
        //            float depthOffset = DepthImage.GetPixel(righti, j).r;
        //            float adjacent = offsetData[righti - 1, j];
        //            float c = adjacent - depthOffset;// - 1;
        //            //OffsetImage.SetPixel(lefti, j, new Color(c, c, c));
        //            offsetData[righti, j] = c;
        //            if (c < min) min = c;
        //            if (c > max) max = c;
        //        }
        //    }
        //}

        //compute texture coords for left side of image
        //for (int i = 0; i < shift; i++)
        //{
        //    float horizontalCoord = (float)i / (shift-1.0f);
        //    if ((i == shift - 1) && horizontalCoord != 1.0f)
        //        Debug.Log("not reaching 1.0 coord");
        //    for (int j = 0; j < OffsetImage.height; j++)
        //    {
        //        offsetData[i, j] = horizontalCoord;
        //    }
        //}

        float maxOffset = (float)shift / 2.0f;
        for (int i = 0; i < OffsetImage.width; i++)
        {
            for (int j = 0; j < OffsetImage.height; j++)
            {
                float depthOffset = DepthImage.GetPixelBilinear((float)i / OffsetImage.width, (float)j / OffsetImage.height).r / 2.0f + 0.5f;
                float adjacentIndex = i - shift + (depthOffset * maxOffset);

                
                
                //int adjacentIndexLow = (int)adjacentIndex;
                //int adjacentIndexHigh = adjacentIndexLow + 1;

                //float adjacentSampleLow;
                //float adjacentSampleHigh;

                //if (adjacentIndexLow > 0)
                //{
                //    adjacentSampleLow = offsetData[adjacentIndexLow, j];
                //}
                //else
                //{
                //    //adjacentSampleLow = ((float)(shift - adjacentIndexLow % shift)) / ((float)(shift - 1)) - 1.0f;
                //    adjacentSampleLow = -((float)(adjacentIndexLow % shift)) / ((float)(shift - 1));
                //}

                //if (adjacentIndexHigh > 0)
                //{
                //    adjacentSampleHigh = offsetData[adjacentIndexHigh, j];
                //}
                //else
                //{
                //    //adjacentSampleHigh = ((float)(shift - adjacentIndexHigh % shift)) / ((float)(shift - 1)) - 1.0f;
                //    adjacentSampleHigh = -((float)(adjacentIndexHigh % shift)) / ((float)(shift - 1));
                //}

                //float adjacentSampleLerped = adjacentSampleLow + (adjacentSampleHigh - adjacentSampleLow) * (adjacentIndex - (float)adjacentIndexLow);

                //if (adjacentIndexLow % shift == 0)
                //{
                //    adjacentSampleLerped = adjacentSampleLow;
                //}
                //if (adjacentIndexHigh % shift == 0)
                //{
                //    adjacentSampleLerped = adjacentSampleHigh;
                //}




                float adjacentSampleLerped;
                if (adjacentIndex > 0)
                    adjacentSampleLerped = offsetData[(int)adjacentIndex, j];
                else
                    adjacentSampleLerped = (adjacentIndex % shift) / (shift -1.0f);

                offsetData[i, j] = 1.0f + adjacentSampleLerped;
                if (adjacentSampleLerped < min) min = adjacentSampleLerped;
                if (adjacentSampleLerped > max) max = adjacentSampleLerped;
            }
        }

        ////center the offsets
        //for (int j = 0; j < OffsetImage.height; j++)
        //{
        //    float RowOffsetAtCenter = offsetData[centeri, j];
        //    float adjustment = RowOffsetAtCenter;
        //    for (int i = 0; i < OffsetImage.width; i++)
        //    {
        //        //Debug.Log("i and j " + i.ToString() + "," + j.ToString());
        //        float newoffset = offsetData[i, j] - adjustment;
        //        if (newoffset < 0) newoffset += 1.0f;
        //        if (newoffset > 1.0f) newoffset -= 1.0f;
        //        offsetData[i, j] = newoffset;
        //    }
        //}
        //this makes weird asymptote things where it goes over (would be so much better if I could do the 1.0f + thing above)... instead consider a method of building up around the center

        for (int i = 0; i < OffsetImage.width; i++)
        {
            for (int j = 0; j < OffsetImage.height; j++)
            {
                //float normalized = (offsetData[i,j] - min) / (max-min);

                float normalized = offsetData[i, j] / 10.0f;

                offsetData[i,j] = normalized;

                //OffsetImage.SetPixel(i, j, new Color(normalized, normalized, normalized));

                //if ((i % 20 == 0) && (j % 30 == 0))
                //{
                //    Debug.Log(normalized);
                //}
            }
        }

        float[] offsetData_test = new float[OffsetImage.width * OffsetImage.height];// * 4];
        for (int i = 0; i < OffsetImage.width; i++)
        {
            for (int j = 0; j < OffsetImage.height; j++)
            {
                float offset = offsetData[i, j];
                //offsetData_test[j * (OffsetImage.width*4) + i*4 + 0] = offset;
                offsetData_test[j * OffsetImage.width + i] = offset;
            }
        }

        byte[] offsetData_testbyte = new byte[OffsetImage.width * OffsetImage.height * 4];
        System.Buffer.BlockCopy(offsetData_test, 0, offsetData_testbyte, 0, offsetData_testbyte.Length);

        OffsetImage.LoadRawTextureData(offsetData_testbyte);

        OffsetImage.Apply();
        Debug.Log("test"); ;

    }

    // Postprocess the image
    void OnRenderImage(RenderTexture source, RenderTexture destination)
    {
        //if (intensity <= 0)
        //{
        //    Graphics.Blit(source, destination);
        //    intensity = 0;
        //    return;
        //}

        //if (intensity > 1)
        //{
        //    intensity = 1;
        //}




        //material.SetFloat("_BWBlend", intensity);
        material.SetTexture("_OffsetImage", this.OffsetImage);
        material.SetTexture("_PatternImage", this.PatternImage);
        material.SetFloat("_PatternScale", this.PatternScale);
        Graphics.Blit(source, destination, material);
    }
}

