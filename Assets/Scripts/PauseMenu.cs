using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class PauseMenu : MonoBehaviour
{
    public KeyCode pauseKey = KeyCode.I;

    private void Start()
    {
        Debug.Log("Starting");
        changeKey(pauseKey);
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(pauseKey))
        {
            Debug.Log(GameObject.Find("PauseMenu"));
            GameObject.Find("PauseMenu").SetActive(true);
        }
    }

    public void changeKey(KeyCode k)
    {
        pauseKey = k;
        GameObject.Find("PauseKey").GetComponent<TextMeshProUGUI>().SetText("Pause - " + k);
        Debug.Log("UI text changed");
    }
}
