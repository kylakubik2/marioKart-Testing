using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour
{
    public GameObject kart;
    private Vector3 offset;

    private float xRotation;
    private float yRotation;
    private float zRotation;

    private Quaternion target;
    private float smooth = 5.0f;

    // Start is called before the first frame update
    void Start()
    {
        offset = transform.position - kart.transform.position;
    }

    // Update is called once per frame
    void Update()
    {
        transform.position = kart.transform.position + offset;

        if (kart.transform.eulerAngles.x <= 180f)
        {
            xRotation = kart.transform.eulerAngles.x;
        }
        else
        {
            xRotation = kart.transform.eulerAngles.x - 360f;
        }

        if (kart.transform.eulerAngles.y <= 180f)
        {
            yRotation = kart.transform.eulerAngles.y;
        }
        else
        {
            yRotation = kart.transform.eulerAngles.y - 360f;
        }

        if (kart.transform.eulerAngles.x <= 180f)
        {
            zRotation = kart.transform.eulerAngles.z;
        }
        else
        {
            zRotation = kart.transform.eulerAngles.z - 360f;
        }

        target = new Quaternion(xRotation, yRotation, zRotation, kart.transform.rotation.w);
        transform.rotation = Quaternion.Slerp(transform.rotation, target, Time.deltaTime * smooth);

    }
}
