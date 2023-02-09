using System;
using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneTransition : MonoBehaviour
{
    public Animator transition;
    public GameObject Player;
    void Awake() {
        DontDestroyOnLoad(this.gameObject);
        DontDestroyOnLoad(Player);
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Return))
        {
            Debug.Log("Transition");
            StartCoroutine(LoadNext());
        }
    }

    IEnumerator LoadNext()
    {
        transition.SetBool("Transition", true);
        yield return new WaitForSeconds(1f);
        Scene scene = SceneManager.GetActiveScene();
        int nextLevelBuildIndex = 1 - scene.buildIndex;
        SceneManager.LoadScene(nextLevelBuildIndex);
        transition.SetBool("Transition", false);
    }
}
