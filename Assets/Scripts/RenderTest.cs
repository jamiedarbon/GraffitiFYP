using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RenderTest : MonoBehaviour
{
    public RenderTexture renderTexture;
    public Renderer renderer;
    private int at = 0;

    private Texture2D texture;
    // Start is called before the first frame update
    void Start()
    {
        texture = new Texture2D (renderTexture.width, renderTexture.height);
        renderer.material.mainTexture =  texture;
    }

    // Update is called once per frame
    void Update()
    {
        at++;
        RenderTexture.active = renderTexture; 
        //don't forget that you need to specify rendertexture before you call readpixels
        //otherwise it will read screen pixels.
        texture.ReadPixels (new Rect (0, 0, renderTexture.width, renderTexture.height), 0, 0);
        
        /*float k, angle;
        int x1, y1, l;
        for(k = 0; k < 360; k += 0.1f)
        {
            angle = k;
            x1 = Mathf.RoundToInt(150 * Mathf.Cos(angle * 3.14f / 180));
            y1 = Mathf.RoundToInt(150 * Mathf.Sin(angle * 3.14f / 180));
            for (l = x1; l <= y1; l++)
            {
                //Debug.Log("x1 = " + x1);
                texture.SetPixel((int)at + x1, (int)k + l, new Color(255, 0, 0));
            }
            for (l = y1; l <= x1; l++)
            {
                texture.SetPixel((int)at + l, (int) k + y1, new Color(255, 0, 0));
            }
            texture.SetPixel((int) at + x1, (int) k + y1, new Color(255, 0, 0));
        }
        
        for (int i = 0; i < renderTexture.width*.2f; i++) {
            for (int j = 0; j < renderTexture.height; j++)
            {
                texture.SetPixel((at + i), j, new Color(255, 0, 0));
            }
        }
        texture.Apply (); */
        //RenderTexture.active = null;
    }
}
