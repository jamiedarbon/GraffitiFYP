using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

[System.Serializable]
public class Dialogue {

    [SerializeField]
    public textSection[] sections;
    public Color colour;
    public bool slow = false;
}
