using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class DebugStats : MonoBehaviour
{
    private float framerate;
    private GameObject fps;
    // Start is called before the first frame update
    void Start()
    {
        fps = GameObject.Find("FrameRate");
    }

    // Update is called once per frame
    void Update()
    {
        framerate = Mathf.Floor(1.0f / Time.deltaTime);
        fps.GetComponent<TextMeshProUGUI>().SetText(framerate + " fps");
    }
}
