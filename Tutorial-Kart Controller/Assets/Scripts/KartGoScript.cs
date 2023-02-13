using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Valve.VR;

public class KartGoScript : MonoBehaviour
{
    public SteamVR_Action_Boolean kartGo;
    public SteamVR_Input_Sources handType;
    public GameObject kart;
    private Rigidbody rb;

    private float CurrentSpeed = 0;
    public float MaxSpeed;

    void Start()
    {
        kartGo.AddOnStateDownListener(TriggerDown, handType);
        kartGo.AddOnStateUpListener(TriggerUp, handType);

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
        CurrentSpeed = Mathf.Lerp(CurrentSpeed, MaxSpeed, Time.deltaTime * 0.5f);
    }

    void FixedUpdate()
    {
        Vector3 vel = transform.forward * CurrentSpeed;
        vel.y = rb.velocity.y; //gravity
        rb.velocity = vel;
    }

}