using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine;

public class ColourPicker : MonoBehaviour
{
    public Slider mHue, mSat, mVal;
    private float fHue, fSat, fVal;
    private GameObject player, playerMovement;
    public KeyCode colourPickerKey = KeyCode.H;
    private bool hasChanged = false;
    private bool isActive = false;

    private bool choosingUI;
    // Start is called before the first frame update
    void Start()
    {
        mHue.minValue = 0;
        mSat.minValue = 0;
        mVal.minValue = 0;

        mHue.maxValue = 1;
        mSat.maxValue = 1;
        mVal.maxValue = 1;

        mHue.value = 1;
        mSat.value = 1;
        mVal.value = 1;

        player = GameObject.Find("PlayerCamera");
        playerMovement = GameObject.Find("FirstPersonController");
    }

    // Update is called once per frame
    void Update()
    {
        fHue = mHue.value;
        fSat = mSat.value;
        fVal = mVal.value;

        //Debug.Log("mHue = " + mHue.value);

        player.GetComponent<RayCast>().Colour = Color.HSVToRGB(fHue, fSat, fVal);
        if (GameObject.Find("Container").activeSelf)
        {
            GameObject.Find("Container").transform.GetChild(0).transform.GetChild(1).GetComponent<Image>().color = 
                Color.HSVToRGB(fHue,fSat,fVal);   
        }

        if (Input.GetKeyDown(colourPickerKey))
        {
            for (int i = 0; i < player.GetComponent<RayCast>().testArray.Length; i++)
            {
                player.GetComponent<RayCast>().testArray[i] = Color.HSVToRGB(fHue, fSat, fVal);
            }
            player.GetComponent<RayCast>().canPaint = !player.GetComponent<RayCast>().canPaint;
            //Lock cursor to center if not picking colour and unlock it when picking colours to allow for use of UI
            if (Cursor.lockState == CursorLockMode.None && !hasChanged)
            {
                Debug.Log("Locked");
                Cursor.lockState = CursorLockMode.Locked;
                hasChanged = true;
            }

            if (Cursor.lockState == CursorLockMode.Locked && !hasChanged)
            {
                Debug.Log("Unlocked");
                Cursor.lockState = CursorLockMode.None;
                hasChanged = true;
            }

            hasChanged = false;
            Debug.Log("Picker toggled");
            /*GameObject.Find("PlayerCamera").GetComponent<RayCast>().canPaint =
                !GameObject.Find("PlayerCamera").GetComponent<RayCast>().canPaint;*/
            GameObject g = GameObject.Find("Container");
            //g.transform.GetChild(0).gameObject.SetActive(!g.transform.GetChild(0).gameObject.activeSelf);
            g.GetComponent<Animator>().SetBool("IsActive", !isActive);
            isActive = !isActive;
            playerMovement.GetComponent<FirstPersonController>().lockCursor =
                !playerMovement.GetComponent<FirstPersonController>().lockCursor;
            playerMovement.GetComponent<FirstPersonController>().crosshair =
                !playerMovement.GetComponent<FirstPersonController>().crosshair;
            playerMovement.GetComponent<FirstPersonController>().cameraCanMove =
                !playerMovement.GetComponent<FirstPersonController>().cameraCanMove;
            playerMovement.GetComponent<FirstPersonController>().playerCanMove =
                !playerMovement.GetComponent<FirstPersonController>().playerCanMove;
        }
    }
}
