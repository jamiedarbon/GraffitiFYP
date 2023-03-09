using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DialogueTrigger : MonoBehaviour
{
    public Dialogue dialogue;
    [SerializeField]
    private GameObject Player;
    //[SerializeField]
    //private GameObject Prompt;
    private DialogueManager dialogueManager;
    private bool interactable;
    private bool talking;
    [SerializeField]
    private int questNumber;

    private void Start()
    {
        dialogueManager = GameObject.Find("DialogueManager").GetComponent<DialogueManager>();
        Debug.Log(dialogueManager.name);
        interactable = false;
        talking = false;
    }

    public void Update()
    {
        if (Vector3.Distance(transform.position, Player.transform.position) < 5)
        {
            interactable = true;
            if(!dialogueManager.talking)
            {
                //Prompt.SetActive(true);
            }
        } else
        {
            interactable = false;
            //Prompt.SetActive(false);
        }
        if (Input.GetKeyDown(KeyCode.Q) && interactable)
        {
            TriggerDialogue();
        }
        if (Input.GetKeyDown(KeyCode.Return))
        {
            GameObject.Find("DialogueManager").GetComponent<DialogueManager>().DisplayNextSentence();
        }
    }

    public void TriggerDialogue()
    {
        //Prompt.SetActive(false);
        //If the current NPC has a quest, activate their associated quest behaviour
        if (questNumber > 0) { 
            Debug.Log("Quest Start");
            GameObject.Find("QuestManager").GetComponent<QuestSystem>().QuestManager(questNumber);
        }
        Debug.Log("Quest End");
        FindObjectOfType<DialogueManager>().actor = this.gameObject;
        FindObjectOfType<DialogueManager>().StartDialogue(dialogue);
    }
}
