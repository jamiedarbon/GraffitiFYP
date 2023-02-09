using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public struct textSection
{
    public string name;
    public Sprite image;
    [TextArea(3, 10)]
    public string sentence;
    public AudioClip[] sounds;
}
