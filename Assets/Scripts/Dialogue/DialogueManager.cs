using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class DialogueManager : MonoBehaviour
{
    public TextMeshProUGUI nameText;
    public TextMeshProUGUI dialogueText;
    public Image characterPortrait;

    private float waitTime;
    public bool talking;
    private bool interrupt;
    private Dialogue currentDialogue;

    public Animator animator;
    public AudioSource voice;

    //public GameObject mainCamera;
    //public GameObject dialogueCamera;

    private Queue<string> sentences;
    private Queue<Sprite> images;
    private Queue<string> names;
    private Queue<AudioClip[]> sounds;

    // Start is called before the first frame update
    void Start()
    {
        talking = false;
        waitTime = 0.025f;
        sentences = new Queue<string>();
        images = new Queue<Sprite>();
        names = new Queue<string>();
        sounds = new Queue<AudioClip[]>();
    }

    public void StartDialogue(Dialogue dialogue)
    {
        currentDialogue = dialogue;
        interrupt = false;
        talking = true;
        //mainCamera.SetActive(false);
        //dialogueCamera.SetActive(true);

        animator.SetBool("isOpen", true);

        sentences.Clear();
        images.Clear();
        names.Clear();
        sounds.Clear();

        foreach (textSection section in dialogue.sections)
        {
            sentences.Enqueue(section.sentence);
            images.Enqueue(section.image);
            names.Enqueue(section.name);
            sounds.Enqueue(section.sounds);
        }

        DisplayNextSentence();
    }

    public void DisplayNextSentence()
    {
        if (sentences.Count == 0)
        {
            EndDialogue();
            return;
        }
        string sentence = sentences.Dequeue();
        //characterPortrait.sprite = images.Dequeue();
        //nameText.text = names.Dequeue();
        StopAllCoroutines();
        StartCoroutine(TypeSentence(sentence));
    }

    IEnumerator TypeSentence (string sentence)
    {
        AudioClip[] sectionVoice = sounds.Dequeue();
        dialogueText.text = "";
        foreach (char c in sentence.ToCharArray())
        {
            if (c == '`')
            {
                currentDialogue.slow = !currentDialogue.slow;
                if (currentDialogue.slow)
                {
                    waitTime = 0.25f;
                } else if (!currentDialogue.slow)
                {
                    waitTime = 0.025f;
                }
            } else if (c == '¬')
            {
                //dialogueText.transform.position = dialogueText.transform.position + new Vector3(0, 10, 0);
            }
            else
            {
                dialogueText.text += c;
                if (!interrupt)
                {
                    int r = Random.Range(0, sectionVoice.Length);
                    voice.clip = sectionVoice[r];
                    voice.Play();
                }
                yield return new WaitForSeconds(waitTime);
            }
        }
    }

    public void EndDialogue()
    {
        interrupt = true;
        talking = false;
        animator.SetBool("isOpen", false);
        //mainCamera.SetActive(true);
        //dialogueCamera.SetActive(false);
    }
}
