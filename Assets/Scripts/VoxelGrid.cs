using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class VoxelGrid : MonoBehaviour
{
    public int resolutionX;
    public int resolutionY;
    private Texture2D tex;

    private bool[] voxels;
    private float voxelSize;

    private void Awake()
    {
        voxelSize = 1f / resolutionX;
        tex = GetComponent<Renderer>().material.GetTexture("_MainTex") as Texture2D;
        Debug.Log("Height = " + tex.height + ", Width = " + tex.width);
        voxels = new bool[resolutionX * resolutionY];
        for (int i = 0, y = 0; y < resolutionX; y++) {
            for (int x = 0; x < resolutionY; x++, i++) {
                //CreateVoxel(i, x, y);
            }
        }
    }
}
