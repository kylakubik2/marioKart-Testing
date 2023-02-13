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
    private float RealSpeed;

    private bool going;

    void Start()
    {
        kartGo.AddOnStateDownListener(TriggerDown, handType);
        kartGo.AddOnStateUpListener(TriggerUp, handType);
    }

    public void TriggerUp(SteamVR_Action_Boolean fromAction, SteamVR_Input_Sources fromSource)
    {
        Debug.Log("Trigger is up");
        going = false;
    }

    public void TriggerDown(SteamVR_Action_Boolean fromAction, SteamVR_Input_Sources fromSource)
    {
        Debug.Log("Trigger is down");
        going = true;
    }

    void FixedUpdate()
    {
        move();
    }

    private void move()
    {
        RealSpeed = transform.InverseTransformDirection(rb.velocity).z; //real velocity before setting the value. This can be useful if say you want to have hair moving on the player, but don't want it to move if you are accelerating into a wall, since checking velocity after it has been applied will always be the applied value, and not real

        if (going)
        {
            CurrentSpeed = Mathf.Lerp(CurrentSpeed, MaxSpeed, Time.deltaTime * 0.5f); //speed
        }
        else
        {
            CurrentSpeed = Mathf.Lerp(CurrentSpeed, 0, Time.deltaTime * 1.5f); //speed
        }

    }
}
