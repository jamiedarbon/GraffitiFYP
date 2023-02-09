using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;

public class TextureSave : MonoBehaviour
{
    private Texture2D tex;
    // Start is called before the first frame update
    void Start()
    {
        Debug.Log("Started");
        if (File.Exists(Application.dataPath + "/../SavedScreen.png"))
        {
            Debug.Log("Texture found");
            var imageBytes = File.ReadAllBytes(Application.dataPath + "/../SavedScreen.png");
            Texture2D returningTex = new Texture2D(2,2); //must start with a placeholder Texture object
            returningTex.LoadImage(imageBytes);
            GetComponent<Renderer>().material.mainTexture = returningTex;
        }
    }

    // Update is called once per frame
    void Update()
    {
        tex = (Texture2D) GetComponent<Renderer>().sharedMaterial.mainTexture;
        if (Input.GetKeyDown(KeyCode.U))
        {
            SaveTexture();
        }
    }
    
    public void SaveTexture()
    {
        byte[] bytes = tex.EncodeToPNG();
        File.WriteAllBytes(Application.dataPath + "/../SavedScreen.png", bytes);
        Debug.Log("Saved");
    }
}
