using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class QuestSystem : MonoBehaviour
{
    public GameObject Player;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void QuestManager(int questNumber)
    {
        switch(questNumber)
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
}
