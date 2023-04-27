using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NPCPrompt : MonoBehaviour
{
    [Range(0,50)]
    public int segments = 50;
    [Range(0,10)]
    public float xradius = 10;
    [Range(0,10)]
    public float yradius = 10;
    LineRenderer line;
    private GameObject player;
    private GameObject playerCamera;
    private bool inRadius;

    void Start ()
    {
        line = gameObject.GetComponent<LineRenderer>();
        playerCamera = GameObject.Find("PlayerCamera");

        player = GameObject.Find("FirstPersonController");
        line.SetVertexCount (segments + 1);
        line.useWorldSpace = false;
        CreatePoints ();
    }

    private void Update()
    {
        checkInteractable();
        if (inRadius)
        {
            checkDialogue();
        }
    }

    void CreatePoints ()
    {
        float x;
        float y;
        float z;

        float angle = 20f;

        for (int i = 0; i < (segments + 1); i++)
        {
            x = Mathf.Sin (Mathf.Deg2Rad * angle) * xradius;
            y = Mathf.Cos (Mathf.Deg2Rad * angle) * yradius;

            line.SetPosition (i,new Vector3(x,0,y) );

            angle += (360f / segments);
        }
    }

    void checkInteractable()
    {
        if (Vector3.Distance(gameObject.transform.position, player.transform.position) < xradius)
        {
            transform.GetChild(0).gameObject.SetActive(true);
            inRadius = true;
        }
        else
        {
            transform.GetChild(0).gameObject.SetActive(false);
            inRadius = false;
        }
    }

    void checkDialogue()
    {
        if (Input.GetKeyDown(KeyCode.E))
        {
            //GameObject.Find("DialogueUI").GetComponent<Animator>().GetBool("isOpen") =
                //!GameObject.Find("DialogueUI").GetComponent<Animator>().GetBool("isOpen");
                
        }
    }
}
