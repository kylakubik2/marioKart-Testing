using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KartGoScript : MonoBehaviour
{
    public SteamVR_Action_Boolean goAction;

    public GameObject kart;
    public Rigidbody rb;

    private float CurrentSpeed = 0;
    private float RealSpeed;

    private void OnEnable()
    {
        if (goAction == null)
        {
            Debug.LogError("<b>[SteamVR Interaction]</b> No go action assigned", this);
            return;
        }

        goAction.AddOnChangeListener(OnGoActionChange, kart);
    }

    private void OnDisable()
    {
        if (goAction != null)
            goAction.RemoveOnChangeListener(OnGoActionChange, kart);
        
    }

    private void OnGoActionChange(SteamVR_Action_Boolean actionIn, SteamVR_Input_Sources inputSource, bool newValue)
    {
        if (newValue)
        {
            move();
        }
    }

    private void move()
    {
        RealSpeed = kart.transform.InverseTransformDirection(rb.velocity).z; //real velocity before setting the value. This can be useful if say you want to have hair moving on the player, but don't want it to move if you are accelerating into a wall, since checking velocity after it has been applied will always be the applied value, and not real

        if (goAction)
        {
            CurrentSpeed = Mathf.Lerp(CurrentSpeed, MaxSpeed, Time.deltaTime * 0.5f); //speed
        }
        else
        {
            CurrentSpeed = Mathf.Lerp(CurrentSpeed, 0, Time.deltaTime * 1.5f); //speed
        }

        /*
        if (!GLIDER_FLY)
        {
            Vector3 vel = transform.forward * CurrentSpeed;
            vel.y = rb.velocity.y; //gravity
            rb.velocity = vel;
        }
        else
        {
            Vector3 vel = kart.transform.forward * CurrentSpeed;
            vel.y = rb.velocity.y * 0.6f; //gravity with gliding
            rb.velocity = vel;
        }
        */

    }
}
