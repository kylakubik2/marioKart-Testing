using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Valve.VR;

public class KartReverseScript : MonoBehaviour
{
    public SteamVR_Action_Boolean kartReverse;
    public SteamVR_Input_Sources handType;
    public GameObject kart;
    private Rigidbody rb;

    private float CurrentSpeed = 0;
    public float MaxSpeed;

    void Start()
    {
        kartReverse.AddOnStateDownListener(TriggerDown, handType);
        kartReverse.AddOnStateUpListener(TriggerUp, handType);

        rb = GetComponent<Rigidbody>();
    }

    public void TriggerUp(SteamVR_Action_Boolean fromAction, SteamVR_Input_Sources fromSource)
    {
        Debug.Log("Trigger is up");
        CurrentSpeed = Mathf.Lerp(CurrentSpeed, 0, Time.deltaTime * 1.5f);
    }

    public void TriggerDown(SteamVR_Action_Boolean fromAction, SteamVR_Input_Sources fromSource)
    {
        Debug.Log("Trigger is down");
        CurrentSpeed = Mathf.Lerp(CurrentSpeed, -MaxSpeed / 1.75f, 1f * Time.deltaTime);
    }

    void FixedUpdate()
    {
        Vector3 vel = transform.forward * CurrentSpeed;
        vel.y = rb.velocity.y; //gravity
        rb.velocity = vel;
    }
}
