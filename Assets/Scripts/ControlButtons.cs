using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class ControlButtons : MonoBehaviour
{
    private KeyCode k;
    public GameObject Player;
    void OnGUI()
    {
        Event e = Event.current;
        if (e.isKey)
        {
            Debug.Log("Detected key code: " + e.keyCode);
            k = e.keyCode;
        }
    }

    public void changePauseKey()
    {
        Debug.Log("Clicked");
        GameObject.Find("PauseMenu").GetComponent<PauseMenu>().changeKey(k);
    }

    public void changeJumpKey()
    {
        Player.GetComponent<FirstPersonController>().jumpKey = k;
        GameObject.Find("Jump Key").GetComponent<TextMeshProUGUI>().SetText("Jump Key - " + k);
    }

    public void changeSprintKey()
    {
        Player.GetComponent<FirstPersonController>().sprintKey = k;
        GameObject.Find("Sprint Key").GetComponent<TextMeshProUGUI>().SetText("Sprint Key - " + k);
    }
}
