using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour
{
    public GameObject kart;
    private Vector3 offset;
    private Quaternion target;

    // Start is called before the first frame update
    void Start()
    {
        offset = transform.position - kart.transform.position;
    }

    // Update is called once per frame
    void Update()
    {
        transform.position = kart.transform.position + offset;
    }
}
