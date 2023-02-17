using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Valve.VR;

public class KartGoScript : MonoBehaviour
{
    public SteamVR_Action_Boolean kartGo;
    public SteamVR_Input_Sources rightHand;

    public SteamVR_Action_Boolean kartReverse;
    public SteamVR_Input_Sources leftHand;

    public SteamVR_Action_Boolean kartTurnLeft;
    public SteamVR_Action_Boolean kartTurnRight;
    public Transform frontLeftTire;
    public Transform frontRightTire;
    public Transform backLeftTire;
    public Transform backRightTire;

    public GameObject kart;
    public Rigidbody rb;

    private float CurrentSpeed = 0;
    public float MaxSpeed;
    private float RealSpeed;

    private float steerDirection;
    Vector3 steerDirVect;
    float steerAmount;
    void Start()
    {
        kartGo.AddOnStateDownListener(GoTriggerDown, rightHand);
        kartGo.AddOnStateUpListener(GoTriggerUp, rightHand);

        kartReverse.AddOnStateDownListener(ReverseTriggerDown, leftHand);

        kartTurnLeft.AddOnStateDownListener(LeftTurnDown, leftHand);
        kartTurnLeft.AddOnStateUpListener(LeftTurnUp, leftHand);

        kartTurnRight.AddOnStateDownListener(RightTurnDown, rightHand);
        kartTurnRight.AddOnStateUpListener(RightTurnDown, rightHand);
    }
    public void GoTriggerUp(SteamVR_Action_Boolean fromAction, SteamVR_Input_Sources fromSource)
    {
        Debug.Log("Go trigger is up");
        CurrentSpeed = Mathf.Lerp(CurrentSpeed, 0, Time.deltaTime * 1.5f);
    }
    public void GoTriggerDown(SteamVR_Action_Boolean fromAction, SteamVR_Input_Sources fromSource)
    {
        Debug.Log("Go trigger is down");
        CurrentSpeed = Mathf.Lerp(CurrentSpeed, MaxSpeed, Time.deltaTime * 0.5f);
    }


    public void ReverseTriggerDown(SteamVR_Action_Boolean fromAction, SteamVR_Input_Sources fromSource)
    {
        Debug.Log("Reverse trigger is down");
        CurrentSpeed = Mathf.Lerp(CurrentSpeed, -MaxSpeed / 1.75f, 1f * Time.deltaTime);
    }

    public void LeftTurnUp(SteamVR_Action_Boolean fromAction, SteamVR_Input_Sources fromSource)
    {
        Debug.Log("Left is up");
        frontLeftTire.localEulerAngles = Vector3.Lerp(frontLeftTire.localEulerAngles, new Vector3(0, 180, 0), 5 * Time.deltaTime);
        frontRightTire.localEulerAngles = Vector3.Lerp(frontLeftTire.localEulerAngles, new Vector3(0, 180, 0), 5 * Time.deltaTime);

        steerDirection = 0.0f;
    }
    public void LeftTurnDown(SteamVR_Action_Boolean fromAction, SteamVR_Input_Sources fromSource)
    {
        Debug.Log("Left is down");
        frontLeftTire.localEulerAngles = Vector3.Lerp(frontLeftTire.localEulerAngles, new Vector3(0, 155, 0), 5 * Time.deltaTime);
        frontRightTire.localEulerAngles = Vector3.Lerp(frontLeftTire.localEulerAngles, new Vector3(0, 155, 0), 5 * Time.deltaTime);

        steerDirection = -1.0f;
    }


    public void RightTurnUp(SteamVR_Action_Boolean fromAction, SteamVR_Input_Sources fromSource)
    {
        Debug.Log("Right is up");
        frontLeftTire.localEulerAngles = Vector3.Lerp(frontLeftTire.localEulerAngles, new Vector3(0, 180, 0), 5 * Time.deltaTime);
        frontRightTire.localEulerAngles = Vector3.Lerp(frontLeftTire.localEulerAngles, new Vector3(0, 180, 0), 5 * Time.deltaTime);

        steerDirection = 0.0f;
    }
    public void RightTurnDown(SteamVR_Action_Boolean fromAction, SteamVR_Input_Sources fromSource)
    {
        Debug.Log("Right is down");
        frontLeftTire.localEulerAngles = Vector3.Lerp(frontLeftTire.localEulerAngles, new Vector3(0, 155, 0), 5 * Time.deltaTime);
        frontRightTire.localEulerAngles = Vector3.Lerp(frontLeftTire.localEulerAngles, new Vector3(0, 155, 0), 5 * Time.deltaTime);

        steerDirection = 1.0f;
    }

    void FixedUpdate()
    {
        Vector3 vel = transform.forward * CurrentSpeed;
        vel.y = rb.velocity.y; //gravity
        rb.velocity = vel;

        RealSpeed = transform.InverseTransformDirection(rb.velocity).z;
        steerAmount = RealSpeed > 30 ? RealSpeed / 4 * steerDirection : steerAmount = RealSpeed / 1.5f * steerDirection;
        steerDirVect = new Vector3(transform.eulerAngles.x, transform.eulerAngles.y + steerAmount, transform.eulerAngles.z);
        transform.eulerAngles = Vector3.Lerp(transform.eulerAngles, steerDirVect, 3 * Time.deltaTime);
    }
}