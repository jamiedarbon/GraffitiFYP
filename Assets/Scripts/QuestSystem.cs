using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class QuestSystem : MonoBehaviour
{
    public GameObject Player;
    public bool tutorial, banner, banner2;
    // Start is called before the first frame update
    void Start()
    {
        tutorial = false;
        banner = false;
        banner2 = false;
    }

    // Update is called once per frame
    void Update()
    {
        if (tutorial)
        {
            TutorialFinish();
        }
        if (banner)
        {
            ShopKeepTwo();
        }
        if(banner2)
        {
            ShopKeepEnd();
        }
    }

    public void QuestManager(int questNumber)
    {
        switch (questNumber)
        {
            case 1:
                TutorialQuest();
                break;

            case 2:
                ShopKeepQuest();
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

    private void ShopKeepQuest()
    {
        Player.GetComponent<RayCast>().bannerCheck = true;
    }

    private void ShopKeepTwo()
    {
        Player.GetComponent<RayCast>().bannerCheck2 = true;
        GameObject.Find("lolipop (1)").SetActive(false);
        GameObject.Find("lolipop (2)").gameObject.transform.GetChild(0).gameObject.SetActive(true);
    }

    private void ShopKeepEnd()
    {
        GameObject.Find("lolipop (2)").gameObject.transform.GetChild(0).gameObject.SetActive(false);
        GameObject.Find("lolipop (3)").gameObject.transform.GetChild(0).gameObject.SetActive(true);
    }
}
