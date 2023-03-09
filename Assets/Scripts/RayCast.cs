using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;
using System;
using System.Diagnostics;
using UnityEngine.UI;
using Debug = UnityEngine.Debug;

public class RayCast : MonoBehaviour
{
    public Slider slider;
    public Slider healthbar;
    public Camera cam;
    public Texture2D myTexture;
    public RenderTexture rt;
    [Range(0, 50)]
    public int brushSize = 10;
    [SerializeField]
    public Color Colour;
    private float rvalue, gvalue, bvalue;
    private int colourIndex = 0;
    private IEnumerator Coroutine;
    private Color rainbowColour;
    private Color[] testArray;
    private Color32[] colours;
    private GameObject ColourIndicator;
    public bool canPaint;
    public bool isPainting = false;
    public bool paintCheck = false;


    void Start()
    {
        cam = GetComponent<Camera>();
        Coroutine = rainbow();
        StartCoroutine(Coroutine);
        rainbowColour = new Color(255, 0, 0, 255);
        colours = new Color32[] { new Color(0, 0, 0,255), new Color(255, 255, 255,255),
            new Color(255, 0, 0,255), new Color(0, 255, 0,255), new Color(0, 0, 255,255)};
        ColourIndicator = GameObject.Find("ColourIndicator");
        ColourIndicator.GetComponent<Image>().color = Colour;
        canPaint = true;
    }

    void Update()
    {
        SizeChanger();
        sliderManager();
        brushChange();
        colourChange();
        isPainting = false;
        Debug.Log("Colour = " + Colour);
        
        if (!Input.GetMouseButton(0))
            return;
        
        RaycastHit hit;
        if (!Physics.Raycast(cam.ScreenPointToRay(Input.mousePosition), out hit))
            return;
        Debug.Log(hit);
        Debug.Log(hit.transform);
        Renderer rend = hit.transform.GetComponent<Renderer>();
        MeshCollider meshCollider = hit.collider as MeshCollider;

        if (rend == null || rend.sharedMaterial == null || rend.sharedMaterial.mainTexture == null ||
            meshCollider == null)
        {
            Debug.Log("main =" + rend.sharedMaterial.mainTexture);
            Debug.Log(rend.sharedMaterial.mainTexture);
            Debug.Log("mesh coll =" + meshCollider);
            Debug.Log("rend =" + rend);
            Debug.Log("Shared Material = " + rend.sharedMaterial);
            return;
        }

        if (!canPaint)
        {
            return;
        }

        Debug.Log(rend.sharedMaterial.mainTexture);
        Debug.Log(rend.material.mainTexture);
        Texture2D tex = rend.material.mainTexture as Texture2D;
        testArray = tex.GetPixels();
        Vector2 pixelUV = hit.textureCoord;
        Debug.Log("tex = " + tex);
        Debug.Log("UV = " + pixelUV);
        pixelUV.x *= tex.width;
        pixelUV.y *= tex.height;

        Debug.Log("PAINTING AAAAAAHA");
        if(paintCheck)
        {
            Debug.Log("PaintCheck");
            isPainting = true;
            GameObject.Find("QuestManager").GetComponent<QuestSystem>().tutorial = true;
        }

        //Circle Brush (setpixel)
        
        /*
        float k, angle;
        int x1, y1, l;
        for(k = 0; k < 360; k += 0.1f)
        {
            angle = k;
            x1 = Mathf.RoundToInt(brushSize * Mathf.Cos(angle * 3.14f / 180));
            y1 = Mathf.RoundToInt(brushSize * Mathf.Sin(angle * 3.14f / 180));
            for (l = x1; l <= y1; l++)
            {
                //Debug.Log("l = " + l);
                if (l > -y1)
                {
                    tex.SetPixel((int)pixelUV.x + x1, (int)pixelUV.y + l, Colour);
                }
            }
            for (l = y1; l <= x1 ; l++)
            {
                if (l > -x1)
                {
                    tex.SetPixel((int)pixelUV.x + l, (int)pixelUV.y + y1, Colour);   
                }
            }
            tex.SetPixel((int) pixelUV.x + x1, (int) pixelUV.y + y1, Colour);
        }
        //StartCoroutine(rainbow());
        tex.Apply();
        */
        //Circle Brush (setpixels)
        
        if (brushSize > 1)
        {
            // bottom - left aligned, so find new bottom left coordinate then use that as our starting point
            pixelUV.x = Mathf.Clamp(pixelUV.x - (brushSize / 2), 0, tex.width);
            pixelUV.y = Mathf.Clamp(pixelUV.y - (brushSize / 2), 0, tex.height);

            // add 1 to our brush size so the pixels found are a neighbour search outward from our center point
            int maxWidth = (int)Mathf.Clamp(brushSize + 1, 0, tex.width - pixelUV.x);
            int maxHeight = (int)Mathf.Clamp(brushSize + 1, 0, tex.height - pixelUV.y);

            // cache our maximum dimension size
            int blockDimension = maxWidth * maxHeight;

            // create an array for our colors
            Color[] colorArray = new Color[blockDimension];

            // fill this with our color
            for (int x = 0; x < blockDimension; ++x)
                colorArray[x] = Colour;

            // set our pixel colors
            tex.SetPixels((int)pixelUV.x, (int)pixelUV.y, maxWidth, maxHeight, colorArray);
        }
        else
        {
            // set our color at our position - note this will almost never be seen as most textures are rather large, so a single pixel is not going to
            // appear most of the time
            tex.SetPixel((int)pixelUV.x, (int)pixelUV.y, Colour);
        } 

        // apply the changes - this is what you were missing
        tex.Apply();
        /*GL.PushMatrix();
        GL.LoadOrtho();
        Graphics.DrawTexture(
            new Rect(0, 0, 1, 1),
            myTexture);
        GL.PopMatrix();*/
    }
    
    private void OnPostRender()
    {
        GL.PushMatrix();
        GL.LoadOrtho();
        Graphics.DrawTexture(
            new Rect(0, 0, 1, 1),
            myTexture);
        GL.PopMatrix();
    }

    public void brushChange()
    {
        if (Input.GetKeyDown(KeyCode.T))
        {
            Colour.a = 255;
        }

        if (Input.GetKeyDown(KeyCode.Y))
        {
            Colour.a = 0;
        }
    }

    public void colourChange()
    {
        if (Input.GetKeyDown(KeyCode.K))
        {
            colourIndex++;
            if (colourIndex > colours.Length)
            {
                colourIndex = 0;
            }
            Colour = colours[colourIndex];
        }

        if (Input.GetKeyDown(KeyCode.L))
        {
            colourIndex--;
            if (colourIndex < 0)
            {
                colourIndex = colours.Length;
            }
            Colour = colours[colourIndex];
        }
        ColourIndicator.GetComponent<Image>().color = Colour;
    }
    
    IEnumerator rainbow()
    {
        Debug.Log("Colour Called");
        if (rainbowColour.Equals(new Color(255, 0, 0, 255)))
        {
            rainbowColour = new Color(255, 128, 0, 255);
            Colour = rainbowColour;
        } else if (rainbowColour.Equals(new Color(255, 128, 0, 255)))
        {
            rainbowColour = new Color(255, 255, 0, 255);
            Colour = rainbowColour;
        } else if (rainbowColour.Equals(new Color(255, 255, 0, 255)))
        {
            rainbowColour = new Color(128, 255, 0, 255);
            Colour = rainbowColour;
        } else if (rainbowColour.Equals(new Color(128, 255, 0, 255)))
        {
            rainbowColour = new Color(0, 255, 0, 255);
            Colour = rainbowColour;
        } else if (rainbowColour.Equals(new Color(0, 255, 0, 255)))
        {
            rainbowColour = new Color(0, 255, 128, 255);
            Colour = rainbowColour;
        } else if (rainbowColour.Equals(new Color(0, 255, 128, 255)))
        {
            rainbowColour = new Color(0, 255, 255, 255);
            Colour = rainbowColour;
        } else if (rainbowColour.Equals(new Color(0, 255, 255, 255)))
        {
            rainbowColour = new Color(0, 128, 255, 255);
            Colour = rainbowColour;
        } else if (rainbowColour.Equals(new Color(0, 128, 255, 255)))
        {
            rainbowColour = new Color(0, 0, 255, 255);
            Colour = rainbowColour;
        } else if (rainbowColour.Equals(new Color(0, 0, 255, 255)))
        {
            rainbowColour = new Color(128, 0, 255, 255);
            Colour = rainbowColour;
        } else if (rainbowColour.Equals(new Color(128, 0, 255, 255)))
        {
            rainbowColour = new Color(255, 0, 255, 255);
            Colour = rainbowColour;
        } else if (rainbowColour.Equals(new Color(255, 0, 255, 255)))
        {
            rainbowColour = new Color(255, 0, 128, 255);
            Colour = rainbowColour;
        } else if (rainbowColour.Equals(new Color(255, 0, 128, 255)))
        {
            rainbowColour = new Color(255, 0, 0, 255);
            Colour = rainbowColour;
        }
        yield return null;
    }

    void SizeChanger()
    {
        if (Input.GetKey(KeyCode.O))
        {
            if (brushSize > 0)
            {
                brushSize--;
            }
        } else if (Input.GetKey(KeyCode.P))
        {
            if (brushSize < 50)
            {
                brushSize++;
            }
        }
    }

    public void sliderManager()
    {
        slider.value = brushSize;
    }
}
