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
    private GameObject actor;
    [SerializeField]
    private int questNumber;
    public bool facePlayer;

    private void Start()
    {
        dialogueManager = GameObject.Find("DialogueManager").GetComponent<DialogueManager>();
        Debug.Log(dialogueManager.name);
        interactable = false;
        talking = false;
        actor = gameObject;
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

        if (facePlayer)
        {
            //rotate the actor towards the player
            int modifier = 2;
            float tempX, tempZ;
            tempX = actor.transform.rotation.x;
            tempZ = actor.transform.rotation.z;
            Vector3 lookPos = Player.transform.position - actor.transform.position;
            var rotation = Quaternion.LookRotation(lookPos);
            actor.transform.rotation = Quaternion.Slerp(actor.transform.rotation, rotation, Time.deltaTime * modifier);
            //actor.transform.rotation = Quaternion.Euler(actor.transform.rotation.x, actor.transform.rotation.y, actor.transform.rotation.z);
        }
    }

    public void TriggerDialogue()
    {
        //Prompt.SetActive(false);
        facePlayer = true;
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
