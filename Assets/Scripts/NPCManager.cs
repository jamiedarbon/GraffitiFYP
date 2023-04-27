using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NPCManager : MonoBehaviour
{
    public bool interactable;
    public GameObject Player;
    // Start is called before the first frame update
    void Start()
    {
        interactable = false;
    }

    // Update is called once per frame
    void Update()
    {
        if (Vector3.Distance(transform.position, Player.transform.position) < 5)
        {
            GetComponent<DialogueTrigger>().enabled = true;
        } else
        {
            GetComponent<DialogueTrigger>().enabled = false;
        }
    }
}
