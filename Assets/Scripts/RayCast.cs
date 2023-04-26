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
    public Color[] testArray;
    private Color32[] colours;
    private GameObject ColourIndicator;
    public bool canPaint;
    public bool bannerCheck = false;
    public bool bannerCheck2 = false;
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
        testArray = new Color[1024];
    }

    void LateUpdate()
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
        //testArray = tex.GetPixels();
        Vector2 pixelUV = hit.textureCoord;
        Debug.Log("tex = " + tex);
        Debug.Log("UV = " + pixelUV);
        pixelUV.x *= tex.width;
        pixelUV.y *= tex.height;

        Debug.Log("PAINTING AAAAAAHA");
        if (paintCheck)
        {
            Debug.Log("PaintCheck");
            isPainting = true;
            GameObject.Find("QuestManager").GetComponent<QuestSystem>().tutorial = true;
        }

        if (bannerCheck)
        {
            Debug.Log("BannerCheck = " + hit.transform.gameObject);
            Debug.Log("Comp = " + GameObject.Find("Banner"));
            if(hit.transform.gameObject == GameObject.Find("Cube.003"))
            {
                GameObject.Find("QuestManager").GetComponent<QuestSystem>().banner = true;
            }
        }

        if (bannerCheck2)
        {
            GameObject.Find("QuestManager").GetComponent<QuestSystem>().banner = false;
            if (hit.transform.gameObject == GameObject.Find("Cube.003") && ((GetComponent<ColourPicker>().mHue.value > 0.9) || (GetComponent<ColourPicker>().mHue.value < 0.1)))
            {
                GameObject.Find("QuestManager").GetComponent<QuestSystem>().banner2 = true;
            }
        }

        //SetPixels method
        tex.SetPixels((int)pixelUV.x - (brushSize / 2), (int)pixelUV.y - (brushSize / 2), brushSize, brushSize, testArray, 0);
        tex.Apply();

        /*
        //Circle Brush (setpixel)
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
        }
        //StartCoroutine(rainbow());
        tex.Apply();
        */
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
            if (brushSize < 32)
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
