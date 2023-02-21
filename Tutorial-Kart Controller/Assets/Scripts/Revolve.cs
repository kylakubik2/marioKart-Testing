using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Revolve : MonoBehaviour
{
    public float revSpeed;
    public float radius;
    private float angle;

    void Update()
    {
        angle += revSpeed * Time.deltaTime;

        float xNew = Mathf.Sin(angle) * radius;
        float zNew = Mathf.Cos(angle) * radius;

        transform.position = new Vector3(xNew, 0.5f, zNew - 6.0f);
    }
}
