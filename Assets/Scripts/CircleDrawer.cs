using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CircleDrawer : MonoBehaviour
{
    public Vector2 center = new Vector2(0.5f, 0.5f);
    public float radius = 0.45f;
    public float thickness = 0.1f;
    public int pointCount = 32;

    private LineRenderer lr;

    public void UpdateCircle()
    {
        if (lr == null) lr = GetComponent<LineRenderer>();
        Vector3[] points = new Vector3[pointCount];

        for (int p = 0; p < points.Length; p++)
        {
            float theta = (float) p * Mathf.PI * 2f /
                          points.Length; // theta will be from 0 to 6.28, perfect for trig operations
            points[p] = new Vector3(center.x + Mathf.Cos(theta) * radius, center.y + Mathf.Sin(theta) * radius, 0f);
        }

        lr.SetPositions(points);
        lr.loop = true;
        lr.useWorldSpace = true;
        lr.widthMultiplier = thickness;
    }

    void OnValidate()
    {
        UpdateCircle();
    }
}