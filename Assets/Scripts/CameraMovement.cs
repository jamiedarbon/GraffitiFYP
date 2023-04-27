using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraMovement : MonoBehaviour
{
    private float Xrot, Yrot;

    public float sensitivity = 100f;
    public Transform orientation;
    void Start()
    {
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
    }

    private void Update()
    {
        float mouseX = Input.GetAxisRaw("Mouse X") * Time.deltaTime * sensitivity;
        float mouseY = Input.GetAxisRaw("Mouse Y") * Time.deltaTime * sensitivity;

        Debug.Log(mouseX + ", " + mouseY);

        Yrot += mouseX;
        Xrot -= mouseY;
        Xrot = Mathf.Clamp(Xrot, -90f, 90f);

        transform.rotation = Quaternion.Euler(Xrot, Yrot, 0);
        orientation.rotation = Quaternion.Euler(0, Yrot, 0);
    }
}
