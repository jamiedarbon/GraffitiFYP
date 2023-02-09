using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PromptRotator : MonoBehaviour
{
    public GameObject player;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        Vector3 newDirection = Vector3.RotateTowards(transform.forward, player.transform.position - transform.position, 
            5 * Time.deltaTime, 0.0f);

        transform.rotation = Quaternion.LookRotation(newDirection);
    }
}
