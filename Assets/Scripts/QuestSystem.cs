using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class QuestSystem : MonoBehaviour
{
    public GameObject Player;
    public bool tutorial;
    // Start is called before the first frame update
    void Start()
    {
        tutorial = false;
    }

    // Update is called once per frame
    void Update()
    {
        if (tutorial)
        {
            TutorialFinish();
        }
    }

    public void QuestManager(int questNumber)
    {
        switch (questNumber)
        {
            case 1:
                TutorialQuest();
                break;
        }
    }

    private void TutorialQuest()
    {
        Player.GetComponent<RayCast>().paintCheck = true;
    }

    private void TutorialFinish()
    {
        GameObject.Find("lolipop").SetActive(false);
        GameObject.Find("lolipop 2").gameObject.transform.GetChild(0).gameObject.SetActive(true);
        tutorial = false;
    }
}
