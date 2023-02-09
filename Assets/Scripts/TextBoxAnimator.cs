using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TextBoxAnimator : MonoBehaviour
{
    [SerializeField] 
    public Sprite[] Sprites;
    private Sprite currentSprite;
    public int frames;
    private int i;
    
    // Start is called before the first frame update
    void Start()
    {
        GetComponent<Image>().sprite = Sprites[0];
        currentSprite = GetComponent<Image>().sprite;
        i = 0;
    }

    // Update is called once per frame
    void Update()
    {
        i++;
        if (i == frames)
        {
            if (GetComponent<Image>().sprite == Sprites[0])
            {
                GetComponent<Image>().sprite = Sprites[1];
            } else if (GetComponent<Image>().sprite == Sprites[1])
            {
                GetComponent<Image>().sprite = Sprites[2];
            } else if (GetComponent<Image>().sprite == Sprites[2])
            {
                GetComponent<Image>().sprite = Sprites[0];
            }

            i = 0;
        }
    }
}
